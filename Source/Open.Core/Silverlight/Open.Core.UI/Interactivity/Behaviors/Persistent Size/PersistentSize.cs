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
using System.IO.IsolatedStorage;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>A behavior that stores the size of an item to isolated-storage on the client.</summary>
    public class PersistentSize : Behavior<UIElement>
    {
        #region Head
        private readonly DelayedAction delayedAction;
        private readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public PersistentSize()
        {
            delayedAction = new DelayedAction(0.2, SaveSize);
        }
        #endregion

        #region Event Handlers
        void Handle_Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            delayedAction.Start(); // NB: Only invoke the 'SaveSize' method after all SizeChanged events have finished firing.
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets which dimension value to store.</summary>
        public SizeDimension Dimension { get; set; }

        /// <summary>Gets or sets the unique identifier of the value.</summary>
        public string Key { get; set; }

        /// <summary>Gets or sets the delay (in seconds) after the element has changed size that the save operation is invoked.</summary>
        /// <remarks>Having a delay avoids unnecessary save operations to take place on repeated events (such as size changed).</remarks>
        public double SaveDelay
        {
            get { return delayedAction.Delay; }
            set { delayedAction.Delay = value; }
        }
        #endregion

        #region Properties - Private
        protected virtual bool IsAttached{get{ return Element != null;}}
        protected FrameworkElement Element { get; private set; }
        private string FullyQualifiedKey { get; set; }

        protected double? SettingValue
        {
            get
            {
                if (!IsAttached) return null;
                return settings.Contains(FullyQualifiedKey) ? (double?)settings[FullyQualifiedKey] : null;
            }
            set
            {
                if (value == null) settings.Remove(FullyQualifiedKey);
                else settings[FullyQualifiedKey] = value;
            }
        }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            // Setup initial conditions.
            base.OnAttached();
            Element = (FrameworkElement)AssociatedObject;

            // Get the stored key value.
            if (Key.AsNullWhenEmpty() == null) throw new ArgumentNullException(string.Format("A 'Key' value is required for the {0}.", GetType().Name));
            FullyQualifiedKey = string.Format("{0}.{1}.{2}", GetType().Name, Key, Dimension);

            // Wire up events.
            Element.SizeChanged += Handle_Element_SizeChanged;

            // Finish up.
            SyncSizeWithValue();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (Element == null) return;
            Element.SizeChanged -= Handle_Element_SizeChanged;
            Element = null;
        }

        /// <summary>Updates the 'SettingValue' prior to saving.  Override this to perform custom value setting.</summary>
        protected virtual void SyncValueWithSize()
        {
            switch (Dimension)
            {
                case SizeDimension.Width: SettingValue = Element.ActualWidth; break;
                case SizeDimension.Height: SettingValue = Element.ActualHeight; break;
                default: throw new ArgumentOutOfRangeException(Dimension.ToString());
            }
        }

        /// <summary>Sets the initial size of the element upon startup.  Override this to perform custom size setting (eg on a different element).</summary>
        protected virtual void SyncSizeWithValue()
        {
            if (SettingValue == null) return;
            var value = SettingValue.Value;
            switch (Dimension)
            {
                case SizeDimension.Width: Element.Width = value; break;
                case SizeDimension.Height: Element.Height = value; break;
                default: throw new ArgumentOutOfRangeException(Dimension.ToString());
            }
        }
        #endregion

        #region Internal
        private void SaveSize( )
        {
            if (! IsAttached) return;
            SyncValueWithSize();
            settings.Save();
        }
        #endregion
    }
}
