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
using System.Diagnostics;
using Open.Core.Common;

using T = Open.Core.UI.Controls.Dialogs.DialogContent;

namespace Open.Core.UI.Controls.Dialogs
{
    /// <summary>Content view-model for a dialog box.</summary>
    public class DialogContent : AcceptCancelPresenterViewModel, IDialogContent
    {
        #region Events
        /// <summary>Fires when the dialog is revealed.</summary>
        public event EventHandler Shown;
        private void OnShown()
        {
            Result = DialogResult.None;
            if (Shown != null) Shown(this, new EventArgs());
        }

        /// <summary>Fires when the dialog is hidden.</summary>
        public event EventHandler Hidden;
        private void OnHidden()
        {
            if (Hidden != null) Hidden(this, new EventArgs());
            InvokeHiddenCallback();
        }
        #endregion

        #region Head
        public const double DefaultWidth = 550;
        public const double DefaultHeight = 350;
        private readonly PropertyObserver<DialogContent> propertyObserver;
        private readonly PropertyObserver<IViewTemplate> contentPropertyObserver;
        private PropertyObserver<IAcceptCancelDialog> parentDialogPropertyObserver;
        private Action<DialogResult> onHiddenCallback;

        /// <summary>Constructor.</summary>
        public DialogContent()
        {
            // Setup initial conditions.
            Content = new ViewTemplate { ViewModel = this };

            // Wire up events.
            propertyObserver = new PropertyObserver<DialogContent>(this)
                .RegisterHandler(m => m.Width, m => SyncSizeOnParent())
                .RegisterHandler(m => m.Height, m => SyncSizeOnParent());

            contentPropertyObserver = new PropertyObserver<IViewTemplate>(Content)
                .RegisterHandler(m => m.Template, m => SyncTemplateOnParent());
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            Content.ViewModel = null;
            Content.Dispose();
            propertyObserver.Dispose();
            contentPropertyObserver.Dispose();
            if (ParentDialog != null) ParentDialog = null;
        }
        #endregion

        #region Event Handlers
        private void OnIsShowingChanged()
        {
            if (ParentDialog == null)
            {
                parentDialogPropertyObserver.Dispose();
                return;
            }
            Result = ParentDialog.Result;
            if (ParentDialog.IsShowing) OnShown();
            if (!ParentDialog.IsShowing) OnHidden();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the width of the dialog content.</summary>
        public double Width
        {
            get { return GetPropertyValue<DialogContent, double>(m => m.Width, DefaultWidth); }
            set { SetPropertyValue<DialogContent, double>(m => m.Width, value, DefaultWidth); }
        }

        /// <summary>Gets or sets the height of the dialog content.</summary>
        public double Height
        {
            get { return GetPropertyValue<DialogContent, double>(m => m.Height, DefaultHeight); }
            set { SetPropertyValue<DialogContent, double>(m => m.Height, value, DefaultHeight); }
        }

        /// <summary>Gets the content (XAML template + view-model).</summary>
        public IViewTemplate Content { get; private set; }

        /// <summary>Gets or sets the parent dialog that this content belongs to.</summary>
        public IAcceptCancelDialog ParentDialog
        {
            get { return GetPropertyValue<DialogContent, IAcceptCancelDialog>(m => m.ParentDialog); }
            set
            {
                // Setup initial conditions.
                if (value == ParentDialog) return;
                if (parentDialogPropertyObserver != null) parentDialogPropertyObserver.Dispose();

                // Store value.
                SetPropertyValue<DialogContent, IAcceptCancelDialog>(m => m.ParentDialog, value);

                // Initialize the dialog with this content.
                if (value != null)
                {
                    value.Content.ViewModel = this;
                    SyncTemplateOnParent();
                }

                // Wire up events.
                if (value != null)
                {
                    parentDialogPropertyObserver = new PropertyObserver<IAcceptCancelDialog>(value)
                        .RegisterHandler(m => m.IsShowing, m => OnIsShowingChanged());
                }

                // Finish up.
                UpdateVisualState();
            }
        }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateVisualState()
        {
            SyncSizeOnParent();
        }


        /// <summary>Causes the dialog box to be hidden.</summary>
        public void Hide()
        {
            if (ParentDialog == null) return;
            ParentDialog.IsShowing = false;
        }
        #endregion

        #region Methods - Show
        /// <summary>Causes the dialog box to be shown.</summary>
        public void Show()
        {
            if (ParentDialog == null) return;
            ParentDialog.IsShowing = true;
        }

        /// <summary>Causes the dialog box to be shown.</summary>
        /// <param name="onHidden">Action that is invoked when the dialog is hidden.</param>
        public void Show(Action<DialogResult> onHidden)
        {
            if (ParentDialog == null) return;
            onHiddenCallback = onHidden;
            Show();
        }

        /// <summary>Causes the dialog box to be shown.</summary>
        /// <param name="delay">
        ///    Shows the dialog after the specified delay (secs). 
        ///    This allows screen updates to occur prior to the dialog showing.
        ///    Typically set very short, at either 0.1 secs or 0 secs.
        /// </param>
        /// <param name="onHidden">Action that is invoked when the dialog is hidden.</param>
        public void Show(double delay, Action<DialogResult> onHidden)
        {
            delay = delay.WithinBounds(0, double.MaxValue);
            DelayedAction.Invoke(delay, () => Show(onHidden));
        }
        #endregion

        #region Internal
        private void SyncSizeOnParent()
        {
            if (ParentDialog == null) return;
            ParentDialog.DialogWidth = Width;
            ParentDialog.DialogHeight = Height;
        }

        private void SyncTemplateOnParent()
        {
            if (ParentDialog == null) return;
            ParentDialog.Content.Template = Content.Template;
        }

        private void InvokeHiddenCallback()
        {
            if (onHiddenCallback == null ) return;
            onHiddenCallback(Result);
            onHiddenCallback = null;
        }
        #endregion
    }
}
