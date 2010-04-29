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

using System.ComponentModel.Composition;
using System.Windows;
using T = Open.Core.UI.Controls.ToolDivider;

namespace Open.Core.UI.Controls
{
    /// <summary>A divider of tools within a toolbar.</summary>
    [Export(typeof(IToolDivider))]
    public class ToolDivider : ToolBase, IToolDivider
    {
        #region Head
        private static readonly DataTemplate defaultTemplate = Templates.Instance.GetDataTemplate("ToolDivider.Default");

        public ToolDivider()
        {
            // Set default values.
            VerticalAlignment = VerticalAlignment.Stretch;
            MinWidth = 9;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the template that renders the divider visuals.</summary>
        public DataTemplate Template
        {
            get { return GetPropertyValue<T, DataTemplate>(m => m.Template, defaultTemplate); }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value, defaultTemplate); }
        }
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            return new ToolDividerView { ViewModel = this };
        }
        #endregion
    }
}
