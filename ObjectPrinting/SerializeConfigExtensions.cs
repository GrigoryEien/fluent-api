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
			var printingConfig = ((ISerializeConfig<TOwner, int>) config).PrintingConfig;
			printingConfig.speciallySerializedTypes.Add(typeof(int), new Func<int,string>(x =>x.ToString(culture)));	
			return ((ISerializeConfig<TOwner, int>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, float> config,
			CultureInfo culture)
		{

			var printingConfig = ((ISerializeConfig<TOwner, float>) config).PrintingConfig;
			printingConfig.speciallySerializedTypes.Add(typeof(int), new Func<int,string>(x =>x.ToString(culture)));	
			return ((ISerializeConfig<TOwner, float>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, double> config,
			CultureInfo culture)
		{
			
			var printingConfig = ((ISerializeConfig<TOwner, double>) config).PrintingConfig;
			printingConfig.speciallySerializedTypes.Add(typeof(int), new Func<int,string>(x =>x.ToString(culture)));	
			return ((ISerializeConfig<TOwner, double>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, BigInteger> config,
			CultureInfo culture)
		{
			var printingConfig = ((ISerializeConfig<TOwner, BigInteger>) config).PrintingConfig;
			printingConfig.speciallySerializedTypes.Add(typeof(int), new Func<int,string>(x =>x.ToString(culture)));	
			return ((ISerializeConfig<TOwner, BigInteger>)config).PrintingConfig;
		}

		public static PrintingConfig<TOwner> LengthRestrictionTo<TOwner>(this SerializeConfig<TOwner, string> config,
			int lenght)
		{
			var printingConfig = ((ISerializeConfig<TOwner, string>) config).PrintingConfig;
			printingConfig.speciallySerializedTypes.Add(typeof(string), new Func<string,string>(x => x.Substring(0,lenght)));	
			return ((ISerializeConfig<TOwner, string>)config).PrintingConfig;
		}
	}
}