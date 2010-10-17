using jQueryApi;

namespace Open.Core.Controls.HtmlPrimitive
{
    /// <summary>An <UL></UL> or <OL></OL>.</summary>
    public interface IHtmlList
    {
        /// <summary>Gets the UL.</summary>
        jQueryObject Container { get; }

        /// <summary>Gets the number of items within the list.</summary>
        int Count { get; }

        /// <summary>Gets whether the list is empty.</summary>
        bool IsEmpty { get; }

        /// <summary>Gets the list-item at the given index.</summary>
        jQueryObject this[int index] { get; }

        /// <summary>Gets the HTML of the list.</summary>
        string OuterHtml { get; }

        /// <summary>Adds a new list item <li></li>.</summary>
        /// <param name="text">The text to insert within the element.</param>
        /// <returns>The LI element.</returns>
        jQueryObject Add(string text);

        /// <summary>Adds a new element within an <li></li> item.</summary>
        /// <param name="element">The element to add (within the LI).</param>
        /// <returns>The LI element.</returns>
        jQueryObject AddElement(jQueryObject element);

        /// <summary>Removes the item at the given index.</summary>
        /// <param name="index">The index to remove.</param>
        void Remove(int index);

        /// <summary>Removes all child LI items.</summary>
        void Clear();
    }
}