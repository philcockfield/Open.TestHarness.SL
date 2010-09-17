using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple hyperlink that acts like a button.</summary>
    public class LinkButton : ButtonBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="htmlContent">The HTML content of the button.</param>
        public LinkButton(string htmlContent): base(InitHtml(htmlContent))
        {
            // Suppress the browser treating the ANCHOR like a link.
            Container.Click(delegate(jQueryEvent e) { e.PreventDefault(); });
        }

        private static jQueryObject InitHtml(string htmlContent)
        {
            jQueryObject htmLink = Html.CreateElement(Html.Anchor);
            htmLink.Append(htmlContent);
            htmLink.Attribute(Html.Href, "#");
            return htmLink;
        }
        #endregion
    }
}
