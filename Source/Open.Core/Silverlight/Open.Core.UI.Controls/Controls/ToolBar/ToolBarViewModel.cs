﻿//------------------------------------------------------
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Open.Core.Common;
using T = Open.Core.UI.Controls.ToolBarViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>A laid up structure of tools.</summary>
    [Export(typeof(IToolBar))]
    public class ToolBarViewModel : ToolBase, IToolBar
    {
        #region Events
        /// <summary>Fires when the 'UpdateLayout' method is invoked.</summary>
        public event EventHandler UpdateLayoutRequest;
        private void FireUpdateLayoutRequest(){if (UpdateLayoutRequest != null) UpdateLayoutRequest(this, new EventArgs());}

        /// <summary>Fires when the toolbar is cleared.</summary>
        public event EventHandler Cleared;
        private void FireCleared(){if (Cleared != null) Cleared(this, new EventArgs());}
        #endregion

        #region Head
        internal const string DefaultViewExportKey = "Open.Core.UI.Controls.ToolBarView.Default";
        private readonly List<ToolItem> toolItems = new List<ToolItem>();
        private ToolBarImporter importer;
        private IToolBarTitle defaultTitle;

        public ToolBarViewModel()
        {
            EventBus.Subscribe<UpdateToolbarLayoutEvent>(OnUpdateToolbarLayoutEvent);
        }
        #endregion

        #region Event Handlers
        private void OnTitleIsVisibleChanged(object sender, EventArgs e)
        {
            OnPropertyChanged<T>(m => m.ToolContainerMargin);
        }

        public void OnUpdateToolbarLayoutEvent(UpdateToolbarLayoutEvent e)
        {
            // Update the layout (only if this is the root toolbar).
            if (Parent == null) UpdateLayout();
        }
        #endregion

        #region Properties : IToolBar
        /// <summary>Gets the collection of tools within the toolbar.</summary>
        public IEnumerable<ITool> Tools
        {
            get { return toolItems.Select(m => m.Tool); }
        }

        /// <summary>Gets or sets the key used to import the ToolBar view (via MEF).</summary>
        public object ViewImportKey
        {
            get { return GetPropertyValue<T, object>(m => m.ViewImportKey, DefaultViewExportKey); }
            set { SetPropertyValue<T, object>(m => m.ViewImportKey, value, DefaultViewExportKey); }
        }

        /// <summary>Gets or sets the margin around the toolbar.</summary>
        public Thickness Margin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.Margin, new Thickness(0)); }
            set { SetPropertyValue<T, Thickness>(m => m.Margin, value, new Thickness(0)); }
        }

        /// <summary>Gets or sets the height of the toolbar (NaN by default).</summary>
        public double Height
        {
            get { return GetPropertyValue<T, double>(m => m.Height, double.NaN); }
            set { SetPropertyValue<T, double>(m => m.Height, value, double.NaN); }
        }

        /// <summary>
        ///     Gets or sets the default margin to apply to tools within the toolbar. This is overridden by the tool itself,
        ///     if the CreateView() method yields a control which as a pre-defined Margin value.
        /// </summary>
        public Thickness DefaultToolMargin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.DefaultToolMargin, new Thickness(0)); }
            set { SetPropertyValue<T, Thickness>(m => m.DefaultToolMargin, value, new Thickness(0)); }
        }

        /// <summary>Gets or sets which dividers to display (None | Left | Right.  Top/Bottom values are ignored).</summary>
        public RectEdgeFlag Dividers
        {
            get { return GetPropertyValue<T, RectEdgeFlag>(m => m.Dividers, RectEdgeFlag.None); }
            set
            {
                SetPropertyValue<T, RectEdgeFlag>(
                            m => m.Dividers, value, 
                            RectEdgeFlag.None,
                            m => m.IsLeftDividerVisible,
                            m => m.IsRightDividerVisible);
            }
        }

        /// <summary>Gets or sets the title for the toolbar (guaranteed to return a default object).</summary>
        public IToolBarTitle Title
        {
            get
            {
                var value = GetPropertyValue<T, IToolBarTitle>(m => m.Title);
                return value ?? GetDefaultTitle();
            }
            set { SetPropertyValue<T, IToolBarTitle>(m => m.Title, value); }
        }
        #endregion

        #region Properties : ViewModel
        public bool IsLeftDividerVisible { get { return (Dividers & RectEdgeFlag.Left) == RectEdgeFlag.Left; } }
        public bool IsRightDividerVisible { get { return (Dividers & RectEdgeFlag.Right) == RectEdgeFlag.Right; } }

        public Thickness ToolContainerMargin
        {
            get { return Title.IsVisible ? new Thickness(4, 0, 4, 0) : new Thickness(0); }
        }
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            var viewImport = GetImporter().Views.FirstOrDefault(m => Equals(m.Metadata.Id, ViewImportKey));
            var view = viewImport == null ? null : viewImport.Value as FrameworkElement;
            if (view != null) view.DataContext = this;
            return view;
        }

        /// <summary>Causes the toolbar to re-build it's tool layout.</summary>
        public void UpdateLayout()
        {
            // Update this toolbar.
            FireUpdateLayoutRequest();

            // Update child toolbars.
            var childToolbars = Tools.Where(m => m is IToolBar);
            foreach (IToolBar toolbar in childToolbars)
            {
                toolbar.UpdateLayout();
            }
        }

        /// <summary>Removes all tools.</summary>
        public void Clear()
        {
            if (toolItems.IsEmpty()) return;
            toolItems.Clear();
            FireUpdateLayoutRequest();
            FireCleared();
        }

        /// <summary>Adds a tool to the toolbar.</summary>
        /// <typeparam name="TTool">The type of the tool.</typeparam>
        /// <param name="tool">The instance of the tool model being added.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public void Add<TTool>(TTool tool, int? column = null, int? row = null, int? columnSpan = 1, int? rowSpan = 1) where TTool : ITool
        {
            // Setup initial conditions.
            if (Equals(tool, default(TTool))) throw new ArgumentNullException("tool");
            if (rowSpan < 1) throw new ArgumentOutOfRangeException("rowSpan", "RowSpan's must be 1 or greater.");
            if (columnSpan < 1) throw new ArgumentOutOfRangeException("columnSpan", "ColumnSpan's must be 1 or greater.");

            // Create the new item.
            var item = new ToolItem
                           {
                               Tool = tool,
                               Column = column == null ? toolItems.Count : column.Value,
                               Row = row == null ? 0 : row.Value,
                               ColumnSpan = columnSpan == null ? 1 : columnSpan.Value,
                               RowSpan = rowSpan == null ? 1 : rowSpan.Value
                           };
            toolItems.Add(item);

            // Finish up.
            tool.Parent = this;
        }

        /// <summary>Gets the tool with the specified ID.</summary>
        /// <param name="toolId">The unique identifier of the tool.</param>
        /// <param name="includeChildToolbars">Flag indicating if child toolbars (tool-groups) should be included within the search.</param>
        public ITool GetTool(object toolId, bool includeChildToolbars = true)
        {
            return GetTool(this, toolId, includeChildToolbars);
        }
        private static ITool GetTool(IToolBar toolbar, object toolId, bool includeChildToolbars)
        {
            // Look for a matching tool.
            var tool = toolId == null
                        ? null
                        : toolbar.Tools.Where(m => Equals(m.Id, toolId)).FirstOrDefault();
            if (tool != null) return tool;

            // Search child toolbars (Recursion).
            if (includeChildToolbars)
            {
                var toolGroups = toolbar.Tools.OfType<IToolBar>();
                foreach (var toolGroup in toolGroups)
                {
                    tool = GetTool(toolGroup, toolId, true);
                    if (tool != null) return tool;
                }
            }

            // Finish up.
            return null;
        }

        /// <summary>Gets the tool with the specified ID.</summary>
        /// <typeparam name="TTool">The type of the tool.</typeparam>
        /// <param name="toolId">The unique identifier of the tool.</param>
        public TTool GetTool<TTool>(object toolId) where TTool : ITool
        {
            var tool = GetTool(toolId);
            return tool == null ? default(TTool) : (TTool)tool;
        }

        /// <summary>Gets the column value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        public int GetColumn(ITool tool)
        {
            if (! Tools.Contains(tool)) throw new NotFoundException("The tool has not been added to the ToolBar.");
            return GetItem(tool).Column;
        }

        /// <summary>Gets the row value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        public int GetRow(ITool tool)
        {
            if (!Tools.Contains(tool)) throw new NotFoundException("The tool has not been added to the ToolBar.");
            return GetItem(tool).Row;
        }

        /// <summary>Gets the column-span value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        public int GetColumnSpan(ITool tool)
        {
            if (!Tools.Contains(tool)) throw new NotFoundException("The tool has not been added to the ToolBar.");
            return GetItem(tool).ColumnSpan;
        }

        /// <summary>Gets the row-span value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        public int GetRowSpan(ITool tool)
        {
            if (!Tools.Contains(tool)) throw new NotFoundException("The tool has not been added to the ToolBar.");
            return GetItem(tool).RowSpan;
        }
        #endregion

        #region Internal
        private ToolItem GetItem(ITool tool)
        {
            return toolItems.FirstOrDefault(m => m.Tool == tool);
        }

        private ToolBarImporter GetImporter() { return importer ?? (importer = new ToolBarImporter()); }
        private IToolBarTitle GetDefaultTitle()
        {
            return defaultTitle ?? (defaultTitle = CreateTitle());
        }
        private IToolBarTitle CreateTitle()
        {
            var title = GetImporter().TitleFactory.CreateExport().Value;
            title.IsVisible = false;
            WireTitleEvents(title);
            return title;
        }

        private void WireTitleEvents(IToolBarTitle title)
        {
            title.IsVisibleChanged -= OnTitleIsVisibleChanged;
            title.IsVisibleChanged += OnTitleIsVisibleChanged;
        }
       #endregion

        private class ToolItem
        {
            public ITool Tool { get; set; }
            public int Column { get; set; }
            public int Row { get; set; }
            public int ColumnSpan { get; set; }
            public int RowSpan { get; set; }
        }

        public class ToolBarImporter : ImporterBase
        {
            [ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
            public IEnumerable<Lazy<IToolBarView, IIdentifiable>> Views { get; set; }

            [Import]
            public ExportFactory<IToolBarTitle> TitleFactory { get; set; }
        }
    }
}