using System;
using jQueryApi;
using Open.Core;

namespace Open.TestHarness.Log
{
    public class LogView : ViewBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="divLogList">The container of the log</param>
        public LogView(jQueryObject divLogList)
        {
            Initialize(divLogList);
        }
        #endregion

        #region Methods
        /// <summary>Appends the given message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        public void Write(string message)
        {
            jQueryObject div = Html.CreateElement(Html.Div);
            div.AddClass(CssSelectors.ClassLogListItem);
            div.Append(message);
            div.AppendTo(Container);
        }
        #endregion
    }
}
