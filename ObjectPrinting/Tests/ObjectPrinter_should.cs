using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    public class ObjectPrinter_should
    {
        [TestFixture]
        public class ObjectPrinter_Should_Should
        {
            [Test]
            public void IgnoreExcludedType()
            {
                var person = new Person { Name = "Alex", Age = 19 };

                var printer = ObjectPrinter.For<Person>();
                printer.ExcludeType<int>();
                var expected = "Person\r\n	Id = Guid\r\n	Name = Alex\r\n	Height = 0\r\n";
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

			[Test]
			public void UseCustomSerializationForType() {
				var person = new Person { Name = "Alex", Age = 19 };

				var printer = ObjectPrinter.For<Person>();
				printer.Printing<int>().Using(x => (2 * x).ToString());
				var expected = "Person\r\n	Id = Guid\r\n	Name = Alex\r\n	Height = 0\r\n	Age = 38\r\n";
				var actual = printer.PrintToString(person);
				actual.Should().Be(expected);
			}
	        
	        [Test]
	        public void UseCustomSerializationForProperty() {
		        var person = new Person { Name = "Alex", Age = 19 };

		        var printer = ObjectPrinter.For<Person>();
		        printer.Printing( x => x.Age).Using(x => (3 * x).ToString());
		        var expected = "Person\r\n	Id = Guid\r\n	Name = Alex\r\n	Height = 0\r\n	Age = 57\r\n";
		        var actual = printer.PrintToString(person);
		        actual.Should().Be(expected);
	        }

			[Test]
            public void IgnoreExcludedProperty()
            {
                var person = new Person { Name = "Alex", Age = 19 };

                var printer = ObjectPrinter.For<Person>();
                printer.ExcludeProperty(x => x.Id);
                var expected = "Person\r\n	Name = Alex\r\n	Height = 0\r\n	Age = 19\r\n";
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

	        [Test]
	        public void SerializeIntWithCulture()
	        {
		        var person = new Person { Name = "Alex", Age = 19 };
		        var printer = ObjectPrinter.For<Person>();
		        printer.Printing<int>().UsingCulture(new CultureInfo("ru-RU"));
		        var expected = "Person\r\n	Name = Alex\r\n	Height = 0\r\n	Age = "+(19).ToString(new CultureInfo("ru-RU")+"\r\n");
		        var actual = printer.PrintToString(person);
	        }
	        
	        [Test]
	        public void SerializeDoubleWithCulture()
	        {
		        var person = new Person { Name = "Alex", Age = 19 };
		        var printer = ObjectPrinter.For<Person>();
		        printer.Printing<double>().UsingCulture(new CultureInfo("ru-RU"));
		        var expected = "Person\r\n	Name = Alex\r\n	Height = "+(0).ToString(new CultureInfo("ru-RU")+"\r\n	Age = 19\r\n");
		        var actual = printer.PrintToString(person);
	        }

	        [Test]
	        public void CutStringWhenSerializing()
	        {
		        var person = new Person { Name = "Alex", Age = 19 };

		        var printer = ObjectPrinter.For<Person>();
		        printer.Printing<string>().Using(2);
		        var expected = "Person\r\n	Id = Guid\r\n	Name = Al\r\n	Height = 0\r\n	Age = 19\r\n";
		        var actual = printer.PrintToString(person);
		        actual.Should().Be(expected);
	        }
        }
    }
}