using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.ModelessMessageDialogContentViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IModelessMessageDialogContent))]
    public class ModelessMessageDialogContentViewModel : ViewModelBase, IModelessMessageDialogContent
    {
        #region Head
        private const double DefaultHeight = 50;        
        #endregion

        #region Properties
        public double Height
        {
            get { return Property.GetValue<T, double>(m => m.Height, DefaultHeight); }
            set { Property.SetValue<T, double>(m => m.Height, value, DefaultHeight); }
        }

        public IDropdownDialog Dialog
        {
            get { return Property.GetValue<T, IDropdownDialog>(m => m.Dialog); }
            private set { Property.SetValue<T, IDropdownDialog>(m => m.Dialog, value); }
        }

        public string Title
        {
            get { return Property.GetValue<T, string>(m => m.Title); }
            set { Property.SetValue<T, string>(m => m.Title, value); }
        }

        public string Message
        {
            get { return Property.GetValue<T, string>(m => m.Message); }
            set { Property.SetValue<T, string>(m => m.Message, value); }
        }
        #endregion

        #region Methods
        public void Initialize(IDropdownDialog dialog)
        {
            // Setup initial conditions.
            if (dialog == null) throw new ArgumentNullException("dialog");
            Dialog = dialog;

            // Update the dialog settings.
            Dialog.SizeMode = DialogSize.StretchHorizontal;
            Dialog.Margin = new Thickness(0);
            Dialog.Padding = new Thickness(0);
            Dialog.ButtonBar.IsVisible = false;
            Dialog.Background.Color = Color.FromArgb(255, 250, 231, 147).ToBrush();
            Dialog.Buttons.Configuration = PromptButtonConfiguration.YesNo;
            Dialog.Mask.Color = Colors.Transparent.ToBrush();
        }

        public FrameworkElement CreateView()
        {
            return new ModelessMessageDialogContent { ViewModel = this };
        }
        #endregion
    }
}
