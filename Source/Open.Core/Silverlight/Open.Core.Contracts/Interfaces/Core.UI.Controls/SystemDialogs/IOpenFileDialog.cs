using System.Collections.Generic;
using System.IO;

namespace Open.Core.UI.Controls
{
    /// <summary>Defines the properties for the system OpenFile dialog.</summary>
    public interface IOpenFileDialog : IFileSystemDialog
    {
        /// <summary>Gets or sets a value that indicates whether the OpenFileDialog allows users to select multiple files.</summary>
        bool MultiSelect { get; set; }

        /// <summary>Gets a FileInfo object for the selected file. If multiple files are selected, returns the first selected file.</summary>
        FileInfo File { get; set; }

        /// <summary>Gets a collection of FileInfo objects for the selected files.</summary>
        IEnumerable<FileInfo> Files { get; set; }
    }
}
