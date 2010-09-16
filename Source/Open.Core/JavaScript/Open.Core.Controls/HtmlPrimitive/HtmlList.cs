using System;
using System.Collections;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls.HtmlPrimitive
{
    /// <summary>Renders an <UL></UL> or <OL></OL>.</summary>
    public class HtmlList : ViewBase
    {
        #region Head
        private readonly HtmlListType listType;
        private ArrayList items = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="listType">The type of list to construct.</param>
        /// <param name="cssClass">The CSS class attribute to add to the root list element (can be multiple classes).</param>
        public HtmlList(HtmlListType listType, string cssClass)
            : base(Html.CreateElement(listType == HtmlListType.Unordered ? "ul" : "ol"))
        {
            // Setup initial conditions.
            this.listType = listType;

            // Create the root list element.
            Css.AddClasses(Container, cssClass);
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of list to construct.</summary>
        public HtmlListType ListType { get { return listType; } }

        /// <summary>Gets the number of items within the list.</summary>
        public int Count { get { return Container.Children().Length; } }

        /// <summary>Gets whether the list is empty.</summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>Gets the list-item at the given index.</summary>
        public jQueryObject this[int index]
        {
            get
            {
                if (index < 0) index = 0;
                if (IsEmpty || index >= Count) return null;
                return jQuery.FromElement(Container.Children().GetElement(index));
            }
        }

        public jQueryObject First { get { return this[0]; } }
        public jQueryObject Last { get { return this[Count - 1]; } }
        #endregion

        #region Methods
        /// <summary>Adds a new list item <li></li>.</summary>
        /// <param name="text">The text to insert within the element.</param>
        /// <returns>The LI element.</returns>
        [AlternateSignature]
        public extern jQueryObject Add(string text);

        /// <summary>Adds a new list item <li></li>.</summary>
        /// <param name="text">The text to insert within the element.</param>
        /// <param name="cssClass">The class(es) to apply to the LI.</param>
        /// <returns>The LI element.</returns>
        public jQueryObject Add(string text, string cssClass)
        {
            jQueryObject li = Html.CreateElement("li");
            if (!string.IsNullOrEmpty(text)) li.Append(string.Format("<p>{0}</p>", text));
            Css.AddClasses(li, cssClass);
            li.AppendTo(Container);
            FireSizeChanged();
            return li;
        }

        /// <summary>Removes the item at the given index.</summary>
        /// <param name="index">The index to remove.</param>
        public void Remove(int index)
        {
            jQueryObject item = this[index];
            if (item == null) return;
            item.Remove();
            FireSizeChanged();
        }

        /// <summary>Removes all child LI items.</summary>
        public void Clear()
        {
            if (IsEmpty) return;
            do
            {
                Remove(0);
            } while (!IsEmpty);
        }
        #endregion
    }
}