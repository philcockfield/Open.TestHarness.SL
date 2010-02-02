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
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Updates a textbox's source databinding value on key-press.</summary>
    /// <remarks>
    ///    Without this behavior, focus has to be removed from the textbox before the data-binding is syncronized.<BR/>
    ///    When using the behavior you may want ot use the 'UpdateSourceTrigger=Explicit' flag in the binding, for example:<BR/>
    ///    <BR/>
    ///    Text="{Binding Mode=TwoWay, Path=PropertyName, UpdateSourceTrigger=Explicit}"
    /// </remarks>
    public class UpdateOnKeyPress : Behavior<TextBox>
    {
        #region Events
        /// <summary>Fires when the data-source has been updated by the behavior.</summary>
        public event EventHandler Updated;
        protected void OnUpdated() { if (Updated != null) Updated(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropIsActive = "IsActive";
        public const string PropDelay = "Delay";

        /// <summary>The default delay time (in seconds).</summary>
        public const double DefaultDelay = 0.2;
        private readonly DelayedAction delayedAction;

        public UpdateOnKeyPress()
        {
            delayedAction = new DelayedAction(DefaultDelay, OnDelayTimeout);
        }
        #endregion
        
        #region Event Handlers
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (!IsActive) return;
            if (AssociatedObject.DataContext == null) return;
            delayedAction.Start();
        }

        private void OnDelayTimeout()
        {
            if (!IsActive) return;
            UpdateDataSource(AssociatedObject);
            OnUpdated();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the time delay in delay (in seconds) before the data-source is updated.</summary>
        /// <remarks>
        ///    This is useful for not overloading the updating system when the user is typing several characters or words.
        ///    Set to zero (0) for instant updating.
        /// </remarks>
        public double Delay
        {
            get { return (double) (GetValue(DelayProperty)); }
            set { SetValue(DelayProperty, value); }
        }
        /// <summary>Gets or sets the time delay in delay (in seconds) before the data-source is updated.</summary>
        /// <remarks>
        ///    This is useful for not overloading the updating system when the user is typing several characters or words.
        ///    Set to zero (0) for instant updating.
        /// </remarks>
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register(
                PropDelay,
                typeof (double),
                typeof (UpdateOnKeyPress),
                new PropertyMetadata(DefaultDelay, (sender, e) => ((UpdateOnKeyPress)sender).delayedAction.Delay = (double)e.NewValue));


        /// <summary>Gets or sets whether updates are initiated on keypress.</summary>
        public bool IsActive
        {
            get { return (bool) (GetValue(IsActiveProperty)); }
            set { SetValue(IsActiveProperty, value); }
        }
        /// <summary>Gets or sets whether updates are initiated on keypress.</summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                PropIsActive,
                typeof (bool),
                typeof (UpdateOnKeyPress),
                new PropertyMetadata(true));
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyUp += HandleKeyPress;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyUp -= HandleKeyPress;
        }

        /// <summary>Updates the data-source that is bound to the 'Text' property with the current value of the textbox.</summary>
        /// <param name="textbox">The textbox to update the data-source of.</param>
        public static void UpdateDataSource(TextBox textbox)
        {
            var binding = textbox.GetBindingExpression(TextBox.TextProperty);
            if (binding == null) return;
            binding.UpdateSource();
        }
        #endregion
    }
}
