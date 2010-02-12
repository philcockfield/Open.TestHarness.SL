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
using System.Linq;
using System.Windows.Controls;
using Open.Core.Assets;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;

using T = Open.TestHarness.View.Selector.TestSelectorViewModel;

namespace Open.TestHarness.View.Selector
{
    /// <summary>View model for the test-selector.</summary>
    public class TestSelectorViewModel : ViewModelBase
    {
        #region Head
        private ViewTestClass model;
        private ObservableCollectionWrapper<ViewTest, ViewTestButtonViewModel> viewTests;
        private DelegateCommand<Button> reloadClick;
        #endregion

        #region Properties
        /// <summary>Gets or sets the currently selected class.</summary>
        public ViewTestClass Model
        {
            get { return model; }
            set
            {
                // Setup initial conditions.
                if (value == Model) return;

                // Store values.
                model = value;
                viewTests = null;

                // Finish up.
                OnPropertyChanged<T>(m => m.Model, m => m.ViewTests, m => m.IsViewTestsVisible, m => m.IsReloadButtonVisible);
            }
        }

        /// <summary>Gets the collection of view-model wrappers representing each [ViewTest].</summary>
        public ObservableCollectionWrapper<ViewTest, ViewTestButtonViewModel> ViewTests
        {
            get
            {
                if (viewTests == null && Model != null)
                    viewTests = new ObservableCollectionWrapper<ViewTest, ViewTestButtonViewModel>(
                                                        model.ViewTests, 
                                                        item => new ViewTestButtonViewModel(item));
                return viewTests;
            }
        }

        /// <summary>Gets the refresh button command.</summary>
        public DelegateCommand<Button> ReloadClick
        {
            get { return reloadClick ?? (reloadClick = new DelegateCommand<Button>(button => model.Reload())); }
        }

        /// <summary>Gets or sets the refresh label.</summary>
        public string ReloadLabel { get { return StringLibrary.Common_Reload; } }

        /// <summary>Gets the visibility of the collection of tests.</summary>
        public bool IsViewTestsVisible
        {
            get
            {
                if (Model == null || Model.ViewTests.Count == 0) return false;
                return ViewTests.Count(item => item.Visibility == Visibility.Visible) > 0;
            }
        }

        /// <summary>Gets whether the 'Reload' button is visible.</summary>
        public bool IsReloadButtonVisible { get { return Model != null; } }
        #endregion
    }
}
