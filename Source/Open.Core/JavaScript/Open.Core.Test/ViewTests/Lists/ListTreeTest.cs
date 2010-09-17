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
            // Setup initial conditions.
            listTree = new ListTreeView(Html.CreateDiv());
            listTree.SetSize(250, 500);
            listTree.Background = "#f2f2f2";
            TestHarness.AddControl(listTree);

            // Wire up events.
            listTree.SelectedNodeChanged += delegate { Log.Info("!! SelectedNodeChanged | SelectedNode: " + listTree.SelectedNode); };
            listTree.SelectedParentChanged += delegate { Log.Info("!! SelectedParentChanged | SelectedParent: " + listTree.SelectedParent); };
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
        public void Back() { listTree.Back(); }
        public void Home() { listTree.Home(); }

        public void GetDescendent()
        {
            if (listTree.RootNode == null) AddNodes();
            ITreeNode node = Helper.Tree.FirstDescendent(listTree.RootNode, delegate(object o)
                                                        {
                                                            SampleListItem n = (SampleListItem) o;
                                                            return n.Text == "Great Grandchild 2";
                                                        });
            Log.Info(node);
        }

        public void GetDescendent_NotFound()
        {
            if (listTree.RootNode == null) AddNodes();
            ITreeNode node = Helper.Tree.FirstDescendent(listTree.RootNode, delegate(object o)
                                                    {
                                                        SampleListItem n = (SampleListItem)o;
                                                        return n.Text == "Foo";
                                                    });
            Log.Info(node);
        }
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
            recent1.AddChild(new SampleListItem("Grandchild 1"));
            recent1.AddChild(new SampleListItem("Grandchild 2"));
            recent1.AddChild(new SampleListItem("Grandchild 3"));

            SampleListItem recent2 = recent1.ChildAt(0) as SampleListItem;
            recent2.AddChild(new SampleListItem("Great Grandchild 1"));
            recent2.AddChild(new SampleListItem("Great Grandchild 2"));
        }
        #endregion
    }

}