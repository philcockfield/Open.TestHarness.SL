using System;
using System.Collections;
using Open.Core.Lists;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Lists
{
    public class ListViewTest
    {
        #region Head
        private ListView view;
        private int addCount;

        public void ClassInitialize()
        {
            view = new ListView();
            TestHarness.AddControl(view);

            view.SetSize(300, 350);
            view.Background = Color.Black(0.05);

            Add_5_Items();
        }
        #endregion

        #region Methods
        public void Add_5_Items() { AddItems(5); }
        public void Add_10_Items() { AddItems(10); }
        public void Add_100_Items() { AddItems(100); }

        public void Custom_ItemFactory()
        {
            view.ItemFactory = ItemFactory;
            Add_5_Items();
        }

        public void Custom_ItemFactory__Null()
        {
            view.ItemFactory = null;
            Add_5_Items();
        }

        public void Clear()
        {
            view.Clear();
        }
        #endregion

        #region Internal
        private void AddItems(int total)
        {
            addCount++;
            SampleListItem root = SampleListItem.Create("Root", addCount + "] Item -", total - 1);
            root.AddChild(new CustomItem("Last Item (Custom)"));

            view.Load(root.Children);

            Log.Info("ChildCount: " + root.ChildCount);
            Log.Info("view.Count: " + view.Count);
        }

        private static IView ItemFactory(object model)
        {
            SampleListItem item = model as SampleListItem;
            return item.Text.EndsWith("3")
                                    ? new SampleItemView(model) as IView
                                    : new ListItemView(model) ;
        }
        #endregion
    }

    public class SampleItemView : ViewBase
    {
        public SampleItemView(object model)
        {
            Container.Append("Custom");

            SampleListItem item = model as SampleListItem;
            if (item != null) Container.Append(" - " + item.Text);

            Background = "orange";
            Height = 24;
        }
    }


    public class CustomItem : SampleListItem, IViewFactory
    {
        public CustomItem(string text) : base(text)
        {
        }

        public IView CreateView()
        {
            IView view = new SampleItemView(this);
            view.Background = Color.HotPink;
            view.SetCss(Css.Color, "white");
            return view;
        }
    }

}
