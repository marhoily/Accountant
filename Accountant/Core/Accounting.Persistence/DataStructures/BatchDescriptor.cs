using NewModel.Accounting.Calculation;
using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;
using NewModel.Shared.FileSystem;

namespace NewModel.Accounting.Persistence
{
    [DataOnlyObject]
    public sealed class BatchDescriptor
    {
        public Interval ReferenceInterval { get; set; }
        [NotMapped]
        public IFile File { get; private set; }
    }
}