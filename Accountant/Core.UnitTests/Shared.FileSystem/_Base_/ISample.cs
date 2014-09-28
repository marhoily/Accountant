using System;

using NewModel.Shared.Annotations.ReSharper;

namespace NewModel.UnitTests.Shared.FileSystem._Base_
{
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)] // By serializer
	public interface ISample
	{
		string Name { get; }
		DateTime Date { get; }
		Sample OtherSample { get; }
	}
}