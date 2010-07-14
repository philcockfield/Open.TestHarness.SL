using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    public partial class PromptButtonBar : UserControl
    {
        /// <summary>Constructor.</summary>
        public PromptButtonBar()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public PromptButtonBarViewModel ViewModel
        {
            get { return DataContext as PromptButtonBarViewModel; }
            set { DataContext = value; }
        }
    }
}