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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public bool IsVisible
        {
            get { return Property.GetValue<T, bool>(m => m.IsVisible, true); }
            set { Property.SetValue<T, bool>(m => m.IsVisible, value, true); }
        }

        public Thickness Margin
        {
            get { return Property.GetValue<T, Thickness>(m => m.Margin); }
            set { Property.SetValue<T, Thickness>(m => m.Margin, value); }
        }

        public string Text
        {
            get { return GetPropertyValue<T, string>(m => m.Text, defaultButtonText); }
            set { SetPropertyValue<T, string>(m => m.Text, value, defaultButtonText); }
        }

        public string ToolTip
        {
            get { return Property.GetValue<T, string>(m => m.ToolTip); }
            set { Property.SetValue<T, string>(m => m.ToolTip, value); }
        }

        public DataTemplate Template
        {
            get
            {
                return 
                        GetPropertyValue<T, DataTemplate>(m => m.Template) 
                        ?? ButtonTemplates.ButtonModelDefault;
            }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value); }
        }

        public object Tag
        {
            get { return Property.GetValue<T, object>(m => m.Tag); }
            set { Property.SetValue<T, object>(m => m.Tag, value); }
        }
        #endregion

        #region Methods
        public void InvokeClick(bool force = false)
        {
            if (Command.CanExecute(null) || force) Command.Execute(null);
        }

        public FrameworkElement CreateView()
        {
            return new ContentControl
                              {
                                  DataContext = this,
                                  ContentTemplate = Template,
                                  HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                  VerticalContentAlignment = VerticalAlignment.Stretch,
                                  Content = new ContentPresenter(),
                                  IsTabStop = false,
                              };
        }
        #endregion
    }
}
