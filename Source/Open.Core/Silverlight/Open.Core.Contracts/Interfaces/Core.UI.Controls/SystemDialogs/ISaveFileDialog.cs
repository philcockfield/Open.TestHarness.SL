namespace Open.Core.UI.Controls
{
    /// <summary>Defines the properties for the system SaveFile dialog.</summary>
    public interface ISaveFileDialog : IFileSystemDialog
    {
        /// <summary>Gets or sets the default file name extension applied to files that are saved with the SaveFileDialog.</summary>
        string DefaultExt { get; set; }

        /// <summary>Gets the file name for the selected file associated with the SaveFileDialog.</summary>
        string SafeFileName { get; }
    }
}
