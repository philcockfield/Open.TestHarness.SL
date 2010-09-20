using System;
using System.Collections;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class TemplateTest
    {
        #region Head
        const string Url = "/Samples/Templates";
        private const string SelectorSample1 = "#template_sample1";
        private readonly string urlLink = Html.ToHyperlink(Url);
        private MyTemplateView view;

        public void ClassInitialize()
        {
            view = new MyTemplateView();
            TestHarness.AddControl(view);
        }
        #endregion

        #region Tests
        public void Download()
        {
            Log.Info("Downloading templates: " + urlLink);
            Helper.Template.Download(Url, delegate
                                              {
                                                  Log.Success("Downloaded. " + urlLink);
                                              });
        }

        public void IsDownloaded()
        {
            Log.Info("IsDownloaded: " + Helper.Template.IsDownloaded(Url) + " | " + urlLink);
        }

        public void GetTemplate()
        {
            Log.Info(string.Format("Getting template '{0}' at {1}", SelectorSample1, urlLink));
            Helper.Template.Get(Url, SelectorSample1, delegate(Template template)
                                                          {
                                                              Log.Success("Retrieved. Template result: " + template.ToString());
                                                          });
        }

        public void RenderTemplate()
        {
            Log.Info("Rendering...");
            Helper.Template.Get(Url, SelectorSample1, delegate(Template template)
                    {
                        Log.Info("Template: " + template.ToString());
                        
                        Dictionary data= new Dictionary();
                        data["firstName"] = "Phil";

                        view.Container.Empty();
                        template.AppendTo(view.Container, data);

                        Log.Info("Rendered");
                    });
        }

        public void ToHtml()
        {
            Log.Info("Rendering...");
            Helper.Template.Get(Url, SelectorSample1, delegate(Template template)
                    {
                        Log.Info("Template: " + template.ToString());

                        Dictionary data = new Dictionary();
                        data["firstName"] = "Doug";
                        string html = template.ToHtml(data);

                        Log.Info("ToHtml: " + html.HtmlEncode());
                        Log.Info(html);
                        view.Container.Empty();
                        view.Container.Append(html);
                    });
            
        }
        #endregion
    }

    public class MyTemplateView : ViewBase
    {
        public MyTemplateView()
        {
            SetSize(400, 200);
            Background = Color.Black(0.05);
        }
    }
}
