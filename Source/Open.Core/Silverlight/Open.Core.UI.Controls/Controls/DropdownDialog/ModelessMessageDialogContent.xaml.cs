using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    public partial class ModelessMessageDialogContent : UserControl
    {
        /// <summary>Constructor.</summary>
        public ModelessMessageDialogContent()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ModelessMessageDialogContentViewModel ViewModel
        {
            get { return DataContext as ModelessMessageDialogContentViewModel; }
            set { DataContext = value; }
        }
    }
}