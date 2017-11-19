using System;

namespace ObjectPrinting
{
	public class PrintingPropertyConfig<TOwner, TProperty> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TProperty>
	{
		private readonly PrintingConfig<TOwner> printingConfig;

		public PrintingPropertyConfig(PrintingConfig<TOwner> printingConfig)
		{
			this.printingConfig = printingConfig;
		}

		public PrintingConfig<TOwner> Using(Func<TProperty, string> func)
		{
			// ...
			return printingConfig;
		}

		PrintingConfig<TOwner> ISerializeConfig<TOwner, TProperty>.PrintingConfig => printingConfig;
	}
}