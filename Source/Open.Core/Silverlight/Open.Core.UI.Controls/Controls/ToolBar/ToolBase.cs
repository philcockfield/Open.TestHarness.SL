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

using System.Windows;
using Open.Core.Common;
using T = Open.Core.UI.Controls.ToolBase;

namespace Open.Core.UI.Controls
{
    /// <summary>The base class for tools.</summary>
    public abstract class ToolBase : ModelBase, ITool
    {
        #region Properties
        /// <summary>Gets the toolbar that this tool resides within (null if not added to a toolbar, or is a root element).</summary>
        public IToolBar Parent
        {
            get { return GetPropertyValue<T, IToolBar>(m => m.Parent); }
            set { SetPropertyValue<T, IToolBar>(m => m.Parent, value); }
        }
        #endregion

        #region Methods
        /// <summary>Creates a new instance of the tool's view.</summary>
        public virtual FrameworkElement CreateView()
        {
            return null;
        }
        #endregion
    }
}