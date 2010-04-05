using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    internal class ToolLayoutController : DisposableBase
    {
        #region Head
        private Grid toolContainer;

        public ToolLayoutController(Grid toolContainer)
        {
            this.toolContainer = toolContainer;
        }

        protected override void OnDisposed()
        {
            Clear();
            toolContainer = null;
            base.OnDisposed();
        }
        #endregion

        #region Methods
        public void Clear()
        {
            toolContainer.Children.Clear();
            toolContainer.ColumnDefinitions.Clear();
            toolContainer.RowDefinitions.Clear();
        }

        public void LayupTools(IToolBar model)
        {
            // Setup initial conditions.
            Clear();
            if (model == null || model.Tools.IsEmpty()) return;

            // Add row/column definitions.
            AddRowDefinitions(model);
            AddColumnDefinitions(model);

            // Insert tools.
            foreach (var tool in model.Tools)
            {
                InsertTool(tool);
            }
        }
        #endregion

        #region Internal
        private void InsertTool(ITool tool)
        {
            // Create the view.
            var view = tool.CreateView();
            if (view.DataContext == null) view.DataContext = tool;
            view.HorizontalAlignment = HorizontalAlignment.Left;
            view.VerticalAlignment = VerticalAlignment.Top;

            // Assign the margin (if the generated view did not arrive with an explicitly set value).
            if (view.Margin == default(Thickness)) view.Margin = tool.Parent.DefaultToolMargin;

            // Assign Row/Column position.
            Grid.SetRow(view, tool.Parent.GetRow(tool));
            Grid.SetColumn(view, tool.Parent.GetColumn(tool));

            // Set spans.
            Grid.SetRowSpan(view, tool.Parent.GetRowSpan(tool));
            Grid.SetColumnSpan(view, tool.Parent.GetColumnSpan(tool));

            // Insert into the visual tree.
            toolContainer.Children.Add(view);
        }

        private void AddRowDefinitions(IToolBar model)
        {
            var total = model.Tools.Max(m => model.GetRow(m));
            for (var i = 0; i <= total; i++)
            {
                toolContainer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
        }

        private void AddColumnDefinitions(IToolBar model)
        {
            var total = model.Tools.Max(m => model.GetColumn(m));
            for (var i = 0; i <= total; i++)
            {
                toolContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
        }
        #endregion
    }
}
