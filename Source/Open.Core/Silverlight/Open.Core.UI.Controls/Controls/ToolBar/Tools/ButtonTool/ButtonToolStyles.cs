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

using T = Open.Core.UI.Controls.ButtonToolStyles;

namespace Open.Core.UI.Controls
{
    /// <summary>The default styles elements for the ButtonTool.</summary>
    public class ButtonToolStyles : ModelBase, IButtonToolStyles
    {
        #region Properties
        public DataTemplate BackgroundDefault
        {
            get { return GetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundDefault); }
            set { SetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundDefault, value); }
        }

        public DataTemplate BackgroundOver
        {
            get { return GetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundOver); }
            set { SetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundOver, value); }
        }

        public DataTemplate BackgroundDown
        {
            get { return GetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundDown); }
            set { SetPropertyValue<ButtonToolStyles, DataTemplate>(m => m.BackgroundDown, value); }
        }
        #endregion

        #region Methods
        public void SetDefaults()
        {
            BackgroundDefault = GetTemplate("ButtonTool.Background.Default");
            BackgroundOver = GetTemplate("ButtonTool.Background.Over");
            BackgroundDown = GetTemplate("ButtonTool.Background.Down");
        }
        #endregion

        #region Internal
        private static DataTemplate GetTemplate(string key)
        {
            return Templates.Instance.GetDataTemplate(key);
        }
        #endregion
    }
}
