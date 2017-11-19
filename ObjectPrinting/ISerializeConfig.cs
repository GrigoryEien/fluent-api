namespace ObjectPrinting
{
	public interface ISerializeConfig<TOwner, TPropType>
	{
		PrintingConfig<TOwner> PrintingConfig { get; }
	}
}