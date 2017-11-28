using System;
using System.Globalization;
using System.Runtime.InteropServices;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    public class ObjectPrinter_should
    {
        [TestFixture]
        public class ObjectPrinter_Should_Should
        {
            private Person person;
            private PrintingConfig<Person> printer;

            [SetUp]
            public void SetUp()
            {
                person = new Person {Name = "Alex", Age = 19};
                printer = ObjectPrinter.For<Person>();
            }

            [Test]
            public void IgnoreExcludedType()
            {
                printer.ExcludeType<int>();
                var expected = String.Format("Person{0}	Id = Guid{0}	Name = Alex{0}	Height = 0{0}",
                    Environment.NewLine);
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

            [Test]
            public void UseCustomSerializationForType()
            {
                printer.Printing<int>().Using(x => (2 * x).ToString());
                var expected = String.Format("Person{0}	Id = Guid{0}	Name = Alex{0}	Height = 0{0}	Age = 38{0}",
                    Environment.NewLine);
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

            [Test]
            public void UseCustomSerializationForProperty()
            {
                printer.Printing(x => x.Age).Using(x => (3 * x).ToString());
                var expected = String.Format("Person{0}	Id = Guid{0}	Name = Alex{0}	Height = 0{0}	Age = 57{0}",
                    Environment.NewLine);
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

            [Test]
            public void IgnoreExcludedProperty()
            {
                printer.ExcludeProperty(x => x.Id);
                var expected = String.Format("Person{0}	Name = Alex{0}	Height = 0{0}	Age = 19{0}", Environment.NewLine);
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

            [Test]
            public void SerializeIntWithCulture()
            {
                printer.Printing<int>().UsingCulture(new CultureInfo("ru-RU"));
                var expected = String.Format("Person{0}	Name = Alex{0}	Height = 0{0}	Age = {1}{0}", Environment.NewLine,
                    (19).ToString(new CultureInfo("ru-RU")));

                var actual = printer.PrintToString(person);
            }

            [Test]
            public void SerializeDoubleWithCulture()
            {
                printer.Printing<double>().UsingCulture(new CultureInfo("ru-RU"));
                var expected = String.Format("Person{0}	Name = Alex{0}	Height = {1}{0}	Age = 19{0}",
                    Environment.NewLine,
                    (0).ToString(new CultureInfo("ru-RU")));
                var actual = printer.PrintToString(person);
            }

            [Test]
            public void CutStringWhenSerializing()
            {
                printer.Printing<string>().LengthRestrictionTo(2);
                var expected = String.Format("Person{0}	Id = Guid{0}	Name = Al{0}	Height = 0{0}	Age = 19{0}",
                    Environment.NewLine);
                var actual = printer.PrintToString(person);
                actual.Should().Be(expected);
            }

            [Test]
            public void ShouldSerializeNestedObject()
            {
                var child = new NestedObject(null, 13, 2.5, 9, "LongRandomName");
                var parent = new NestedObject(child, 15, 5.7, 11, "EvenLongestRandomName");

                var nestedPrinter = new PrintingConfig<NestedObject>();
                var expected = String.Format(
                    "NestedObject{0}	Child = NestedObject{0}		Child = null{0}		Id = 13{0}		Weight = 2.5{0}		Height = 9{0}		Name = LongRandomName{0}	" +
                    "Id = 15{0}	Weight = 5.7{0}	Height = 11{0}	Name = EvenLongestRandomName{0}",
                    Environment.NewLine);
                var actual = nestedPrinter.PrintToString(parent);

                actual.Should().Be(expected);
            }
        }
    }
}