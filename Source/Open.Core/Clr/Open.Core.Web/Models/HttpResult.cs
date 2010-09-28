using System.Web.Mvc;

namespace Open.Core.Web
{
    /// <summary>The result content from an HTTP get operation.</summary>
    /// <remarks>This can be returned directly from a controller's Action.</remarks>
    public class HttpResult : ActionResult
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

        #region Methods
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            response.Write(Content);
        }
        #endregion
    }
}
