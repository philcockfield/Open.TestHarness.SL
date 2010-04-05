using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls.Controls.ToolBar
{
    public partial class ButtonToolView : UserControl
    {
        #region Head
        /// <summary>Constructor.</summary>
        public ButtonToolView()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            IsEnabledChanged += delegate { SyncIsEnabled(); };
            Loaded += delegate { SyncIsEnabled(); };
            MouseEnter += delegate { if (ViewModel != null) ViewModel.OnMouseEnter(); };
            MouseLeave += delegate { if (ViewModel != null) ViewModel.OnMouseLeave(); };
            MouseLeftButtonDown += delegate
                                       {
                                           CaptureMouse();
                                           if (ViewModel != null) ViewModel.OnMouseDown();
                                       };
            MouseLeftButtonUp += delegate
                                     {
                                         ReleaseMouseCapture();
                                         if (ViewModel != null) ViewModel.OnMouseUp();
                                     };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ButtonToolViewModel ViewModel
        {
            get { return DataContext as ButtonToolViewModel; }
            set { DataContext = value; }
        }
        #endregion

        #region Internal
        private void SyncIsEnabled( )
        {
            if (ViewModel != null) ViewModel.IsViewEnabled = IsEnabled;
        }
        #endregion
    }
}
