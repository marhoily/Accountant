using System;

using NewModel.Shared.Annotations.ReSharper;

namespace NewModel.UnitTests.Shared.FileSystem._Base_
{
	[Serializable]
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)] // By serializer
	public sealed class Sample : ISample
	{
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public Sample OtherSample { get; set; }
	}
}