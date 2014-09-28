using NewModel.Shared.Annotations;

namespace NewModel.Shared.ModelReflection
{
    [DataOnlyObject]
    public sealed class ModelProp
    {
        public bool IsNavigation { get; set; }
        public string Definition { get; set; }
        public string Initializer { get; set; }
        public string Finalizer { get; set; }
//        public ModelEntry Subtype { get; set; }
    }
}