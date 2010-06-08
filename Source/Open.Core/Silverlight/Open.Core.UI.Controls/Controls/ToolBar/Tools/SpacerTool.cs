using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using T = Open.Core.UI.Controls.SpacerTool;

namespace Open.Core.UI.Controls
{
    /// <summary>A tool that inserts white space into a toolbar.</summary>
    [Export(typeof(ISpacerTool))]
    public class SpacerTool : ToolBase, ISpacerTool
    {
        #region Head
        private static readonly GridLength defaultGridLength = new GridLength(1, GridUnitType.Star);
        #endregion

        #region Properties
        public GridLength ColumnWidth
        {
            get { return GetPropertyValue<T, GridLength>(m => m.ColumnWidth, defaultGridLength); }
            set { SetPropertyValue<T, GridLength>(m => m.ColumnWidth, value, defaultGridLength); }
        }
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            return new Border { DataContext = this, IsHitTestVisible = false };
        }
        #endregion
    }
}
