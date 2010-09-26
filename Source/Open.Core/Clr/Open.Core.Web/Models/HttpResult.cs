using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Open.Core.Web
{
    /// <summary>The result content from an HTTP get operation.</summary>
    public class HttpResult
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="contentType">The HTTP content-type value.</param>
        /// <param name="content">The content string</param>
        public HttpResult(string contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }
        #endregion

        #region Properties
        /// <summary>Gets the HTTP content-type value.</summary>
        public string ContentType { get; private set; }

        /// <summary>Gets the content string.</summary>
        public string Content { get; private set; }
        #endregion
    }
}
