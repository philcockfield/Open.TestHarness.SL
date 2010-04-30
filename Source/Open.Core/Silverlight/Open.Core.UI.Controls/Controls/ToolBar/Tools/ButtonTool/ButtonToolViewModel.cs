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
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ButtonToolViewModel;

namespace Open.Core.UI.Controls
{
    public class ButtonToolViewModel : ViewModelBase
    {
        #region Head
        private readonly PropertyObserver<ButtonTool> modelObserver;
        private bool isMouseOver;
        private bool isMouseDown;

        public ButtonToolViewModel(ButtonTool model)
        {
            // Setup initial conditions.
            if (model == null) throw new ArgumentNullException("model");

            // Store values.
            Model = model;

            // Wire up events.
            model.PropertyChanged += delegate { FireChanged(); };
            modelObserver = new PropertyObserver<ButtonTool>(model)
                        .RegisterHandler(m => m.IsEnabled, FireIsEnabledChanged);
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            modelObserver.Dispose();
        }
        #endregion

        #region Event Handlers
        public void OnMouseEnter()
        {
            isMouseOver = true;
            UpdateMouseState();
        }

        public void OnMouseLeave()
        {
            isMouseOver = false;
            UpdateMouseState();
        }

        public void OnMouseDown()
        {
            isMouseDown = true;
            UpdateMouseState();
        }

        public void OnMouseUp()
        {
            var wasMouseDown = isMouseDown;
            isMouseDown = false;
            UpdateMouseState();
            if (isMouseOver && wasMouseDown) Model.FireClick();
        }
        #endregion

        #region Properties
        public ButtonTool Model { get; private set; }
        public bool IsDropDownPointerVisible { get { return Model.ButtonType == ButtonToolType.Split || Model.ButtonType == ButtonToolType.DropDown; } }
        public bool IsEnabled { get { return Model.IsEnabled && IsViewEnabled; } }
        public double EnabledOpacity { get { return IsEnabled ? 1 : 0.3; } }
        public bool IsViewEnabled
        {
            get { return GetPropertyValue<ButtonToolViewModel, bool>(m => m.IsViewEnabled, true); }
            set
            {
                SetPropertyValue<ButtonToolViewModel, bool>(m => m.IsViewEnabled, value, true);
                FireIsEnabledChanged();
            }
        }
        public bool IsIconVisible { get { return Model.Icon != null; } }
        public bool IsTextVisible { get { return Model.Text.AsNullWhenEmpty() != null; } }
        #endregion

        #region Properties - Templates
        public DataTemplate ButtonStructureTemplate
        {
            get
            {
                var name = string.Format("ButtonTool.{0}.{1}", Model.ButtonType, Model.Orientation);
                var template = GetTemplate(name);
                if (template == null) throw new NotFoundException(name);
                return template;
            }
        }

        public DataTemplate BackgroundTemplate
        {
            get
            {
                if (Model.IsMouseDown) return Model.Styles.BackgroundDown;
                if (Model.IsMouseOver) return Model.Styles.BackgroundOver;
                if (Model.IsDefaultBackgroundVisible) return Model.Styles.BackgroundDefault;
                return null;
            }
        }
        #endregion

        #region Internal
        private void FireChanged()
        {
            OnPropertyChanged<ButtonToolViewModel>(
                            m => m.ButtonStructureTemplate,
                            m => m.BackgroundTemplate,
                            m => m.IsIconVisible,
                            m => m.IsTextVisible,
                            m => m.IsDropDownPointerVisible
                            );
        }

        private void FireIsEnabledChanged()
        {
            OnPropertyChanged<ButtonToolViewModel>(m => m.IsEnabled, m => m.EnabledOpacity);
        }

        private static DataTemplate GetTemplate(string key)
        {
            return Templates.Instance.GetDataTemplate(key);
        }

        private void UpdateMouseState()
        {
            if (! IsEnabled)
            {
                Model.MouseState = ButtonMouseState.Default;
            }
            else if (isMouseOver && isMouseDown)
            {
                Model.MouseState = ButtonMouseState.Pressed;
            }
            else if (isMouseOver)
            {
                Model.MouseState = ButtonMouseState.MouseOver;
            }
            else
            {
                Model.MouseState = ButtonMouseState.Default;
            }
        }
        #endregion
    }
}