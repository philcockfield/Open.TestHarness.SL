using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    public partial class ToolDividerView : UserControl
    {
        /// <summary>Constructor.</summary>
        public ToolDividerView()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IToolDivider ViewModel
        {
            get { return DataContext as IToolDivider; }
            set { DataContext = value; }
        }
    }
}