﻿using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using T = Open.Core.UI.Controls.Controls.ToolBar.ButtonTool;

[assembly: InternalsVisibleTo("Open.Core.Test")]

namespace Open.Core.UI.Controls.Controls.ToolBar
{
    /// <summary>A ToolBar tool which behaves like a button.</summary>
    [Export(typeof(IButtonTool))]
    public class ButtonTool : ToolBase, IButtonTool
    {
        #region Events
        /// <summary>Fires when the button is clicked.</summary>
        public event EventHandler Click;
        internal void FireClick()
        {
            ShowDialog();
            if (Click != null) Click(this, new EventArgs());
            FireExecuedEvent();
        }
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

        #region Properties

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
            get { return GetPropertyValue<T, ButtonToolType>(m => m.ButtonType); }
            set { SetPropertyValue<T, ButtonToolType>(m => m.ButtonType, value); }
        }

        /// <summary>Gets or sets the icon image.</summary>
        public Image Icon
        {
            get { return GetPropertyValue<T, Image>(m => m.Icon); }
            set { SetPropertyValue<T, Image>(m => m.Icon, value); }
        }

        /// <summary>Gets or sets the text label of the button.</summary>
        public string Text
        {
            get { return GetPropertyValue<T, string>(m => m.Text); }
            set { SetPropertyValue<T, string>(m => m.Text, value); }
        }

        /// <summary>Gets or sets the tooltip of the button.</summary>
        public string ToolTip
        {
            get { return GetPropertyValue<T, string>(m => m.ToolTip); }
            set { SetPropertyValue<T, string>(m => m.ToolTip, value); }
        }

        /// <summary>Gets or sets the mouse related state of the button.</summary>
        public ButtonMouseState MouseState
        {
            get { return GetPropertyValue<T, ButtonMouseState>(m => m.MouseState); }
            set { SetPropertyValue<T, ButtonMouseState>(m => m.MouseState, value, m => m.IsMouseOver, m => m.IsMouseDown); }
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
            get { return GetPropertyValue<T, bool>(m => m.IsDefaultBackgroundVisible); }
            set { SetPropertyValue<T, bool>(m => m.IsDefaultBackgroundVisible, value); }
        }
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            return new ButtonToolView { ViewModel = new ButtonToolViewModel(this) };
        }

        public void RegisterFileSaveDialog(Action<ISaveFileDialog> setupDialog, Action<ISaveFileDialog> onAccepted)
        {
            // Setup initial conditions.
            if (setupDialog == null) throw new ArgumentNullException("setupDialog");
            if (onAccepted == null) throw new ArgumentNullException("onAccepted");
            if (openFileDialog != null) throw new InitializationException("The button has already been registered as a file-open dialog.");
            if (saveFileDialog != null) throw new InitializationException("The file-save dialog handlers have already been registered.");

            // Store state.
            saveFileDialog = new DialogInvoker<ISaveFileDialog>
                                 {
                                     DialogInfo = new Models.SaveFileDialog(),
                                     SetupDialog = setupDialog,
                                     OnAccepted = onAccepted,
                                 };
        }

        public void RegisterFileOpenDialog(Action<IOpenFileDialog> setupDialog, Action<IOpenFileDialog> onAccepted)
        {
            // Setup initial conditions.
            if (setupDialog == null) throw new ArgumentNullException("setupDialog");
            if (onAccepted == null) throw new ArgumentNullException("onAccepted");
            if (saveFileDialog != null) throw new InitializationException("The button has already been registered as a file-save dialog.");
            if (openFileDialog != null) throw new InitializationException("The file-open dialog handlers have already been registered.");

            // Store state.
            openFileDialog = new DialogInvoker<IOpenFileDialog>
                                {
                                    DialogInfo = new Models.OpenFileDialog(),
                                    SetupDialog = setupDialog,
                                    OnAccepted = onAccepted,
                                };
        }
        #endregion

        #region Internal - Dialog
        private void ShowDialog()
        {
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
                                    DefaultExt = invoker.DialogInfo.DefaultExt,
                                };
            if (dialog.ShowDialog() == true)
            {
                invoker.AfterAccepted();
            }
        }
        #endregion

        internal class DialogInvoker<TDialog> where TDialog : IFileSystemDialog
        {
            public TDialog DialogInfo { get; set; }
            public Action<TDialog> SetupDialog { get; set; }
            public Action<TDialog> OnAccepted { get; set; }

            public void BeforeShow() { SetupDialog(DialogInfo); }
            public void AfterAccepted() { OnAccepted(DialogInfo); }
        }
    }
}
