using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner>
    {
	    public PrintingConfig<TOwner> ExcludeType<TType>()
	    {
			    return this;
	    }

	    public SerializeConfig<TOwner, TType> Printing<TType>()
	    {
		    return new SerializeConfig<TOwner,TType>(this);
	    }

		public PrintingConfig<TOwner> SetCultureForNumericData(CultureInfo culture)
	    {
		    return this;
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
            //TODO apply configurations
            if (obj == null)
                return "null" + Environment.NewLine;

            var finalTypes = new[]
            {
                typeof(int), typeof(double), typeof(float), typeof(string),
                typeof(DateTime), typeof(TimeSpan)
            };
            if (finalTypes.Contains(obj.GetType()))
                return obj + Environment.NewLine;

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            var type = obj.GetType();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in type.GetProperties())
            {
                sb.Append(identation + propertyInfo.Name + " = " +
                          PrintToString(propertyInfo.GetValue(obj),
                              nestingLevel + 1));
            }
            return sb.ToString();
        }

	    public PrintingConfig<TOwner> ExcludeProperty<TProperty>(Expression<Func<TOwner, TProperty>> func)
	    {
			// ...
		    return this;
	    }
    }
}