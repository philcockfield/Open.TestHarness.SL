using System;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;

namespace Open.TestHarness.Sidebar
{
    /// <summary>Controller for the side-bar.</summary>
    public class SidebarController : ControllerBase
    {
        #region Head
        private readonly ListTreeView listTree;
        private readonly ListTreeBackController backController;

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
                                                jQuery.Select(CssSelectors.HomeButton));


            TEMP();

        }

        protected override void OnDisposed()
        {
            backController.Dispose();
            base.OnDisposed();
        }

        private void TEMP( ) //TEMP 
        {
            MyNode rootNode = new MyNode("Root");
            rootNode.Add(new MyNode("Recent"));
            rootNode.Add(new MyNode("Child 2"));
            rootNode.Add(new MyNode("Child 3"));

            MyNode child1 = rootNode.ChildAt(0) as MyNode;
            MyNode child2 = ((MyNode)rootNode.ChildAt(1));
            MyNode child3 = ((MyNode)rootNode.ChildAt(2));

            child1.Add(new MyNode("Recent Child 1"));
            child1.Add(new MyNode("Recent Child 2"));
            child1.Add(new MyNode("Recent Child 3"));

            MyNode recent1 = child1.ChildAt(0) as MyNode;
            recent1.Add(new MyNode("Recent Grandchild 1"));
            recent1.Add(new MyNode("Recent Grandchild 2"));
            recent1.Add(new MyNode("Recent Grandchild 3"));

            child2.Add(new MyNode("Yo Child"));
            child3.Add(new MyNode("Yo Child"));

            listTree.RootNode = rootNode;

            // ---

            child1.Text = "My Recent Foo";


            child2.CanSelect = false;
            child2.IsSelected = true;
            child3.RightIconSrc = "http://www.feedicons.com/images/standard-icons.gif";


        }
        #endregion
    }

    public class MyNode : ListItem
    {
        public MyNode(string text)
        {
            Text = text;
        }
    }
}
