using System.ComponentModel.Composition;
using System.Windows.Controls;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [ExportToolBarView(Id = ExportKey)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ToolBarViewOverride : UserControl, IToolBarView
    {
        public const string ExportKey = "MyOverride";

        /// <summary>Constructor.</summary>
        public ToolBarViewOverride()
        {
            InitializeComponent();
        }

        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IToolBar ViewModel
        {
            get { return DataContext as IToolBar; }
            set { DataContext = value; }
        }
    }
}
