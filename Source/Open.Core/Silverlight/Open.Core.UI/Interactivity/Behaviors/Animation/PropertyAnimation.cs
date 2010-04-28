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
using System.ComponentModel;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>An attached behavior that invokes an animation when a property changes on the element's view-model.</summary>
    public abstract class PropertyAnimation : Behavior<FrameworkElement>
    {
        #region Head
        public const string PropEasing = "Easing";
        public const string PropDuration = "Duration";
        public const string PropTransformOrigin = "TransformOrigin";
        #endregion

        #region Event Handlers
        private void Handle_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= Handle_Loaded;
            OnAttachComplete();
        }

        private void Handle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnViewModelPropertyChanged(e.PropertyName);
        }
        #endregion

        #region Properties
        /// <summary>Gets the objects view-model.</summary>
        public object ViewModel { get { return AssociatedObject.DataContext; } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the default duration of the animation (in seconds).</summary>
        public double Duration
        {
            get { return (double) (GetValue(DurationProperty)); }
            set { SetValue(DurationProperty, value); }
        }
        /// <summary>Gets or sets the default duration of the animation (in seconds).</summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(
                PropDuration,
                typeof (double),
                typeof (PropertyAnimation),
                new PropertyMetadata(0d));
        

        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction) (GetValue(EasingProperty)); }
            set { SetValue(EasingProperty, value); }
        }
        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register(
                PropEasing,
                typeof (IEasingFunction),
                typeof (PropertyAnimation),
                new PropertyMetadata(null));


        /// <summary>Gets or sets the orgin of the rotation (x,y in percentage of element size).</summary>
        /// <remarks>
        ///    For example, Point(0.5, 0.5) would rotate around the center of the element.
        ///    NB: Implementers, make sure you set this value on the element each time you animate
        ///    because if multiple attached-animations are associated with the element they will override
        ///    each others 'TransformOrigin' values every time they run/construct.
        /// </remarks>
        public Point TransformOrigin
        {
            get { return (Point) (GetValue(TransformOriginProperty)); }
            set { SetValue(TransformOriginProperty, value); }
        }
        /// <summary>Gets or sets the orgin of the rotation (x,y in percentage of element size).</summary>
        public static readonly DependencyProperty TransformOriginProperty =
            DependencyProperty.Register(
                PropTransformOrigin,
                typeof (Point),
                typeof (PropertyAnimation),
                new PropertyMetadata(new Point(0.5,0.5), (s, e) => ((PropertyAnimation) s).SyncTransformOrigin()));
        #endregion

        #region Properties - Private
        private INotifyPropertyChanged NotifyingViewModel
        {
            get { return ViewModel == null ? null : ViewModel as INotifyPropertyChanged; }
        }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject.DataContext != null) OnAttachComplete();
            else AssociatedObject.Loaded += Handle_Loaded;
        }

        private void OnAttachComplete()
        {
            WireEvent(true);
            SyncTransformOrigin();
            Initialize();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            WireEvent(false);
        }

        /// <summary>Called after the behavior has been attached to an element, and the element has completed loading.</summary>
        protected virtual void Initialize(){}

        /// <summary>Retrieves the value of the specified property from the ViewModel.</summary>
        /// <param name="propertyName">The name of the property to retrieve the value of.</param>
        /// <returns>The property value.</returns>
        /// <exception cref="Exception">Thrown if the property could not be read.</exception>
        protected object GetPropertyValue(string propertyName)
        {
            object value;
            if (!GetPropertyValue(propertyName, out value))
            {
                throw new Exception(
                    string.Format("Failed to read the value of property '{0}' when animating.", propertyName));
            }
            return value;
        }

        /// <summary>Retrieves the value of the specified property from the ViewModel.</summary>
        /// <param name="propertyName">The name of the property to retrieve the value of.</param>
        /// <param name="returnValue">The variable to return the property value within.</param>
        /// <returns>True if the value property exists, other false.</returns>
        /// <exception cref="Exception">Thrown if the property could not be read.</exception>
        protected bool GetPropertyValue(string propertyName, out object returnValue)
        {
            // Setup initial conditions.
            returnValue = null;
            propertyName = propertyName.AsNullWhenEmpty();
            if (propertyName == null) return false;

            // Attempt to read the value.
            try
            {
                returnValue = ViewModel.GetType().GetProperty(propertyName).GetValue(ViewModel, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>Invoked when a property value changes on the ViewModel.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected abstract void OnViewModelPropertyChanged(string propertyName);
        #endregion

        #region Internal
        private void WireEvent(bool add)
        {
            var model = NotifyingViewModel;
            if (model == null) return;
            if (add)
            {
                model.PropertyChanged += Handle_PropertyChanged;   
            }
            else
            {
                model.PropertyChanged -= Handle_PropertyChanged;   
            }
        }

        private void SyncTransformOrigin()
        {
            if (AssociatedObject != null) AssociatedObject.RenderTransformOrigin = TransformOrigin;
        }
        #endregion
    }
}
