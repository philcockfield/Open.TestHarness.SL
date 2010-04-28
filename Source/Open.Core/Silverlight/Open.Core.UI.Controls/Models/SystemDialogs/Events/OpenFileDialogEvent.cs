namespace Open.Core.UI.Controls.Models
{
    internal class OpenFileDialogEvent : IOpenFileDialogEvent
    {
        public ITool Tool { get; set; }
        public IOpenFileDialog Dialog { get; set; }
    }
}
