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
using System.Windows.Input;
using Open.Core.Common;
using Open.Core.UI.Common;

namespace Open.Core.UI.Controls
{
    public partial class DropdownDialog : UserControl
    {
        #region Head
        private DataContextObserver dataContextObserver;
        private PropertyObserver<DropdownDialogViewModel> viewModelObserver;
        private int animationCount;

        /// <summary>Constructor.</summary>
        public DropdownDialog()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
            SizeChanged += delegate { UpdateDialogPositionAndSize(); };
            KeyDown += OnKeyDown;
            Loaded += delegate
                          {
                              content.SizeChanged += delegate { UpdateDialogPositionAndSize(); };
                          };

            // Finish up.
            SetTop(-50000);
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            // Setup initial conditions.
            if (viewModelObserver != null) viewModelObserver.Dispose();
            if (ViewModel == null) return;

            // Wire up events.
            viewModelObserver = new PropertyObserver<DropdownDialogViewModel>(ViewModel)
                .RegisterHandler(m => m.IsShowing, OnIsShowingChanged)
                .RegisterHandler(m => m.SizeMode, UpdateDialogPositionAndSize)
                .RegisterHandler(m => m.DropShadowOpacity, UpdateElementVisibility)
                .RegisterHandler(m => m.Margin, UpdateDialogPositionAndSize);

            // Finish up.
            UpdateDialogPositionAndSize();
            UpdateElementVisibility();
        }

        private void OnIsShowingChanged()
        {
            AnimateDialog();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;
            if (e.Key == Key.Escape) ViewModel.OnEscapeKeyPress();
            if (e.Key == Key.Enter) ViewModel.OnEnterKeyPress();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public DropdownDialogViewModel ViewModel
        {
            get { return DataContext as DropdownDialogViewModel; }
            set { DataContext = value; }
        }

        private bool IsShowing { get { return ViewModel == null ? false : ViewModel.IsShowing; } }
        private bool IsInitalized { get { return ViewModel != null; } }
        private bool IsAnimating { get { return animationCount > 0; } }
        private Thickness DialogMargin { get { return ViewModel == null ? default(Thickness) : ViewModel.Margin; } }
        #endregion

        #region Internal
        private void AnimateDialog()
        {
            if (ViewModel == null) return;
            if (content.ViewFactory != null && !content.IsViewLoaded)
            {
                new AnimateWhenLoaded(this, content);
                return;
            }
            AnimateDialog(ViewModel.SlideDuration);
        }

        private void AnimateDialog(double duration)
        {
            // Setup initial conditions.
            if (ViewModel == null) return;
            animationCount++;
            UpdateDialogPositionAndSize();

            // Start the mask-fade in animation.
            mask.Visibility = Visibility.Visible;
            var fromOpacity = IsShowing ? 0 : 1;
            var toOpacity = IsShowing ? 1 : 0;
            AnimationUtil.Fade(mask, fromOpacity, toOpacity, duration);

            // Start the slide animation.
            Point startPosition;
            Point endPosition;
            GetSlidePositions(out startPosition, out endPosition);
            dropShadow.Opacity = 0;
            AnimationUtil.Move(contentContainer, startPosition, endPosition, duration, ViewModel.Easing, () =>
                                                            {
                                                                animationCount--;
                                                                UpdateElementVisibility();
                                                                UpdateDialogPositionAndSize();
                                                                if (IsShowing) FocusContent();
                                                                if (ViewModel != null)
                                                                {
                                                                    if (IsShowing) ViewModel.FireShown(); else ViewModel.OnHidden();
                                                                }
                                                            });
        }

        private void GetSlidePositions(out Point start, out Point end)
        {
            var x = GetDialogLeft();
            var offStage = new Point(x, 0 - contentContainer.DesiredSize.Height);
            var onStage = new Point(x, 0);

            start = IsShowing ? offStage : onStage;
            end = IsShowing ? onStage : offStage;
        }

        private void UpdateElementVisibility()
        {
            mask.Visibility = IsShowing ? Visibility.Visible : Visibility.Collapsed;
            dropShadow.Opacity = IsShowing && !IsAnimating ? ViewModel.DropShadowOpacity : 0;
            IsHitTestVisible = IsShowing;
        }

        private double GetDialogLeft()
        {
            return Math.Round((ActualWidth * 0.5) - (contentContainer.DesiredSize.Width * 0.5));
        }

        private void FocusContent()
        {
            var contentElement = Content as UIElement;
            if (contentElement == null) return;
            if (contentElement.ContainsFocus()) return;

            // Attempt to set focus to a child adorned with the 'Focus.IsDefault' attached property.
            var defaultChildFocused = contentElement.FocusDefaultChild();

            // Focus the content directly if there was no default child.
            if (!defaultChildFocused) contentElement.Focus();
        }
        #endregion

        #region Internal : Position and Size
        private void UpdateDialogPositionAndSize()
        {
            UpdateDialogPosition();
            UpdateDialogSize();
        }

        private void UpdateDialogPosition(bool updateX = true, bool updateY = true)
        {
            // Setup initial conditions.
            if (!IsInitalized) return;

            // Center the dialog (X).
            if (updateX) Canvas.SetLeft(contentContainer, GetDialogLeft());

            // Set top value (Y).
            if (updateY && !IsAnimating)
            {
                SetTop(IsShowing ? 0 : -500000);
            }
        }

        private void SetTop(double y)
        {
            Canvas.SetTop(contentContainer, y);
        }

        private void UpdateDialogSize()
        {
            if (!IsInitalized) return;
            switch (ViewModel.SizeMode)
            {
                case DialogSize.Fixed:
                    contentContainer.Width = double.NaN;
                    contentContainer.Height = double.NaN;
                    break;

                case DialogSize.Fill:
                    StretchHorizontal();
                    StretchVertical();
                    break;

                case DialogSize.StretchHorizontal:
                    StretchHorizontal();
                    contentContainer.Height = double.NaN;
                    break;

                case DialogSize.StretchVertical:
                    StretchVertical();
                    contentContainer.Width = double.NaN;
                    break;

                default: throw new ArgumentOutOfRangeException(ViewModel.SizeMode.ToString());
            }
        }

        private void StretchHorizontal()
        {
            contentContainer.Width = (ActualWidth - (DialogMargin.Left + DialogMargin.Right)).WithinBounds(0,double.MaxValue);
        }

        private void StretchVertical()
        {
            contentContainer.Height = (ActualHeight - DialogMargin.Bottom).WithinBounds(0, double.MaxValue);
        }
        #endregion

        /// <summary>Provides single callback for an animate pause waiting for content to be loaded.</summary>
        private class AnimateWhenLoaded
        {
            private DropdownDialog parent;
            private ViewFactoryContent content;

            public AnimateWhenLoaded(DropdownDialog parent, ViewFactoryContent content)
            {
                this.parent = parent;
                this.content = content;
                parent.SetTop(-50000);
                content.IsViewLoadedChanged += OnIsLoaded;
            }
            private void OnIsLoaded(object sender, EventArgs e)
            {
                // Unwire from the loaded event.
                content.IsViewLoadedChanged -= OnIsLoaded;

                // Make sure the dialog is correctly sized and positioned.
                parent.UpdateLayout();
                parent.UpdateDialogSize();
                parent.UpdateDialogPosition(updateY: false);

                // Restart the animation.
                parent.AnimateDialog();

                // Finish up.
                parent = null;
                content = null;
            }
        }
    }
}