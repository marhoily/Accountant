using NewModel.Shared.Annotations;

namespace NewModel.Shared.ModelReflection
{
	[DataOnlyObject]
	public sealed class ModelEntry
	{
        public bool HasKey { get; set; }
		public string Name { get; set; }
        public ModelProp[] Properties { get; set; }
	}
}