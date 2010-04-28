namespace Open.Core.UI.Controls.Models
{
    internal class SaveFileDialogEvent : ISaveFileDialogEvent
    {
        public ITool Tool { get; set; }
        public ISaveFileDialog Dialog { get; set; }
    }
}
