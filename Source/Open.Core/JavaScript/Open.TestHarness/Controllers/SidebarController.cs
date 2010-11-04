using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controller for the side-bar.</summary>
    public class SidebarController : TestHarnessControllerBase
    {
        #region Head
        private readonly ArrayList packageControllers = new ArrayList();
        private readonly SidebarView view;
        private readonly MethodListController methodListController;
        private readonly ListItem listRoot;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        public SidebarController()
        {
            // Setup initial conditions.
            events = Common.Events;
            listRoot = new ListItem();
            view = Common.Shell.Sidebar;
            view.RootList.RootNode = listRoot;

            // Create child controllers.
            methodListController = new MethodListController();

            // Insert the 'Add Package' list-item.
            listRoot.AddChild(new CustomListItem(CustomListItemType.AddPackage));

            // Wire up events.
            listRoot.ChildSelectionChanged += OnChildSelectionChanged;
            events.AddPackage += OnAddPackageRequest;
        }

        protected override void OnDisposed()
        {
            listRoot.ChildSelectionChanged -= OnChildSelectionChanged;
            events.AddPackage -= OnAddPackageRequest;
            view.Dispose();
            methodListController.Dispose();
            foreach (PackageController controller in packageControllers)
            {
                controller.Dispose();
            }
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private static void OnChildSelectionChanged(object sender, EventArgs e)
        {
            // An item in the root sidebar list has been selected.
            TestHarness.Reset();
        }

        private void OnAddPackageRequest(object sender, PackageEventArgs e)
        {
            AddPackage(e.PackageInfo);
        }
        #endregion

        #region Methods
        /// <summary>Adds a test-package to the controller.</summary>
        /// <param name="testPackage">The test-package to add.</param>
        public void AddPackage(PackageInfo testPackage)
        {
            // Setup initial conditions.
            if (testPackage == null) return;

            // Create the list-item node and insert it within the tree.
            PackageListItem node = new PackageListItem(testPackage);
            listRoot.InsertChild(listRoot.ChildCount == 0 ? 0 : listRoot.ChildCount - 1, node); // Insert before last node.

            // Create the controller.
            PackageController controller = new PackageController(node);
            packageControllers.Add(controller);
            controller.Loaded += delegate
                                     {
                                         // When the controller loads, reveal it in the list.
                                         view.RootList.SelectedParent = controller.RootNode;
                                     };
        }

        /// <summary>Removes the specified package.</summary>
        /// <param name="testPackage">The test-package to remove.</param>
        public void RemovePackage(PackageInfo testPackage)
        {
            // Setup initial conditions.
            if (testPackage == null) return;
            PackageController controller = GetController(testPackage);
            if (controller == null) return;

            // Remove from tree.
            view.RootList.RootNode.RemoveChild(controller.RootNode);

            // Finish up.
            Log.Info(string.Format("Test package unloaded: {0}", Html.ToHyperlink(testPackage.Id, null, LinkTarget.Blank)));
            Log.LineBreak();
        }
        #endregion

        #region Internal
        private PackageController GetController(PackageInfo testPackage)
        {
            return Helper.Collection.First(packageControllers, delegate(object o)
                                                {
                                                    return ((PackageController) o).TestPackage == testPackage;
                                                }) as PackageController; 
        }
        #endregion
    }
}