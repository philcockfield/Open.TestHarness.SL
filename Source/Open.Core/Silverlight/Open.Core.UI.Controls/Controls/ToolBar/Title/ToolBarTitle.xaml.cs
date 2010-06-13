using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    public partial class ToolBarTitle : UserControl
    {
        /// <summary>Constructor.</summary>
        public ToolBarTitle()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ToolBarTitleViewModel ViewModel
        {
            get { return DataContext as ToolBarTitleViewModel; }
            set { DataContext = value; }
        }
    }
}