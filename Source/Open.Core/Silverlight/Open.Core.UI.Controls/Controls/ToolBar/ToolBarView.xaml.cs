//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.Linq;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Renders a toolbar.</summary>
    [ExportToolBarView(Key = ToolBarModel.DefaultViewExportKey)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ToolBarView : UserControl, IToolBarView
    {
        #region Head
        private DataContextObserver dataContextObserver;
        private IToolBar viewModel;

        /// <summary>Constructor.</summary>
        public ToolBarView()
        {
            InitializeComponent();
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged( )
        {
            // Unwire old view-model.
            if (viewModel != null) viewModel.UpdateLayoutRequest += OnUpdateLayout;

            // Wire up events.
            viewModel = ViewModel;
            if (viewModel != null) viewModel.UpdateLayoutRequest += OnUpdateLayout;
        }

        private void OnUpdateLayout(object sender, EventArgs e)
        {
            ClearToolBar();
            if (ViewModel != null) BuildToolBar(ViewModel);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IToolBar ViewModel
        {
            get { return DataContext as IToolBar; }
            set { DataContext = value; }
        }
        #endregion

        #region Internal
        private void ClearToolBar()
        {
            toolContainer.Children.Clear();
            toolContainer.ColumnDefinitions.Clear();
            toolContainer.RowDefinitions.Clear();
        }

        private void BuildToolBar(IToolBar model)
        {
            // Setup initial conditions.
            if (model.Tools.IsEmpty()) return;

            // Add row/column definitions.
            AddRowDefinitions(model);
            AddColumnDefinitions(model);

            // Insert tools.
            foreach (var tool in model.Tools)
            {
                InsertTool(tool);
            }
        }

        private void InsertTool(ITool tool)
        {
            // Create the view.
            var view = tool.CreateView();
            view.DataContext = tool;
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