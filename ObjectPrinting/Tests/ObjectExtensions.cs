using System;

namespace ObjectPrinting.Tests
{
    public static class ObjectExtensions
    {
        public static string PrintToString<T>(this T obj)
        {
            var printer = ObjectPrinter.For<T>();
            return printer.PrintToString(obj);
        }
        
        public static string PrintToString<T>(this T obj, Action<PrintingConfig<T>> configurator)
        {
            var printer = ObjectPrinter.For<T>();
            configurator(printer);
            return printer.PrintToString(obj);
        }
    }
}