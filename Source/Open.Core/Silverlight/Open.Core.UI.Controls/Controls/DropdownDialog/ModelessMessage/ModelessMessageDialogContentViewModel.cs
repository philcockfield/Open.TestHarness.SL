using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.ModelessMessageDialogContentViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IModelessMessageDialogContent))]
    public class ModelessMessageDialogContentViewModel : ViewModelBase, IModelessMessageDialogContent
    {
        #region Head
        private const double DefaultHeight = 43;

        /// <summary>Constructor.</summary>
        public ModelessMessageDialogContentViewModel()
        {
            // Setup default values.
            // TODO : Shadow not showing - it doesn't animate well.  Need to hide when animating.
            DropShadow = new DropShadowViewModel
                             {
                                 Direction = Direction.Down,
                                 Size = 6,
                                 Opacity = 0.15,
                                 Color = Colors.Black,
                             };
            Background = new BackgroundModel
                             {
                                 Color = Color.FromArgb(255, 250, 231, 147).ToBrush(),
                                 Border = { Color = Colors.Black.ToBrush(), Thickness = new Thickness(0, 0, 0, 1), Opacity = 0.3 }
                             };
            Icon = new CoreImageViewModel
                           {
                               Margin = new Thickness(0, 0, 8, 0)
                           };
        }
        #endregion

        #region Properties : IModelessMessageDialogContent
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

        public string Text
        {
            get { return Property.GetValue<T, string>(m => m.Text); }
            set { Property.SetValue<T, string>(m => m.Text, value); }
        }

        public IImage Icon { get; private set; }

        public IDropShadow DropShadow { get; private set; }
        public IBackground Background { get; private set; }
        #endregion

        #region Properties : ViewModel
        public bool IsIconVisible { get { return Icon != null; } }
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
            Dialog.Background.Color = Colors.Transparent.ToBrush();
            Dialog.Background.Border.Thickness = new Thickness(0);
            Dialog.Buttons.Configuration = PromptButtonConfiguration.YesNo;
            Dialog.Mask.Color = Colors.Transparent.ToBrush();
            Dialog.DropShadowOpacity = 0;
        }

        public FrameworkElement CreateView()
        {
            return new ModelessMessageDialogContent { ViewModel = this };
        }
        #endregion
    }
}
