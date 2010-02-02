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
using Open.Core.Common;
using Open.TestHarness.Model;
using Open.TestHarness.View.AssemblyChooser;
using Open.TestHarness.View.ControlHost;
using Open.TestHarness.View.FooterPanel;
using Open.TestHarness.View.PropertyGrid;
using Open.TestHarness.View.Selector;

namespace Open.TestHarness.View
{
    /// <summary>Logical representation the TestHarness.</summary>
    public class RootViewModel : ViewModelBase
    {
        #region Head
        public const string PropNavigationPaneWidth = "NavigationPaneWidth";
        public const string PropCurrentClass = "CurrentClass";

        private readonly TestHarnessModel model;
        private SelectorPanelViewModel selectorPanel;
        private ClientBinGridViewModel clientBinGrid;
        private PropertyExplorerPanelViewModel propertyExplorer;
        private GridLength navigationPaneWidth = new GridLength(300);
        private DisplayContainerViewModel currentClass;
        private readonly PropertyObserver<TestHarnessModel> modelObserver;

        public RootViewModel()
        {
            // Setup initial conditions.
            model = TestHarnessModel.Instance;

            // Create child view-models.
            DisplayOptionToolbar = new DisplayOptionToolbarViewModel();
            FooterPanelContainer = new FooterPanelContainerViewModel();

            // Wire up event handlers.
            modelObserver = new PropertyObserver<TestHarnessModel>(model)
                .RegisterHandler(m => m.CurrentClass, m =>
                                                          {
                                                              if (currentClass != null) currentClass.Dispose();
                                                              currentClass = null;
                                                              OnPropertyChanged(PropCurrentClass);
                                                          });
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            modelObserver.Dispose();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pixel width of the Navigation Pane.</summary>
        public GridLength NavigationPaneWidth
        {
            get { return navigationPaneWidth; }
            set
            {
                if (value == NavigationPaneWidth) return;
                navigationPaneWidth = value;
                OnPropertyChanged(PropNavigationPaneWidth);
            }
        }
        #endregion

        #region Properties : Child View-Models
        /// <summary>Gets the class selection view-model.</summary>
        public SelectorPanelViewModel SelectorPanel
        {
            get { return selectorPanel ?? (selectorPanel = new SelectorPanelViewModel(this)); }
        }

        /// <summary>Gets the view-model for the assembly chooser.</summary>
        public ClientBinGridViewModel ClientBinGrid
        {
            get { return clientBinGrid ?? (clientBinGrid = new ClientBinGridViewModel()); }
        }

        /// <summary>Gets the view-model for the property explorer.</summary>
        public PropertyExplorerPanelViewModel PropertyExplorer
        {
            get { return propertyExplorer ?? (propertyExplorer = new PropertyExplorerPanelViewModel()); }
        }

        /// <summary>Gets the view model for the current class (of null if there is no current class).</summary>
        public DisplayContainerViewModel CurrentClass
        {
            get
            {
                if (model.CurrentClass == null) return null;
                if (currentClass == null) currentClass = new DisplayContainerViewModel(model.CurrentClass);
                return currentClass;
            }
        }

        /// <summary>Gets the view-model for the display options toolbar.</summary>
        public DisplayOptionToolbarViewModel DisplayOptionToolbar { get; private set; }

        /// <summary>Gets the view-model for the middle footer panel.</summary>
        public FooterPanelContainerViewModel FooterPanelContainer{ get; private set; }
        #endregion
    }
}
