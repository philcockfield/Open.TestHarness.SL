using System.IO;
using System.Web.Mvc;
using Microsoft.Http;

namespace Open.Core.Web.Controllers
{
    /// <summary>Controller for common re-usable actions.</summary>
    public class CoreController : ControllerBase
    {
        #region Head
        public const string Name = "Core";
        public const string ActionTestHarness = "TestHarness";
        public const string ActionRenderUrl = "RenderUrl";
        #endregion

        #region Methods
        /// <summary>The remote TestHarness at 'http://TestHarness.org'.</summary>
        public virtual ActionResult TestHarness() { return View(); }

        /// <summary>Renders the HTML from the specified remote URL.</summary>
        /// <param name="url">The URL to retrieve.</param>
        public virtual ActionResult RenderUrl(string url)
        {
            using (var client = new HttpClient(url))
            {
                using (var response = client.Get())
                {
                    response.EnsureStatusIsSuccessful();
                    StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                    ViewData["Html"] = reader.ReadToEnd();
                    reader.Dispose();
                }
            }
            return View();
        }
        #endregion
    }
}
