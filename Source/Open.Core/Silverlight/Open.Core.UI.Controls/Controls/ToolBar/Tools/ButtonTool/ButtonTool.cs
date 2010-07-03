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
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.UI.Controls.Models;
using OpenFileDialog = System.Windows.Controls.OpenFileDialog;
using SaveFileDialog = System.Windows.Controls.SaveFileDialog;
using T = Open.Core.UI.Controls.ButtonTool;

namespace Open.Core.UI.Controls
{
    /// <summary>A ToolBar tool which behaves like a button.</summary>
    [Export(typeof(IButtonTool))]
    public class ButtonTool : ToolBase, IButtonTool
    {
        #region Events
        /// <summary>Fires when the button is clicked.</summary>
        public event EventHandler Click;
        #endregion

        #region Head
        private readonly ButtonToolViewModel viewModel;
        private DialogInvoker<ISaveFileDialog> saveFileDialog;
        private DialogInvoker<IOpenFileDialog> openFileDialog;

        /// <summary>Constructor.</summary>
        public ButtonTool()
        {
            viewModel = new ButtonToolViewModel(this);
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            viewModel.Dispose();
        }
        #endregion

        #region Properties - IButtonTool
        /// <summary>Gets or sets the orentation of the label relative to the icon.</summary>
        public Orientation Orientation
        {
            get { return GetPropertyValue<ButtonTool, Orientation>(m => m.Orientation, Orientation.Horizontal); }
            set { SetPropertyValue<ButtonTool, Orientation>(m => m.Orientation, value, Orientation.Horizontal); }
        }

        /// <summary>Gets or sets the XAML elements that make up the tool.</summary>
        public IButtonToolStyles Styles
        {
            get
            {
                var value = GetPropertyValue<ButtonTool, IButtonToolStyles>(m => m.Styles);
                if (value == null) Styles = null; // Cause default styles to be created in setter.
                return GetPropertyValue<ButtonTool, IButtonToolStyles>(m => m.Styles);
            }
            set
            {
                if (value == null)
                {
                    value = new ButtonToolStyles();
                    ((ButtonToolStyles)value).SetDefaults();
                }
                SetPropertyValue<ButtonTool, IButtonToolStyles>(m => m.Styles, value);
            }
        }

        /// <summary>Gets or sets the style of behavior the button expresses.</summary>
        public ButtonToolType ButtonType
        {
            get { return GetPropertyValue<ButtonTool, ButtonToolType>(m => m.ButtonType); }
            set { SetPropertyValue<ButtonTool, ButtonToolType>(m => m.ButtonType, value); }
        }

        /// <summary>Gets or sets the icon image.</summary>
        public Image Icon
        {
            get { return GetPropertyValue<ButtonTool, Image>(m => m.Icon); }
            set { SetPropertyValue<ButtonTool, Image>(m => m.Icon, value); }
        }

        /// <summary>Gets or sets the text label of the button.</summary>
        public string Text
        {
            get { return GetPropertyValue<ButtonTool, string>(m => m.Text); }
            set { SetPropertyValue<ButtonTool, string>(m => m.Text, value); }
        }

        /// <summary>Gets or sets the tooltip of the button.</summary>
        public string ToolTip
        {
            get { return GetPropertyValue<ButtonTool, string>(m => m.ToolTip); }
            set { SetPropertyValue<ButtonTool, string>(m => m.ToolTip, value); }
        }

        /// <summary>Gets or sets the mouse related state of the button.</summary>
        public ButtonMouseState MouseState
        {
            get { return GetPropertyValue<ButtonTool, ButtonMouseState>(m => m.MouseState); }
            set { SetPropertyValue<ButtonTool, ButtonMouseState>(m => m.MouseState, value, m => m.IsMouseOver, m => m.IsMouseDown); } 
        }

        /// <summary>
        ///     Gets or sets whether the button is currently pressed 
        ///     (see also IsMouseDown.  Relevant when IsToggleButton is True).
        /// </summary>
        public bool IsPressed
        {
            get { return GetPropertyValue<T, bool>(m => m.IsPressed, false); }
            set { SetPropertyValue<T, bool>(m => m.IsPressed, value, false); }
        }

        /// <summary>Gets or sets whether the button retains it's state on each click.</summary>
        public bool IsToggleButton
        {
            get { return GetPropertyValue<T, bool>(m => m.IsToggleButton, false); }
            set { SetPropertyValue<T, bool>(m => m.IsToggleButton, value, false); }
        }

        /// <summary>Gets whether the mouse is currently over the button.</summary>
        public bool IsMouseOver
        {
            get { return MouseState == ButtonMouseState.MouseOver || MouseState == ButtonMouseState.Pressed; }
        }

        /// <summary>Gets whether the mouse is currently pressing the button.</summary>
        public bool IsMouseDown
        {
            get { return MouseState == ButtonMouseState.Pressed; }
        }

        /// <summary>Gets or sets whether the default background is rendered when the mouse is not over the tool (see 'Styles').</summary>
        public bool IsDefaultBackgroundVisible
        {
            get { return GetPropertyValue<ButtonTool, bool>(m => m.IsDefaultBackgroundVisible); }
            set { SetPropertyValue<ButtonTool, bool>(m => m.IsDefaultBackgroundVisible, value); }
        }
        #endregion

        #region Methods
        /// <summary>Simulates a click of the button (used internally and for testing).</summary>
        public void InvokeClick()
        {
            if (!IsEnabled) return;
            ShowDialog();
            if (Click != null) Click(this, new EventArgs());
            PublishToolEvent();
        }

        public override FrameworkElement CreateView()
        {
            return new ButtonToolView { ViewModel = new ButtonToolViewModel(this) };
        }
        #endregion

        #region Methods - Dialog Registration
        public IButtonTool RegisterAsFileOpenDialog(string filter = null, int filterIndex = 1, bool multiSelect = false, Action<IOpenFileDialog> dialogAccepted = null)
        {
            RegisterAsFileOpenDialog(dialog =>
                                         {
                                             dialog.Filter = filter;
                                             dialog.FilterIndex = filterIndex.WithinBounds(1, int.MaxValue);
                                             dialog.MultiSelect = multiSelect;
                                         }, dialogAccepted);
            return this;
        }

        public IButtonTool RegisterAsFileOpenDialog(Action<IOpenFileDialog> dialogSetup, Action<IOpenFileDialog> dialogAccepted = null)
        {
            // Setup initial conditions.
            if (saveFileDialog != null) throw new InitializationException("This button has already been registered as a file-save dialog.");
            if (openFileDialog != null) throw new InitializationException("The file-open dialog handlers have already been registered.");

            // Store state.
            openFileDialog = new DialogInvoker<IOpenFileDialog>(new Models.OpenFileDialog(), dialogSetup, dialogAccepted);

            // Finish up.
            return this;
        }

        public IButtonTool RegisterAsFileSaveDialog(string filter, int filterIndex, string defaultExtension, Action<ISaveFileDialog> dialogAccepted)
        {
            RegisterAsFileSaveDialog(dialog =>
                                         {
                                             dialog.Filter = filter;
                                             dialog.FilterIndex = filterIndex.WithinBounds(1, int.MaxValue);
                                             dialog.DefaultExtension = defaultExtension;
                                         }, 
                                     dialogAccepted);
            return this;
        }

        public IButtonTool RegisterAsFileSaveDialog(Action<ISaveFileDialog> dialogSetup, Action<ISaveFileDialog> dialogAccepted = null)
        {
            // Setup initial conditions.
            if (openFileDialog != null) throw new InitializationException("This button has already been registered as a file-open dialog.");
            if (saveFileDialog != null) throw new InitializationException("The file-save dialog handlers have already been registered.");

            // Store state.
            saveFileDialog = new DialogInvoker<ISaveFileDialog>(new Models.SaveFileDialog(), dialogSetup, dialogAccepted);

            // Finish up.
            return this;
        }
        #endregion

        #region Internal - Dialog
        private void ShowDialog()
        {
            if (!IsEnabled) return;
            if (saveFileDialog != null) ShowSaveFileDialog();
            if (openFileDialog != null) ShowOpenFileDialog();
        }

        private void ShowOpenFileDialog()
        {
            // Setup initial conditions.
            var invoker = openFileDialog;
            invoker.BeforeShow();

            // Show the dialog.
            var dialog = new OpenFileDialog
                             {
                                 Filter = invoker.DialogInfo.Filter,
                                 FilterIndex = invoker.DialogInfo.FilterIndex,
                                 Multiselect = invoker.DialogInfo.MultiSelect,
                             };
            if (dialog.ShowDialog() == true)
            {
                invoker.DialogInfo.File = dialog.File;
                invoker.DialogInfo.Files = dialog.Files;
                invoker.AfterAccepted();
                EventBus.Publish<IOpenFileDialogEvent>(new OpenFileDialogEvent { Dialog = invoker.DialogInfo, ToolId = Id });
            }
        }

        private void ShowSaveFileDialog()
        {
            // Setup initial conditions.
            var invoker = saveFileDialog;
            invoker.BeforeShow();

            // Show the dialog.
            var dialog = new SaveFileDialog
                             {
                                 Filter = invoker.DialogInfo.Filter,
                                 FilterIndex = invoker.DialogInfo.FilterIndex,
                                 DefaultExt = invoker.DialogInfo.DefaultExtension,
                             };
            if (dialog.ShowDialog() == true)
            {
                invoker.AfterAccepted();
                EventBus.Publish<ISaveFileDialogEvent>(new SaveFileDialogEvent { Dialog = invoker.DialogInfo, ToolId = Id});
            }
        }
        #endregion

        private class DialogInvoker<TDialog> where TDialog : IFileSystemDialog
        {
            public DialogInvoker(TDialog dialogInfo, Action<TDialog> setupDialog, Action<TDialog> onAccepted)
            {
                DialogInfo = dialogInfo;
                setupDialogAction = setupDialog;
                onAcceptedAction = onAccepted;
            }

            public TDialog DialogInfo { get; private set; }
            private readonly Action<TDialog> setupDialogAction;
            private readonly Action<TDialog> onAcceptedAction;

            public void BeforeShow() { if (setupDialogAction != null) setupDialogAction(DialogInfo); }
            public void AfterAccepted() { if (onAcceptedAction != null) onAcceptedAction(DialogInfo); }
        }
    }
}
