using System;

namespace ObjectPrinting
{
	public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TType>
	{
		private PrintingConfig<TOwner> printingConfig;

		public SerializeConfig(PrintingConfig<TOwner> printingConfig)
		{
			this.printingConfig = printingConfig;
		}

		public PrintingConfig<TOwner> Using(Func<TType, string> serializer)
		{
			printingConfig.speciallySerializedTypes[typeof(TType)] = serializer;
			return printingConfig;
		}

		PrintingConfig<TOwner> ISerializeConfig<TOwner, TType>.PrintingConfig => printingConfig;
	}
}
