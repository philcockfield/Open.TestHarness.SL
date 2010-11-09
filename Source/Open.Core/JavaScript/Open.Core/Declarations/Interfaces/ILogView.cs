using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;

namespace Open.Core
{
    /// <summary>A visual representation of a log.</summary>
    public interface ILogView
    {
        /// <summary>Gets or sets whether the log automatically scrolls to the bottom when new data is added.</summary>
        bool AutoScroll { get; set; }

        /// <summary>Appends the given message to the log.</summary>
        /// <param name="message">The message to write (can be HTML).</param>
        /// <param name="cssClass">The CSS class to apply to log item.</param>
        /// <param name="backgroundColor">The background color to apply to the entry.</param>
        /// <param name="iconPath">The path and name of an icon to prepend the message with.</param>
        void InsertMessage(object message, string cssClass, string backgroundColor, string iconPath);

        /// <summary>Appends the given element to the log.</summary>
        /// <param name="element">The element to insert.</param>
        /// <param name="cssClass">The CSS class to apply to log item.</param>
        /// <param name="backgroundColor">The background color to apply to the entry.</param>
        void InsertElement(jQueryObject element, string cssClass, string backgroundColor);

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <param name="cssClass">The CSS class to apply to log item.</param>
        /// <param name="backgroundColor">The background color to apply to the item</param>
        /// <param name="iconPath">The path and name of an icon to prepend the message with.</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        IHtmlList InsertList(string title, string cssClass, string backgroundColor, string iconPath);

        /// <summary>Inserts a visual divider into the log.</summary>
        /// <param name="type">The type of divider to insert.</param>
        void Divider(LogDivider type);

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}