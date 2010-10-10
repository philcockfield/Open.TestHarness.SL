using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>The logical controller for a view (visual UI control) contained with an HTML element.</summary>
    public interface IView
    {
        #region Events
        /// <summary>Fires when the enabled state of the control changes.</summary>
        event EventHandler IsEnabledChanged;

        /// <summary>Fires when the IsVisible property changes.</summary>
        event EventHandler IsVisibleChanged;

        /// <summary>Fires when the size changes.</summary>
        event EventHandler SizeChanged;

        /// <summary>Fires when the control recieves keyboard focus.</summary>
        event EventHandler GotFocus;

        /// <summary>Fires when the control loses keyboard focus (blur).</summary>
        event EventHandler LostFocus;
        #endregion

        #region Main
        /// <summary>Gets the element that the control is contained within.</summary>
        jQueryObject Container { get; }

        /// <summary>Gets the complete HTML of the control.</summary>
        string OuterHtml { get; }

        /// <summary>Gets the inner HTML of the control (the html within the Container, but not including the container's tag).</summary>
        string InnerHtml { get; }

        /// <summary>Destroys the control and clears resources.</summary>
        void Dispose();

        /// <summary>Gets the contorl's focus options.</summary>
        IFocus Focus { get; }
        #endregion

        #region State
        /// <summary>Gets or sets whether the control is enabled.</summary>
        bool IsEnabled { get; set; }

        /// <summary>Gets or sets whether the control is visible.</summary>
        bool IsVisible { get; set; }
        #endregion

        #region Styles
        /// <summary>Gets or sets the CSS position setting for the control.</summary>
        CssPosition Position { get; set; }

        /// <summary>Gets or sets the background CSS for the control.</summary>
        string Background { get; set; }

        /// <summary>Gets or sets the opacity percentage (0-1).</summary>
        double Opacity { get; set; }

        /// <summary>Gets the specified CSS value.</summary>
        /// <param name="attribute">The CSS attribute name.</param>
        /// <returns>The CSS value, or null if it has not been specified.</returns>
        string GetCss(string attribute);

        /// <summary>Assigns the specified CSS value.</summary>
        /// <param name="attribute">The CSS attribute name.</param>
        /// <param name="value">The CSS value.</param>
        void SetCss(string attribute, string value);

        /// <summary>Gets the specified HTML attribute value from the container element.</summary>
        /// <param name="attribute">The attribute name.</param>
        /// <returns>The attribute value, or null if it has not been specified.</returns>
        string GetAttribute(string attribute);

        /// <summary>Assigns the specified HTML attribute value to the container element.</summary>
        /// <param name="attribute">The attribute name.</param>
        /// <param name="value">The value.</param>
        void SetAttribute(string attribute, string value);

        /// <summary>Finds the element at the given CSS selector and replaces it with this button.</summary>
        /// <param name="cssSeletor">The CSS selector of the element to replace.</param>
        /// <param name="mode">The strategy used for the insertion.</param>
        void Insert(string cssSeletor, InsertMode mode);
        #endregion
    }
}
