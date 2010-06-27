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
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.TestHarness.Model;

using T = Open.TestHarness.View.ControlHost.DisplayContainerViewModel;

namespace Open.TestHarness.View.ControlHost
{
    /// <summary>The view model for the 'DisplayContainer' control.</summary>
    public class DisplayContainerViewModel : ViewModelBase
    {
        #region Head
        private const string ItemsControlBorderElementName = "itemsControlBorder";
            
        private readonly ViewTestClass model;
        private readonly PropertyObserver<ViewTestClass> modelObserver;

        public DisplayContainerViewModel(ViewTestClass model)
        {
            // Setup initial conditions.
            this.model = model;

            // Create wrapper collection.
            CurrentControls = new ObservableCollectionWrapper<object, DisplayItemViewModel>(
                                                model.CurrentControls,
                                                control => new DisplayItemViewModel(this, control as UIElement));

            // Wire up events.
            modelObserver = new PropertyObserver<ViewTestClass>(model)
                .RegisterHandler(m => m.CurrentViewTest, m => OnCurrentViewTestChanged());
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            CurrentControls.Dispose();
            modelObserver.Dispose();
        }
        #endregion

        #region Event Handlers
        private void OnCurrentViewTestChanged()
        {
            OnPropertyChanged<T>(m => m.CurrentControls, m => m.ScrollBarVisibility, m => m.ScrollViewerBorderThickness);
        }
        #endregion

        #region Properties
        /// <summary>Gets the display name of the class.</summary>
        public string DisplayName { get { return model.DisplayName; } }

        /// <summary>Gets the current set of controls pertaining the the current [ViewTest].</summary>
        public ObservableCollectionWrapper<object, DisplayItemViewModel> CurrentControls { get; private set; }

        /// <summary>Gets or sets the visibility of the scrollbars.</summary>
        public ScrollBarVisibility ScrollBarVisibility { get { return IsFillMode ? ScrollBarVisibility.Hidden : ScrollBarVisibility.Auto; } }

        /// <summary>Gets or sets the margin of the scroll viewer.</summary>
        public Thickness ScrollViewerBorderThickness{get{return IsFillMode ? new Thickness(0) : new Thickness(1);}}
        #endregion

        #region Properties - Internal
        private ViewTestAttribute CurrentViewTestAttribute { get { return model.CurrentViewTest == null ? null : model.CurrentViewTest.Attribute; } }

        private bool IsFillMode
        {
            get
            {
                var mode = SizeMode;
                return mode == TestControlSize.Fill || mode == TestControlSize.FillWithMargin;
            }
        }
        private bool IsFillWithMargin { get { return SizeMode == TestControlSize.FillWithMargin; } }

        private TestControlSize SizeMode
        {
            get
            {
                // Check specific [ViewTest] first.
                if (CurrentViewTestAttribute != null)
                {
                    var testMode = CurrentViewTestAttribute.SizeMode;
                    if (testMode != TestControlSize.Default) return testMode;
                }

                // Bubble up to the parent.
                return model.Attribute.SizeMode;
            }
        }
        #endregion

        public class DisplayItemViewModel : ViewModelBase
        {
            #region Head
            private readonly DisplayContainerViewModel parent;
            private readonly ControlDisplayOptionSettings displaySettings;
            private readonly PropertyObserver<ControlDisplayOptionSettings> displaySettingsObserver;
            private readonly PropertyObserver<ViewTestClass> viewTestClassObserver;
            private Border itemsControlBorder;

            public DisplayItemViewModel(DisplayContainerViewModel parent, UIElement control)
            {
                // Setup initial conditions.
                this.parent = parent;
                displaySettings = TestHarnessModel.Instance.Settings.ControlDisplayOptionSettings;

                // Ensure the item is not already within the visual tree.
                var parentBorder = control.GetParentVisual() as Border;
                if (parentBorder != null) parentBorder.Child = null;

                // Create it's container.
                Control = control;
                ControlContainer = new Border { Child = control };

                // Wire up events.
                ControlContainer.Loaded += delegate { InitializeContainerSize(); };
                viewTestClassObserver = new PropertyObserver<ViewTestClass>(parent.model)
                    .RegisterHandler(m => m.CurrentViewTest, m => InitializeContainerSize());
                displaySettingsObserver = new PropertyObserver<ControlDisplayOptionSettings>(displaySettings)
                    .RegisterHandler(s => s.ShowBorder, s => OnPropertyChanged<DisplayItemViewModel>(m => m.Border));
            }

            protected override void OnDisposed()
            {
                // Remove the control from the visual tree.
                ControlContainer.Child = null;
                displaySettingsObserver.Dispose();
                viewTestClassObserver.Dispose();
                WireUpEvents(false);

                // Finish up.
                base.OnDisposed();
            }
            #endregion

            #region Properties
            public UIElement Control { get; private set; }
            public Border ControlContainer { get; private set; }
            public Thickness Border
            {
                get
                {
                    return !displaySettings.ShowBorder || (parent.IsFillMode && !parent.IsFillWithMargin)
                                ? new Thickness(0) 
                                : new Thickness(1);
                }
            }
            public Thickness Margin
            {
                get
                {
                    return parent.IsFillMode
                        ? new Thickness(0)
                        : new Thickness(0, 20, 0, 20);
                }
            }
            #endregion

            #region Properties - Internal
            private Border ItemsControlBorder
            {
                get
                {
                    return itemsControlBorder ?? (itemsControlBorder = ControlContainer.FindByName(ItemsControlBorderElementName) as Border);
                }
            }
            #endregion

            #region Internal
            private void InitializeContainerSize()
            {
                // Update size.
                if (parent.IsFillMode)
                {
                    WireUpEvents(false);
                    WireUpEvents(true);
                    SetContainerSizeToFill();
                }
                else
                {
                    WireUpEvents(false);
                    ControlContainer.Width = double.NaN;
                    ControlContainer.Height = double.NaN;
                }

                // Finish up.
                OnPropertyChanged<DisplayItemViewModel>(m => m.Border, m => m.Margin);
            }

            private void WireUpEvents(bool addHandler)
            {
                if (ItemsControlBorder == null) return;
                if (addHandler)
                {
                    ItemsControlBorder.SizeChanged += OnItemsControlBorderSizeChanged;
                }
                else
                {
                    ItemsControlBorder.SizeChanged -= OnItemsControlBorderSizeChanged;
                }
            }

            private void OnItemsControlBorderSizeChanged(object sender, SizeChangedEventArgs e)
            {
                SetContainerSizeToFill();
            }

            private void SetContainerSizeToFill()
            {
                // Setup initial conditions.
                if (ItemsControlBorder == null) return;

                // Calculate size.
                var width = ItemsControlBorder.ActualWidth;
                var height = ItemsControlBorder.ActualHeight;
                if (parent.IsFillWithMargin)
                {
                    width -= 80;
                    height -= 80;
                }

                // Apply size.
                ControlContainer.Width = width;
                ControlContainer.Height = height;
            }
            #endregion
        }
    }
}
