namespace Open.Core.UI.Controls.Models
{
    internal class OpenFileDialogEvent : ToolEvent, IOpenFileDialogEvent
    {
        public IOpenFileDialog Dialog { get; set; }
    }
}
