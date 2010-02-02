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

using System.Windows;
using System.Windows.Media.Animation;
using Open.Core.UI.Common;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>An attached-property animation that alters the size of the element.</summary>
    public class SizeAnimation : PropertyAnimation
    {
        #region Head
        public const string PropWidthPropertyName = "WidthPropertyName";
        public const string PropHeightPropertyName = "HeightPropertyName";
        public const string PropSyncSizeOnLoad = "SyncSizeOnLoad";
        #endregion

        #region Properties
        /// <summary>Gets the current value of the width property (see 'PropertyName').</summary>
        public double? WidthPropertyValue{ get { return GetDimensionValue(WidthPropertyName); } }

        /// <summary>Gets the current value of the height property (see 'PropertyName').</summary>
        public double? HeightPropertyValue { get { return GetDimensionValue(HeightPropertyName); } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the name of the double property that specifies the 'Width' value to monitor on the ViewModel.</summary>
        public string WidthPropertyName
        {
            get { return (string) (GetValue(WidthPropertyNameProperty)); }
            set { SetValue(WidthPropertyNameProperty, value); }
        }
        /// <summary>Gets or sets the name of the double property that specifies the 'Width' value to monitor on the ViewModel.</summary>
        public static readonly DependencyProperty WidthPropertyNameProperty =
            DependencyProperty.Register(
                PropWidthPropertyName,
                typeof (string),
                typeof (SizeAnimation),
                new PropertyMetadata(null));
        

        /// <summary>Gets or sets the name of the double property that specifies the 'Height' value to monitor on the ViewModel.</summary>
        public string HeightPropertyName
        {
            get { return (string) (GetValue(HeightPropertyNameProperty)); }
            set { SetValue(HeightPropertyNameProperty, value); }
        }
        /// <summary>Gets or sets the name of the double property that specifies the 'Height' value to monitor on the ViewModel.</summary>
        public static readonly DependencyProperty HeightPropertyNameProperty =
            DependencyProperty.Register(
                PropHeightPropertyName,
                typeof (string),
                typeof (SizeAnimation),
                new PropertyMetadata(null));


        /// <summary>Gets or sets whether the initial size of the element should be synced with the view-model upon loading.</summary>
        /// <remarks>This is typically desirable as this behavior is used in stead of directly databinding the element to the view-model.</remarks>
        public bool SyncSizeOnLoad
        {
            get { return (bool) (GetValue(SyncSizeOnLoadProperty)); }
            set { SetValue(SyncSizeOnLoadProperty, value); }
        }
        /// <summary>Gets or sets whether the initial size of the element should be synced with the view-model upon loading.</summary>
        public static readonly DependencyProperty SyncSizeOnLoadProperty =
            DependencyProperty.Register(
                PropSyncSizeOnLoad,
                typeof (bool),
                typeof (SizeAnimation),
                new PropertyMetadata(true));
        #endregion

        #region Methods
        protected override void Initialize()
        {
            base.Initialize();
            if (SyncSizeOnLoad) SyncSize();
        }

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            if (propertyName == null) return;
            if (propertyName == WidthPropertyName) AnimateWidth();
            if (propertyName == HeightPropertyName) AnimateHeight();
        }
        #endregion

        #region Internal
        private void SyncSize()
        {
            var width = WidthPropertyValue;
            var height = HeightPropertyValue;

            if (width != null) AssociatedObject.Width = width.Value;
            if (height != null) AssociatedObject.Height = height.Value;
        }

        private void AnimateWidth()
        {
            var propertyValue = WidthPropertyValue;
            if (propertyValue != null) Animate(AssociatedObject.ActualWidth, propertyValue.Value, "Width");
        }

        private void AnimateHeight()
        {
            var propertyValue = HeightPropertyValue;
            if (propertyValue != null) Animate(AssociatedObject.ActualHeight, propertyValue.Value, "Height");
        }

        private void Animate(double fromValue, double toValue, string propertyName)
        {
            AnimationUtil.DoubleAnimate(
                AssociatedObject, fromValue, toValue, Duration, propertyName, Easing, null);
        }

        private double? GetDimensionValue(string propertyName)
        {
            object value;
            if (GetPropertyValue(propertyName, out value)) return (double?)value;
            return null;
        }
        #endregion
    }
}
