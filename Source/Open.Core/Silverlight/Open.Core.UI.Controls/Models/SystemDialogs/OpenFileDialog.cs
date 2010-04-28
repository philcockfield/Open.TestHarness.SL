using System;
using System.Collections.Generic;
using System.IO;
using Open.Core.Common;

using T = Open.Core.UI.Controls.Models.OpenFileDialog;

namespace Open.Core.UI.Controls.Models
{
    internal class OpenFileDialog : FileSystemDialog, IOpenFileDialog
    {
        public bool MultiSelect
        {
            get { return GetPropertyValue<T, bool>(m => m.MultiSelect); }
            set { SetPropertyValue<T, bool>(m => m.MultiSelect, value); }
        }

        public FileInfo File
        {
            get { return GetPropertyValue<T, FileInfo>(m => m.File); }
            set { SetPropertyValue<T, FileInfo>(m => m.File, value); }
        }

        public IEnumerable<FileInfo> Files
        {
            get { return GetPropertyValue<T, IEnumerable<FileInfo>>(m => m.Files); }
            set { SetPropertyValue<T, IEnumerable<FileInfo>>(m => m.Files, value); }
        }
    }
}
