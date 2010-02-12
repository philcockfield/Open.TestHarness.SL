using System.Windows.Controls;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Displays an individual view-test as a button.</summary>
    public partial class ViewTestButton : UserControl
    {
        /// <summary>Constructor.</summary>
        public ViewTestButton()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ViewTestButtonViewModel ViewModel
        {
            get { return DataContext as ViewTestButtonViewModel; }
            set { DataContext = value; }
        }
    }
}