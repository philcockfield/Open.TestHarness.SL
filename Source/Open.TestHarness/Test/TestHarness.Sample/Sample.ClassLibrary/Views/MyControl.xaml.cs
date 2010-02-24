using System.Windows.Controls;

namespace Sample.ClassLibrary.Views
{
    /// <summary>A Sample control to demonstrate the TestHarness.</summary>
    public partial class MyControl : UserControl
    {
        /// <summary>Constructor.</summary>
        public MyControl()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public MyControlViewModel ViewModel
        {
            get { return DataContext as MyControlViewModel; }
            set { DataContext = value; }
        }
    }
}
