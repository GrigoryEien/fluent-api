using System;
using System.Globalization;
using System.Numerics;

namespace ObjectPrinting
{
	static class SerializeConfigExtensions
	{
		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, int> config,
			CultureInfo culture)
		{
			// ...
			return ((ISerializeConfig<TOwner, int>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, float> config,
			CultureInfo culture)
		{
			// ...
			return ((ISerializeConfig<TOwner, float>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, double> config,
			CultureInfo culture)
		{
			// ...
			return ((ISerializeConfig<TOwner, double>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, BigInteger> config,
			CultureInfo culture)
		{
			// ...
			return ((ISerializeConfig<TOwner, BigInteger>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> Using<TOwner>(this SerializeConfig<TOwner, string> config,
			Func<string, string> formatter)
		{
			// ...
			return ((ISerializeConfig<TOwner, string>)config).PrintingConfig;
		}
	}
}