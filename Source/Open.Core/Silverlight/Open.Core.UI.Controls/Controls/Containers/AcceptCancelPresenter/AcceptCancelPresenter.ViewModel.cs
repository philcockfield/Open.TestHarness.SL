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
using System.Windows.Media;
using Open.Core.Assets;
using Open.Core.Common;
using Open.Core.Composite.Command;

using T = Open.Core.UI.Controls.AcceptCancelPresenterViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>
    ///    The basic implementation of a view-model for a AcceptCancel presenter.
    ///    Override and extend this class to control the 'AcceptCancelPresenter'.</summary>
    public abstract class AcceptCancelPresenterViewModel : ViewModelBase, IAcceptCancelPresenter
    {
        #region Events
        /// <summary>Fires when the Accept button is clicked.</summary>
        public event EventHandler AcceptClick;
        private void FireAcceptClick()
        {
            Result = DialogResult.Accepted;
            OnAcceptClick();
            if (AcceptClick != null) AcceptClick(this, new EventArgs());
        }

        /// <summary>Fires when the Cancel button is clicked</summary>
        public event EventHandler CancelClick;
        private void FireCancelClick()
        {
            Result = DialogResult.Cancelled;
            OnCancelClick();
            if (CancelClick != null) CancelClick(this, new EventArgs());
        }
        #endregion

        #region Head
        private static readonly Brush defaultBackground = StyleResources.Colors["Brush.White.080"] as Brush;
        private DelegateCommand<Button> acceptCommand;
        private DelegateCommand<Button> cancelClick;

        /// <summary>Constructor.</summary>
        protected AcceptCancelPresenterViewModel()
        {
            Reset();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the label to display on the 'Accept' button.</summary>
        public string AcceptLabel
        {
            get { return GetPropertyValue<T, string>(m => m.AcceptLabel); }
            set { SetPropertyValue<T, string>(m => m.AcceptLabel, value); }
        }

        /// <summary>Gets or sets the label to display on the 'Cancel' button.</summary>
        public string CancelLabel
        {
            get { return GetPropertyValue<T, string>(m => m.CancelLabel); }
            set { SetPropertyValue<T, string>(m => m.CancelLabel, value); }
        }

        /// <summary>Gets or sets the margin to put around the presenter's content.</summary>
        public Thickness ContentMargin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.ContentMargin, new Thickness(5)); }
            set { SetPropertyValue<T, Thickness>(m => m.ContentMargin, value, new Thickness(5)); }
        }

        /// <summary>Gets or sets the background of the presenter.</summary>
        public Brush Background
        {
            get { return GetPropertyValue<T, Brush>(m => m.Background, defaultBackground); }
            set { SetPropertyValue<T, Brush>(m => m.Background, value, defaultBackground); }
        }

        /// <summary>Gets or sets whether the user accepted or cancelled.</summary>
        public DialogResult Result
        {
            get { return GetPropertyValue<T, DialogResult>(m => m.Result, DialogResult.None); }
            set { SetPropertyValue<T, DialogResult>(m => m.Result, value, DialogResult.None); }
        }

        /// <summary>Gets or sets whether the 'Accept' button is visible.</summary>
        public bool IsAcceptVisible
        {
            get { return GetPropertyValue<T, bool>(m => m.IsAcceptVisible, true); }
            set { SetPropertyValue<T, bool>(m => m.IsAcceptVisible, value, true); }
        }

        /// <summary>Gets or sets whether the 'Cancel' button is visible.</summary>
        public bool IsCancelVisible
        {
            get { return GetPropertyValue<T, bool>(m => m.IsCancelVisible, true); }
            set { SetPropertyValue<T, bool>(m => m.IsCancelVisible, value, true, m => m.AcceptButtonMargin); }
        }
        #endregion

        #region Properties - Commands
        /// <summary>Gets the 'Accept' click command.</summary>
        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null) acceptCommand = new DelegateCommand<Button>(b => FireAcceptClick(), b => IsAcceptEnabled);
                return acceptCommand as ICommand;
            }
        }

        /// <summary>Gets the 'Cancel' click command.</summary>
        public ICommand CancelCommand
        {
            get
            {
                if (cancelClick == null) cancelClick = new DelegateCommand<Button>(b => FireCancelClick(), b => IsCancelEnabled);
                return cancelClick as ICommand;
            }
        }

        /// <summary>Gets or sets whether the 'Accept' button is enabled.</summary>
        public bool IsAcceptEnabled
        {
            get { return GetPropertyValue<T, bool>(m => m.IsAcceptEnabled); }
            set
            {
                SetPropertyValue<T, bool>(m => m.IsAcceptEnabled, value);
                (AcceptCommand as DelegateCommand<Button>).RaiseCanExecuteChanged();
            }
        }

        /// <summary>Gets or sets whether the 'Cancel' button is enabled.</summary>
        public bool IsCancelEnabled
        {
            get { return GetPropertyValue<T, bool>(m => m.IsCancelEnabled, true); }
            set
            {
                SetPropertyValue<T, bool>(m => m.IsCancelEnabled, value, true);
                (CancelCommand as DelegateCommand<Button>).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Properties - Style
        public Thickness AcceptButtonMargin { get { return IsCancelVisible ? new Thickness(0, 0, 5, 0) : new Thickness(0); } }
        #endregion

        #region Methods
        /// <summary>Invoked when the 'Accept' button is clicked.</summary>
        protected virtual void OnAcceptClick()
        {
        }

        /// <summary>Invoked when the 'Cancel' button is clicked.</summary>
        protected virtual void OnCancelClick()
        {
        }

        /// <summary>Resets the OK/Cancel labels to their default values and ensure both button are visible.</summary>
        public void Reset()
        {
            AcceptLabel = StringLibrary.Common_OK;
            CancelLabel = StringLibrary.Common_Cancel;
            IsAcceptVisible = true;
            IsCancelVisible = true;
        }
        #endregion
    }
}
