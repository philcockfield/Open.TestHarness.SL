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
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.Composite.Command;

using T = Open.Core.UI.Controls.ButtonModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IButton))]
    public class ButtonModel : ModelBase, IButton
    {
        #region Events
        /// <summary>Fires when the button is clicked.</summary>
        public event EventHandler Click;
        private void FireClick(){if (Click != null) Click(this, new EventArgs());}
        #endregion

        #region Head
        private const string defaultButtonText = "Untitled";
        private DelegateCommand<Button> command;

        public ButtonModel()
        {
        }

        #endregion

        #region Event Handlers
        private void OnClick()
        {
            FireClick();
        }
        #endregion

        #region Properties
        public ICommand Command
        {
            get { return command ?? (command = new DelegateCommand<Button>(b => OnClick(), b => IsEnabled)); }
        }

        public bool IsEnabled
        {
            get { return GetPropertyValue<T, bool>(m => m.IsEnabled, true); }
            set
            {
                SetPropertyValue<T, bool>(m => m.IsEnabled, value, true);
                if (command != null) command.RaiseCanExecuteChanged();
            }
        }

        public string Label
        {
            get { return GetPropertyValue<T, string>(m => m.Label, defaultButtonText); }
            set { SetPropertyValue<T, string>(m => m.Label, value, defaultButtonText); }
        }

        public DataTemplate Template
        {
            get
            {
                return GetPropertyValue<T, DataTemplate>(m => m.Template) ?? ButtonTemplates.ButtonModelDefault;
            }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value); }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new ContentControl
                              {
                                  DataContext = this,
                                  ContentTemplate = Template,
                                  HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                  VerticalContentAlignment = VerticalAlignment.Stretch,
                                  Content = new ContentPresenter()
                              };
        }
        #endregion
    }
}
