namespace Open.Core.UI.Controls.Models
{
    internal class SaveFileDialogEvent : ToolEvent, ISaveFileDialogEvent
    {
        public ISaveFileDialog Dialog { get; set; }
    }
}
