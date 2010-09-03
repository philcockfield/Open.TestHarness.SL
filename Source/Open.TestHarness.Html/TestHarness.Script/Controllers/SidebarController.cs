using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.TestHarness.Models;

namespace Open.TestHarness.Controllers
{
    /// <summary>Controller for the side-bar.</summary>
    public class SidebarController : ControllerBase
    {
        #region Head
        private readonly ListTreeView listTree;
        private readonly ListTreeBackController backController;
        private readonly ArrayList packageControllers = new ArrayList();

        /// <summary>Constructor.</summary>
        public SidebarController()
        {
            // Setup initial conditions.
            listTree = new ListTreeView(jQuery.Select(CssSelectors.SidebarList));
            listTree.SlideDuration = 0.2;

            // Create controllers.
            backController = new ListTreeBackController(
                listTree, 
                jQuery.Select(CssSelectors.SidebarToolbar), 
                jQuery.Select(CssSelectors.BackMask));


            TEMP();

        }

        protected override void OnDisposed()
        {
            backController.Dispose();
            foreach (TestPackageController controller in packageControllers)
            {
                controller.Dispose();
            }
            base.OnDisposed();
        }

        private void TEMP( ) //TEMP 
        {
            MyNode rootNode = new MyNode("Root");
            rootNode.AddChild(new MyNode("Recent"));
            rootNode.AddChild(new MyNode("Child 2 (Can't Select)"));
            rootNode.AddChild(new MyNode("Child 3"));

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

            child2.AddChild(new MyNode("Yo Child"));
            child3.AddChild(new MyNode("Yo Child"));

            listTree.RootNode = rootNode;

            // ---

            //MyNode insert1 = new MyNode("Inserted 1");
            //MyNode insert2 = new MyNode("Inserted 2");
            //MyNode insert3 = new MyNode("Inserted 3");
            //ArrayList inserts = new ArrayList();
            //inserts.Add(insert1);
            //inserts.Add(insert2);
            //inserts.Add(insert3);

            //rootNode.AddChild(insert1);
            //rootNode.InsertChild(1, insert2);

            //Log.LineBreak();
            //rootNode.RemoveChild(insert2);
//            rootNode.RemoveChild(insert1);

            // ---

            child1.Text = "My Recent Foo";
            child2.CanSelect = false;
            child2.IsSelected = true;
        }
        #endregion

        #region Methods
        /// <summary>Adds a test-package to the controller.</summary>
        /// <param name="testPackage">The test-package to add.</param>
        public void AddPackage(TestPackageDef testPackage)
        {
            // Setup initial conditions.
            if (testPackage == null) return;

            // Create the list-item node and insert it within the tree.
            TestPackageListItem node = new TestPackageListItem(testPackage);
            listTree.RootNode.AddChild(node);

            // Create the controller.
            TestPackageController controller = new TestPackageController(node);
            packageControllers.Add(controller);

            controller.Loaded += delegate
                                     {
                                         listTree.CurrentListRoot = controller.RootNode;
                                     };

        }
        #endregion
    }

    public class MyNode : ListItem //TEMP 
    {
        public MyNode(string text) { Text = text; }

        public override string ToString() { return base.ToString(); }
    }

}