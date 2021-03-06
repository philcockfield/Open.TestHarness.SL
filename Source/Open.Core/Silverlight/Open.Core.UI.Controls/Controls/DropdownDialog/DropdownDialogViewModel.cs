﻿//------------------------------------------------------
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
using System.Windows.Media;
using System.Windows.Media.Animation;
using Open.Core.Common;
using T = Open.Core.UI.Controls.DropdownDialogViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IDropdownDialog))]
    public class DropdownDialogViewModel : ViewModelBase, IDropdownDialog
    {
        #region Events
        public event EventHandler Showing;
        private void FireShowing(){if (Showing != null) Showing(this, new EventArgs());}

        public event EventHandler Shown;
        internal void FireShown(){if (Shown != null) Shown(this, new EventArgs());}

        public event EventHandler Hiding;
        private void FireHiding(){if (Hiding != null) Hiding(this, new EventArgs());}

        public event EventHandler Hidden;
        private void FireHidden(){if (Hidden != null) Hidden(this, new EventArgs());}
        #endregion

        #region Head
        private const double DefaultSlideDuration = 0.3;
        private const bool DefaultMonitorKeys = true;
        private static readonly IEasingFunction DefaultEasing = new QuadraticEase();
        private const double DefaultDropShadowOpacity = 0.2;
        private const DialogSize DefaultSizeMode = DialogSize.Fixed;
        private static readonly Thickness DefaultPadding = new Thickness(8);
        private static readonly Thickness DefaultMargin = new Thickness(20);

        private Action<PromptResult> onShowComplete;
        private PromptResult? lastClickType;

        /// <summary>Constructor.</summary>
        public DropdownDialogViewModel()
        {
            // Setup initial conditions.
            var black = Colors.Black.ToBrush();

            // Set default values.
            Mask = new BackgroundModel { Color = black, Opacity = 0.2 };
            Background = new BackgroundModel
                             {
                                 Color = Colors.White.ToBrush(),
                                 Opacity = 1,
                                 Border =
                                             {
                                                 Color = black, 
                                                 Opacity = 0.4,
                                                 Thickness = new Thickness(1, 0, 1, 1)
                                             },
                             };
            ButtonBar = new PromptButtonBarViewModel
                            {
                                Padding = new Thickness(8),
                                Background =
                                            {
                                                Color = black, 
                                                Opacity = 0.05,
                                                Border = { Color = black, Thickness = new Thickness(0, 1, 0, 0), Opacity = 0.1 }
                                            }
                            };

            // Wire up events.
            Buttons.Click += (s, e) => OnPromptButtonClick(e.ButtonType);

            // Finish up.
            OnPropertyChanged<T>(m => m.ButtonBar, m => m.Buttons, m => m.Mask, m => m.Background);
        }
        #endregion

        #region Event Handlers
        private void OnPromptButtonClick(PromptResult buttonType)
        {
            lastClickType = buttonType;
            switch (buttonType)
            {
                case PromptResult.Accept:
                case PromptResult.Decline:
                case PromptResult.Cancel:
                    IsShowing = false;
                    break;

                case PromptResult.Next:
                case PromptResult.Back:
                    break;

                default: throw new ArgumentOutOfRangeException(buttonType.ToString());
            }
        }

        internal void OnHidden()
        {
            // Complete a 'Show' interaction sequence if one is in process.
            var buttonType = lastClickType == null ? PromptResult.Cancel : lastClickType.Value;
            if (onShowComplete != null) onShowComplete(buttonType);

            // Finish up.
            lastClickType = null;
            onShowComplete = null;
            FireHidden();
        }

        internal void OnEnterKeyPress()
        {
            if (IsShowing && MonitorEnterKey) Buttons.AcceptButton.InvokeClick();
        }

        internal void OnEscapeKeyPress()
        {
            if (IsShowing && MonitorEscapeKey) Buttons.CancelButton.InvokeClick(); ;
        }
        #endregion

        #region Properties : IDropdownDialog
        public bool IsShowing
        {
            get { return Property.GetValue<T, bool>(m => m.IsShowing, false); }
            set
            {
                if (Property.SetValue<T, bool>(m => m.IsShowing, value, false))
                {
                    if (value) FireShowing(); else FireHiding();
                }
            }
        }
        
        public DialogSize SizeMode
        {
            get { return Property.GetValue<T, DialogSize>(m => m.SizeMode, DefaultSizeMode); }
            set { Property.SetValue<T, DialogSize>(m => m.SizeMode, value, DefaultSizeMode); }
        }

        public double SlideDuration
        {
            get { return Property.GetValue<T, double>(m => m.SlideDuration, DefaultSlideDuration); }
            set { Property.SetValue<T, double>(m => m.SlideDuration, value.WithinBounds(0, double.MaxValue), DefaultSlideDuration); }
        }

        public IEasingFunction Easing
        {
            get { return Property.GetValue<T, IEasingFunction>(m => m.Easing, DefaultEasing); }
            set { Property.SetValue<T, IEasingFunction>(m => m.Easing, value, DefaultEasing); }
        }

        public double DropShadowOpacity
        {
            get { return Property.GetValue<T, double>(m => m.DropShadowOpacity, DefaultDropShadowOpacity); }
            set { Property.SetValue<T, double>(m => m.DropShadowOpacity, value.WithinBounds(0, 1), DefaultDropShadowOpacity); }
        }

        public Thickness Padding
        {
            get { return Property.GetValue<T, Thickness>(m => m.Padding, DefaultPadding); }
            set { Property.SetValue<T, Thickness>(m => m.Padding, value, DefaultPadding); }
        }

        public Thickness Margin
        {
            get { return Property.GetValue<T, Thickness>(m => m.Margin, DefaultMargin); }
            set { Property.SetValue<T, Thickness>(m => m.Margin, value, DefaultMargin); }
        }

        public IViewFactory Content
        {
            get { return Property.GetValue<T, IViewFactory>(m => m.Content); }
            set { Property.SetValue<T, IViewFactory>(m => m.Content, value); }
        }

        public bool MonitorEnterKey
        {
            get { return Property.GetValue<T, bool>(m => m.MonitorEnterKey, DefaultMonitorKeys); }
            set { Property.SetValue<T, bool>(m => m.MonitorEnterKey, value, DefaultMonitorKeys); }
        }

        public bool MonitorEscapeKey
        {
            get { return Property.GetValue<T, bool>(m => m.MonitorEscapeKey, DefaultMonitorKeys); }
            set { Property.SetValue<T, bool>(m => m.MonitorEscapeKey, value, DefaultMonitorKeys); }
        }

        public IBackground Mask { get; private set; }
        public IBackground Background { get; private set; }
        public IPromptButtonBar ButtonBar { get; private set; }
        public IPromptButtons Buttons { get { return ButtonBar == null ? null : ButtonBar.Buttons; } }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new DropdownDialog { ViewModel = this };
        }

        public bool Show(
                        IViewFactory content, 
                        Action<PromptResult> onComplete, 
                        DialogSize? sizeMode, 
                        PromptButtonConfiguration? buttonConfiguration)
        {
            // Setup initial conditions.
            if (IsShowing) return false;
            if (content == null) throw new ArgumentNullException("content");

            // Update state.
            if (sizeMode != null) SizeMode = sizeMode.Value;
            if (buttonConfiguration != null) Buttons.Configuration = buttonConfiguration.Value;
            onShowComplete = onComplete;

            // Insert content.
            if (Content != content) Content = content;
            IsShowing = true; // Reveal the dialog.

            // Finish up.
            return true;
        }
        #endregion
    }
}
