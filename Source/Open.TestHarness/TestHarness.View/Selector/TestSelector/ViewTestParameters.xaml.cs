using System.Windows.Controls;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Renders editable parameters for a view-test.</summary>
    public partial class ViewTestParameters : UserControl
    {
        /// <summary>Constructor.</summary>
        public ViewTestParameters()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ViewTestParametersViewModel ViewModel
        {
            get { return DataContext as ViewTestParametersViewModel; }
            set { DataContext = value; }
        }

    }
}