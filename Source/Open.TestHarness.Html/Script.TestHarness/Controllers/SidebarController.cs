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

        /// <summary>Constructor.</summary>
        public SidebarController()
        {
            // Setup initial conditions.
            view = Common.Shell.Sidebar;

            // Create child controllers.
            methodListController = new MethodListController();

            // Wire up events.
            TEMP();
        }

        protected override void OnDisposed()
        {
            view.Dispose();
            methodListController.Dispose();
            foreach (PackageController controller in packageControllers)
            {
                controller.Dispose();
            }
            base.OnDisposed();
        }

        private void TEMP( ) //TEMP - Add sample nodes to SidePanel.
        {
            MyNode rootNode = new MyNode("Root");
            view.RootList.RootNode = rootNode;

            rootNode.AddChild(new MyNode("Recent"));

            MyNode child1 = rootNode.ChildAt(0) as MyNode;
            MyNode child2 = ((MyNode)rootNode.ChildAt(1));
            MyNode child3 = ((MyNode)rootNode.ChildAt(2));

            child1.AddChild(new MyNode("Recent Child 1"));
            child1.AddChild(new MyNode("Recent Child 2"));
            child1.AddChild(new MyNode("Recent Child 3"));

            MyNode recent1 = child1.ChildAt(0) as MyNode;
            recent1.AddChild(new MyNode("Recent Grandchild 1"));
            recent1.AddChild(new MyNode("Recent Grandchild 2"));
            recent1.AddChild(new MyNode("Recent Grandchild 3"));
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
            view.RootList.RootNode.AddChild(node);

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

    public class MyNode : ListItem //TEMP - MyNode structure
    {
        public MyNode(string text) { Text = text; }

        public override string ToString() { return base.ToString(); }
    }

}