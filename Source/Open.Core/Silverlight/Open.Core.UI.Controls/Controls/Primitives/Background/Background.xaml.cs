using System.Windows;
using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    public partial class Background : UserControl
    {
        /// <summary>Constructor.</summary>
        public Background()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public BackgroundModel ViewModel
        {
            get { return DataContext as BackgroundModel; }
            set { DataContext = value; }
        }
    }
}