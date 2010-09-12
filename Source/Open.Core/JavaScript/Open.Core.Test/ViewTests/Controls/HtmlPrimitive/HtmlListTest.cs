using System;
using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.HtmlPrimitive
{
    public class HtmlListTest
    {
        #region Head
        private HtmlList list;
        private int count = 0;

        public void ClassInitialize()
        {
            list = new HtmlList(HtmlListType.Unordered, "myListClass");
            list.Container.CSS(Css.Background, "orange");
            list.Container.Width(300);
            TestHarness.AddControl(list);
        }
        public void ClassCleanup() { }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests
        public void Add()
        {
            count++;
            string text = "Item " + count;

            jQueryObject ulItem = list.Add(text, "myClass1 myClass2");
            Log.Info("Inserted item: " + Html.ToHtml(ulItem).HtmlEncode());

            Write_Properties();
        }

        public void RemoveAt_Zero()
        {
            list.Remove(0);
            Write_Properties();
        }

        public void RemoveLast()
        {
            if (list.Last != null) list.Last.Remove();
            Write_Properties();
        }

        public void Clear()
        {
            list.Clear();
            Write_Properties();
        }

        public void MyERROR()
        {
            Log.Info("Throwing error now.");
            throw new Exception("Hello"); //TEMP 
        }

        public void Write_Properties()
        {
            Log.Info("Count: " + list.Count);
            Log.Info("ListType: " + list.ListType.ToLocaleString());
            Log.Info("First: " + ItemToString(list.First));
            Log.Info("Last: " + ItemToString(list.Last));
            Log.Info("InnerHtml: " + list.InnerHtml.HtmlEncode());
            Log.Info("OuterHtml: " + list.OuterHtml.HtmlEncode());
        }

        private static string ItemToString(jQueryObject li)
        {
            return Helper.String.FormatToString(li, delegate(object o) { return li.GetHtml(); });
        }
        #endregion
    }
}