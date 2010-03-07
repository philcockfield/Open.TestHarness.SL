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

using System.Collections.Generic;

namespace Open.Core.UI.Controls
{
    /// <summary>A laid up structure of tools.</summary>
    public interface IToolBar : ITool
    {
        /// <summary>Gets the collection of tools within the toolbar.</summary>
        IEnumerable<ITool> Tools { get; }

        /// <summary>Gets or sets the key used to import the ToolBar view (via MEF).</summary>
        object ViewImportKey { get; set; }

        /// <summary>Adds a tool to the toolbar.</summary>
        /// <typeparam name="T">The type of the tool.</typeparam>
        /// <param name="tool">The instance of the tool model being added.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        void Add<T>(
                    T tool,
                    int? column = null,
                    int? row = null,
                    int columnSpan = 0,
                    int rowSpan = 0) where T : ITool;

        /// <summary>Gets the column value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        int GetColumn(ITool tool);

        /// <summary>Gets the row value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        int GetRow(ITool tool);

        /// <summary>Gets the column-span value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        int GetColumnSpan(ITool tool);

        /// <summary>Gets the row-span value for the given tool.</summary>
        /// <param name="tool">The tool to look up.</param>
        /// <exception cref="NotFoundException">If the given tool has not been added to the toolbar.</exception>
        int GetRowSpan(ITool tool);
    }
}
