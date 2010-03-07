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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ToolBarModel;

namespace Open.Core.UI.Controls
{
    /// <summary>A laid up structure of tools.</summary>
    [Export(typeof(IToolBar))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ToolBarModel : ToolBase, IToolBar
    {
        #region Head
        internal const string DefaultViewExportKey = "Open.Core.UI.Controls.ToolBarView.Default";
        private readonly List<ToolItem> toolItems = new List<ToolItem>();
        private Importer importer;
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            if (importer == null) importer = new Importer();
            var viewImport = importer.Views.FirstOrDefault(m => Equals(m.Metadata.Key, ViewImportKey));
            var view = viewImport == null ? null : viewImport.Value as FrameworkElement;
            if (view != null) view.DataContext = this;
            return view;
        }

        /// <summary>Adds a tool to the toolbar.</summary>
        /// <typeparam name="TTool">The type of the tool.</typeparam>
        /// <param name="tool">The instance of the tool model being added.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public void Add<TTool>(TTool tool, int? column = null, int? row = null, int columnSpan = 1, int rowSpan = 1) where TTool : ITool
        {
            // Setup initial conditions.
            if (Equals(tool, default(TTool))) throw new ArgumentNullException("tool");
            if (rowSpan < 1) throw new ArgumentOutOfRangeException("rowSpan", "RowSpan's must be 1 or greater.");
            if (columnSpan < 1) throw new ArgumentOutOfRangeException("columnSpan", "ColumnSpan's must be 1 or greater.");

            // Determine the column.
            if (column == null) column = toolItems.Count;
            if (row == null) row = 0;

            // Create the new item.
            var item = new ToolItem
                           {
                               Tool = tool,
                               Column = column.Value,
                               Row = row.Value,
                               ColumnSpan = columnSpan,
                               RowSpan = rowSpan
                           };
            toolItems.Add(item);
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
        #endregion

        private class ToolItem
        {
            public ITool Tool { get; set; }
            public int Column { get; set; }
            public int Row { get; set; }
            public int ColumnSpan { get; set; }
            public int RowSpan { get; set; }
        }

        public class Importer : ImporterBase
        {
            [ImportMany]
            public IEnumerable<Lazy<IToolBarView, IIdentifiable>> Views { get; set; }
        }
    }
}