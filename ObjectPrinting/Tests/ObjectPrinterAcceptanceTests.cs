﻿using System;
using System.Globalization;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    public class ObjectPrinterAcceptanceTests
    {
        [Test]
        public void Demo()
        {
            var person = new Person {Name = "Alex", Age = 19};

            var printer = ObjectPrinter.For<Person>();
            //1. Исключить из сериализации свойства определенного типа
            printer.ExcludeType<int>();
            //2. Указать альтернативный способ сериализации для определенного типа
            printer.Printing<int>().Using(x => x.ToString());
            //3. Для числовых типов указать культуру
            printer.Printing<int>().UsingCulture(CultureInfo.CurrentCulture);
            //4. Настроить сериализацию конкретного свойства
            printer.Printing(x => x.Age).Using(x => "5");
            //5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
            printer.Printing<string>().LengthRestrictionTo(10);
            //6. Исключить из сериализации конкретного свойства
            printer.ExcludeProperty(x => x.Age);

            string s1 = printer.PrintToString(person);
            //7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию
            string s2 = person.PrintToString();
      
            //8. ...с конфигурированием
            string s3 = person.PrintToString(s => s.ExcludeProperty(p => p.Age));
        }
    }
}