using T = Open.Core.UI.Controls.Models.SaveFileDialog;

namespace Open.Core.UI.Controls.Models
{
    internal class SaveFileDialog : FileSystemDialog, ISaveFileDialog
    {
        public string DefaultExtension
        {
            get { return GetPropertyValue<T, string>(m => m.DefaultExtension); }
            set { SetPropertyValue<T, string>(m => m.DefaultExtension, value); }
        }

        public string SafeFileName
        {
            get { return GetPropertyValue<T, string>(m => m.SafeFileName); }
            set { SetPropertyValue<T, string>(m => m.SafeFileName, value); }
        }
    }
}
