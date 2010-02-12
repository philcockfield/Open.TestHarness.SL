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
        private readonly ViewTestClass model;
        private readonly PropertyObserver<ViewTestClass> modelObserver;

        public DisplayContainerViewModel(ViewTestClass model)
        {
            // Setup initial conditions.
            this.model = model;

            // Create wrapper collection.
            CurrentControls = new ObservableCollectionWrapper<object, DisplayItemViewModel>(
                                                model.CurrentControls,
                                                control => new DisplayItemViewModel(control as UIElement));

            // Wire up events.
            modelObserver = new PropertyObserver<ViewTestClass>(model)
                .RegisterHandler(m => m.CurrentViewTest, m => OnPropertyChanged<T>(o => o.CurrentControls));
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            CurrentControls.Dispose();
            modelObserver.Dispose();
        }
        #endregion

        #region Properties
        /// <summary>Gets the display name of the class.</summary>
        public string DisplayName
        {
            get { return model.DisplayName; }
        }

        /// <summary>Gets the current set of controls pertaining the the current [ViewTest].</summary>
        public ObservableCollectionWrapper<object, DisplayItemViewModel> CurrentControls { get; private set; }
        #endregion

        public class DisplayItemViewModel : ViewModelBase
        {
            #region Head
            private readonly ControlDisplayOptionSettings displaySettings;
            private readonly PropertyObserver<ControlDisplayOptionSettings> displaySettingsObserver;

            public DisplayItemViewModel(UIElement control)
            {
                // Setup initial conditions.
                displaySettings = TestHarnessModel.Instance.Settings.ControlDisplayOptionSettings;
                displaySettingsObserver = new PropertyObserver<ControlDisplayOptionSettings>(displaySettings)
                    .RegisterHandler(s => s.ShowBorder, s => OnPropertyChanged<DisplayItemViewModel>(m => m.Border));

                // Ensure the item is not already within the visual tree.
                var parentBorder = control.GetParentVisual() as Border;
                if (parentBorder != null) parentBorder.Child = null;

                // Create it's container.
                Control = control;
                ControlContainer = new Border { Child = control };
            }

            protected override void OnDisposed()
            {
                base.OnDisposed();

                // Remove the control from the visual tree.
                ControlContainer.Child = null;
                displaySettingsObserver.Dispose();
            }
            #endregion

            #region Properties
            public UIElement Control { get; private set; }
            public Border ControlContainer { get; private set; }
            public Thickness Border { get { return displaySettings.ShowBorder ? new Thickness(1) : new Thickness(0); } }
            #endregion
        }
    }
}
