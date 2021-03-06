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

            Add();
            Add();
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

            jQueryObject ulItem = list.Add(text);
            Log.Info("Inserted item: " + Html.OuterHtml(ulItem).HtmlEncode());
        }

        public void AddElement()
        {
            jQueryObject div = Html.CreateDiv();
            div.CSS("border", "solid 1px green");
            div.CSS(Css.Margin, "5px");
            div.Append("DIV");
            list.AddElement(div);
        }

        public void RemoveAt_Zero()
        {
            list.Remove(0);
        }

        public void RemoveLast()
        {
            if (list.Last != null) list.Last.Remove();
        }

        public void Clear()
        {
            list.Clear();
        }

        public void MyERROR()
        {
            Log.Info("Throwing error now.");
            throw new Exception("Hello"); //TEMP 
        }

        public void Write_Properties()
        {
            Log.WriteProperties(list);
            //Log.Info("Count: " + list.Count);
            //Log.Info("ListType: " + list.ListType.ToLocaleString());
            //Log.Info("First: " + ItemToString(list.First));
            //Log.Info("Last: " + ItemToString(list.Last));
            //Log.Info("InnerHtml: " + list.InnerHtml.HtmlEncode());
            //Log.Info("OuterHtml: " + list.OuterHtml.HtmlEncode());
        }

        private static string ItemToString(jQueryObject li)
        {
            return Helper.String.FormatToString(li, delegate(object o) { return li.GetHtml().HtmlEncode(); });
        }
        #endregion
    }
}