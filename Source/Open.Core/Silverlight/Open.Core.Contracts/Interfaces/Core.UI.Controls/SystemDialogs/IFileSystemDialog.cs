namespace Open.Core.UI.Controls
{
    /// <summary>Defines the common properties for the system dialogs.</summary>
    public interface IFileSystemDialog
    {
        /// <summary>Gets or sets a filter string that specifies the files types and descriptions to display in the dialog.</summary>
        string Filter { get; set; }

        /// <summary>Gets or sets the index of the selected item in the Save as type drop-down list.</summary>
        int FilterIndex { get; set; }
    }
}
