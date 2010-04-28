using Open.Core.Common;

using T = Open.Core.UI.Controls.Models.FileSystemDialog;

namespace Open.Core.UI.Controls.Models
{
    internal abstract class FileSystemDialog : NotifyPropertyChangedBase, IFileSystemDialog
    {
        public string Filter
        {
            get { return GetPropertyValue<T, string>(m => m.Filter); }
            set { SetPropertyValue<T, string>(m => m.Filter, value); }
        }

        public int FilterIndex
        {
            get { return GetPropertyValue<T, int>(m => m.FilterIndex, 1); }
            set { SetPropertyValue<T, int>(m => m.FilterIndex, value.WithinBounds(1, int.MaxValue), 1); }
        }
    }
}
