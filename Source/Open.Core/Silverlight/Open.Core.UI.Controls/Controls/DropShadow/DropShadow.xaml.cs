using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    /// <summary>A simple horizontal drop-shadow.</summary>
    public partial class DropShadow : UserControl
    {
        /// <summary>Constructor.</summary>
        public DropShadow()
        {
            InitializeComponent();
            ViewModel = new DropShadowViewModel();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IDropShadow ViewModel
        {
            get { return DataContext as IDropShadow; }
            set { DataContext = value; }
        }
    }
}