using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
            if (Click != null) Click(this, new EventArgs());
            FireExecuedEvent();
        }
        #endregion

        #region Head
        private readonly ButtonToolViewModel viewModel;

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
            return new ButtonToolView
                       {
                           ViewModel = new ButtonToolViewModel(this),
                       };
        }
        #endregion
    }
}
