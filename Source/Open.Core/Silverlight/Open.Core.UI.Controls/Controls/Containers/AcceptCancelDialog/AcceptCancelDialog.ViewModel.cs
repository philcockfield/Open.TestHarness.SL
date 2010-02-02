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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.ComponentModel;
using Open.Core.Common;

using T = Open.Core.UI.Controls.AcceptCancelDialogViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>The logical presentation model for the 'AcceptCancelDialog' view.</summary>
    public class AcceptCancelDialogViewModel : ViewModelBase, IAcceptCancelDialog
    {
        #region Events
        /// <summary>Fires when the Accept button is clicked.</summary>
        public event EventHandler AcceptClick;
        private void OnAcceptClick() { if (AcceptClick != null) AcceptClick(this, new EventArgs()); }

        /// <summary>Fires when the Cancel button is clicked</summary>
        public event EventHandler CancelClick;
        private void OnCancelClick() { if (CancelClick != null) CancelClick(this, new EventArgs()); }
        #endregion

        #region Head
        private static readonly Brush defaultBackgroundColor = StyleResources.Colors["Brush.White.095"] as Brush;

        /// <summary>Constructor.</summary>
        public AcceptCancelDialogViewModel() : this(null, null) { }

        /// <summary>Constructor.</summary>
        /// <param name="contentTemplate">The XAML template of the dialog content (see 'Content' property).</param>
        /// <param name="contentViewModel">The view-model for the content (see 'Content' property).</param>
        public AcceptCancelDialogViewModel(DataTemplate contentTemplate, INotifyPropertyChanged contentViewModel) 
        {
            // Store the presenter (use the given view-model if it happens to be a presenter).
            var modelAsPresenter = contentViewModel as IAcceptCancelPresenter;
            AcceptCancelPresenter = modelAsPresenter ?? new AcceptCancelPresenterEmbeddedViewModel();
            
            // Store content.
            Content = new ViewTemplate(contentTemplate, contentViewModel);
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            WireEvents(AcceptCancelPresenter, false);
        }
        #endregion

        #region Event Handlers
        private void HandleAcceptClick(object sender, EventArgs e)
        {
            OnAcceptClick();
            if (AutoHideOnAccept) IsShowing = false;
        }

        private void HandleCancelClick(object sender, EventArgs e)
        {
            OnCancelClick();
            if (AutoHideOnCancel) IsShowing = false;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pixel width of the dialog.</summary>
        public double DialogWidth
        {
            get { return GetPropertyValue<T, double>(m => m.DialogWidth, 550); }
            set { SetPropertyValue<T, double>(m => m.DialogWidth, value, 550); }
        }

        /// <summary>Gets or sets the pixel height of the dialog.</summary>
        public double DialogHeight
        {
            get { return GetPropertyValue<T, double>(m => m.DialogHeight, 350); }
            set { SetPropertyValue<T, double>(m => m.DialogHeight, value, 350); }
        }

        /// <summary>Gets the content (Template + View-Model).</summary>
        public IViewTemplate Content { get; private set; }

        /// <summary>Gets or sets whether the dialog is automatically hidden when the Cancel button is clicked.</summary>
        public bool AutoHideOnCancel
        {
            get { return GetPropertyValue<T, bool>(m => m.AutoHideOnCancel, true); }
            set { SetPropertyValue<T, bool>(m => m.AutoHideOnCancel, value, true); }
        }

        /// <summary>Gets or sets whether the dialog is automatically hidden when the Accept button is clicked.</summary>
        public bool AutoHideOnAccept
        {
            get { return GetPropertyValue<T, bool>(m => m.AutoHideOnAccept, false); }
            set { SetPropertyValue<T, bool>(m => m.AutoHideOnAccept, value, false); }
        }

        /// <summary>Gets the embedded view-model for the 'AcceptCancelPresenter'.</summary>
        public IAcceptCancelPresenter AcceptCancelPresenter
        {
            get { return GetPropertyValue<T, IAcceptCancelPresenter>(m => m.AcceptCancelPresenter); }
            set
            {
                // Setup initial conditions.
                if (value == AcceptCancelPresenter) return;
                if (AcceptCancelPresenter != null) WireEvents(AcceptCancelPresenter, false);

                // Store value.
                SetPropertyValue<T, IAcceptCancelPresenter>(m => m.AcceptCancelPresenter, value);
                if (value != null) value.Background = Background;

                // Wire up events.
                if (value != null) WireEvents(value, true);
            }
        }
        #endregion

        #region Properties - IDialogPresenter
        /// <summary>Gets or sets whether the dialog is currently being shown.</summary>
        public bool IsShowing
        {
            get { return GetPropertyValue<T, bool>(m => m.IsShowing); }
            set { SetPropertyValue<T, bool>(m => m.IsShowing, value); }
        }

        /// <summary>Gets or sets the duration (in seconds) of the animated slide when the dialog is shown or hidden.</summary>
        public double AnimationDuration
        {
            get { return GetPropertyValue<T, double>(m => m.AnimationDuration, 0.2); }
            set { SetPropertyValue<T, double>(m => m.AnimationDuration, value, 0.2); }
        }

        /// <summary>Gets or sets the opacity of the mask which covers the UI when the dialog is showing.</summary>
        public double MaskOpacity
        {
            get { return GetPropertyValue<T, double>(m => m.MaskOpacity, 0.7); }
            set { SetPropertyValue<T, double>(m => m.MaskOpacity, value, 0.7); }
        }

        /// <summary>Gets or sets the brush used for the mask which covers the UI when the dialog is showing.</summary>
        public Brush MaskBrush
        {
            get { return GetPropertyValue<T, Brush>(m => m.MaskBrush, new SolidColorBrush(Colors.White)); }
            set { SetPropertyValue<T, Brush>(m => m.MaskBrush, value, new SolidColorBrush(Colors.White)); }
        }

        /// <summary>Gets or sets the easing function to animate the slide with (Null if not required).</summary>
        public IEasingFunction Easing
        {
            get { return GetPropertyValue<T, IEasingFunction>(m => m.Easing, new QuadraticEase{EasingMode = EasingMode.EaseIn}); }
            set { SetPropertyValue<T, IEasingFunction>(m => m.Easing, value, new QuadraticEase{EasingMode = EasingMode.EaseIn}); }
        }
        #endregion

        #region Properties - IAcceptCancelPresenter
        /// <summary>Gets or sets the label to display on the 'Accept' button.</summary>
        public string AcceptLabel
        {
            get { return AcceptCancelPresenter.AcceptLabel; }
            set { AcceptCancelPresenter.AcceptLabel = value; }
        }

        /// <summary>Gets or sets the label to display on the 'Cancel' button.</summary>
        public string CancelLabel
        {
            get { return AcceptCancelPresenter.CancelLabel; }
            set { AcceptCancelPresenter.CancelLabel = value; }
        }

        /// <summary>Gets or sets whether the user accepted or cancelled.</summary>
        public DialogResult Result
        {
            get { return AcceptCancelPresenter.Result; }
            set { AcceptCancelPresenter.Result = value; }
        }

        /// <summary>Gets or sets the margin to put around the presenter's content.</summary>
        public Thickness ContentMargin
        {
            get { return AcceptCancelPresenter.ContentMargin; }
            set { AcceptCancelPresenter.ContentMargin = value; }
        }

        /// <summary>Gets or sets the background of the presenter.</summary>
        public Brush Background
        {
            get { return GetPropertyValue<T, Brush>(m => m.Background, defaultBackgroundColor); }
            set
            {
                if (value == Background) return;
                SetPropertyValue<T, Brush>(m => m.Background, value, defaultBackgroundColor);
                if (AcceptCancelPresenter != null) AcceptCancelPresenter.Background = value;
            }
        }

        /// <summary>Gets the 'Accept' click command.</summary>
        public ICommand AcceptCommand
        {
            get { return AcceptCancelPresenter.AcceptCommand; }
        }

        /// <summary>Gets the 'Cancel' click command.</summary>
        public ICommand CancelCommand
        {
            get { return AcceptCancelPresenter.CancelCommand; }
        }

        /// <summary>Gets or sets whether the 'Accept' button is enabled.</summary>
        public bool IsAcceptEnabled
        {
            get { return AcceptCancelPresenter.IsAcceptEnabled; }
            set { AcceptCancelPresenter.IsAcceptEnabled = value; }
        }

        /// <summary>Gets or sets whether the 'Cancel' button is enabled.</summary>
        public bool IsCancelEnabled
        {
            get { return AcceptCancelPresenter.IsCancelEnabled; }
            set { AcceptCancelPresenter.IsCancelEnabled = value; }
        }

        /// <summary>Gets or sets whether the 'Accept' button is visible.</summary>
        public bool IsAcceptVisible
        {
            get { return AcceptCancelPresenter.IsAcceptVisible; }
            set { AcceptCancelPresenter.IsAcceptVisible = value; }
        }

        /// <summary>Gets or sets whether the 'Cancel' button is visible.</summary>
        public bool IsCancelVisible
        {
            get { return AcceptCancelPresenter.IsCancelVisible; }
            set { AcceptCancelPresenter.IsCancelVisible = value; }
        }
        #endregion

        #region Methods
        /// <summary>Resets the OK/Cancel labels to their default values and ensure both button are visible.</summary>
        public void Reset()
        {
            AcceptCancelPresenter.Reset();
        }
        #endregion

        #region Internal
        private void WireEvents(IAcceptCancelPresenter presenter, bool add)
        {
            if (presenter == null) return;
            if (add)
            {
                presenter.AcceptClick += HandleAcceptClick;
                presenter.CancelClick += HandleCancelClick;
            }
            else
            {
                presenter.AcceptClick -= HandleAcceptClick;
                presenter.CancelClick -= HandleCancelClick;
            }
        }
        #endregion

        public class AcceptCancelPresenterEmbeddedViewModel : AcceptCancelPresenterViewModel
        {
        }
    }
}
