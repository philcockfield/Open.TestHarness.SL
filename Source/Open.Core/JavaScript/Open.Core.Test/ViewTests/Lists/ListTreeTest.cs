using System;
using System.Collections;
using jQueryApi;
using Open.Core.Lists;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Lists
{
    public class ListTreeTest
    {
        #region Head
        private ListTreeView listTree;


        public void ClassInitialize()
        {
            listTree = new ListTreeView(Html.CreateDiv());

            //TEMP 
            listTree.Container.CSS(Css.Width, "250px");
            listTree.Container.CSS(Css.Height, "500px");
//            listTree.Container.CSS(Css.Background, "orange");

            listTree.Background = "#f2f2f2";

            TestHarness.AddControl(listTree, SizeMode.ControlsSize);


        }
        public void ClassCleanup()
        {
            listTree.Dispose();
        }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Methods
        public void AddNodes()
        {
            AddSampleNodes();
        }

        public void RootNode__Null() { listTree.RootNode = null; }
        #endregion

        #region Internal
        private void AddSampleNodes()
        {
            SampleListItem rootNode = new SampleListItem("Root");
            listTree.RootNode = rootNode;

            rootNode.AddChild(new SampleListItem("Child 1"));
            rootNode.AddChild(new SampleListItem("Child 2"));
            rootNode.AddChild(new SampleListItem("Child 3"));

            SampleListItem recent1 = rootNode.ChildAt(0) as SampleListItem;
            recent1.AddChild(new SampleListItem("Recent Grandchild 1"));
            recent1.AddChild(new SampleListItem("Recent Grandchild 2"));
            recent1.AddChild(new SampleListItem("Recent Grandchild 3"));
        }
        #endregion
    }

}