using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using FluentAssertions;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner>
    {
        private readonly HashSet<Type> excludedTypes = new HashSet<Type>();
        public Dictionary<Type, Delegate> speciallySerializedTypes = new Dictionary<Type, Delegate>();
        private readonly HashSet<string> excludedProperties = new HashSet<string>();

        private static Type[] finalTypes = new[]
        {
            typeof(int), typeof(double), typeof(float), typeof(string),
            typeof(DateTime), typeof(TimeSpan)
        };

        public PrintingConfig<TOwner> ExcludeProperty<TProperty>(Expression<Func<TOwner, TProperty>> selector)
        {
            var className = typeof(TOwner).ToString();
            var member = (selector.Body as MemberExpression).ToString();
            var dotIndex = member.IndexOf('.');
            member = member.Substring(dotIndex);
            excludedProperties.Add(className + member);
            return this;
        }

        public PrintingConfig<TOwner> ExcludeType<TType>()
        {
            excludedTypes.Add(typeof(TType));
            return this;
        }

        public SerializeConfig<TOwner, TType> Printing<TType>()
        {
            return new SerializeConfig<TOwner, TType>(this);
        }

        public SerializeConfig<TOwner, TProperty> Printing<TProperty>(
            Func<TOwner, TProperty> propetySelector)
        {
            return new SerializeConfig<TOwner, TProperty>(this);
        }

        public string PrintToString(TOwner obj)
        {
            return PrintToString(obj, 0);
        }

        private string PrintToString(object obj, int nestingLevel)
        {
            if (obj == null)
                return "null" + Environment.NewLine;


            if (finalTypes.Contains(obj.GetType()))
                return obj + Environment.NewLine;

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            var type = obj.GetType();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in type.GetProperties())
            {
                var propertyPath = propertyInfo.DeclaringType.ToString() + "." + propertyInfo.Name;

                if (excludedTypes.Contains(propertyInfo.PropertyType))
                    continue;
                if (excludedProperties.Contains(propertyPath))
                    continue;
                if (speciallySerializedTypes.ContainsKey(propertyInfo.PropertyType))
                    sb.AppendLine(identation + propertyInfo.Name + " = " +
                              speciallySerializedTypes[propertyInfo.PropertyType]
                                  .DynamicInvoke(propertyInfo.GetValue(obj)));
                else
                    sb.Append(identation + propertyInfo.Name + " = " +
                              PrintToString(propertyInfo.GetValue(obj),
                                  nestingLevel + 1));
            }
            return sb.ToString();
        }
    }
}