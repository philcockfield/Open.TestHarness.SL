using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class ButtonBaseTest
    {
        #region Head
        private MyButtonView view;
        private IButton model;


        public void ClassInitialize()
        {
            view = new MyButtonView();
            model = view.Model;
            model.Click += delegate { Log.Info("!! Click"); };
            model.IsPressedChanged += delegate { Log.Info("!! IsPressedChanged | IsPressed: " + model.IsPressed); };
//            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { Log.Info("!! View.PropertyChanged: " + args.Property.Name); };

            TestHarness.AddControl(view);
        }
        #endregion

        #region Methods
        public void Toggle_View__IsEnabled()
        {
            view.IsEnabled = !view.IsEnabled;
            Log.Info("View Toggled - IsEnabled: " + view.IsEnabled);
        }

        public void Toggle_Model__IsEnabled()
        {
            model.IsEnabled = !model.IsEnabled;
            Log.Info("Model Toggled - IsEnabled: " + model.IsEnabled);
        }

        public void Toggle__CanToggle()
        {
            model.CanToggle = !model.CanToggle;
            Log.Info("Toggled - CanToggle: " + model.CanToggle);
        }

        public void Toggle__IsPressed()
        {
            model.IsPressed = !model.IsPressed;
            Log.Info("Toggled - IsPressed: " + model.IsPressed);
        }

        public void GetSampleTemplate__Normal() { GetTemplate("#btnSample_Normal"); }
        public void GetSampleTemplate__Over() { GetTemplate("#btnSample_Over"); }

        private static void GetTemplate(string cssSelector)
        {
            string url = "/Samples/ButtonTemplate";
            Log.Info(string.Format("IsDownloaded: {0} (Url: {1})", Helper.Template.IsDownloaded(url), Html.ToHyperlink(url)));
            Log.Info("CSS Selector: " + cssSelector);
            Helper.Template.Get(url, cssSelector, delegate(Template template)
                            {
                                Log.Info(template.TemplateHtml.HtmlEncode());
                            });
        }

        public void Write_Properties()
        {
            Log.Title("Model");
            LogButtons.WriteButtonModel(model);
            Log.LineBreak();
            Log.Title("View");
            LogButtons.WriteButtonView(view);
        }
        #endregion
    }

    public class MyButtonView : ButtonView
    {
        public MyButtonView()
        {
            Background = "orange";
            SetSize(120, 34);
        }

        [AlternateSignature]
        public extern void TestStateContent(ButtonState state, jQueryObject html);

        [AlternateSignature]
        public extern void TestStateContent(ButtonState state, string cssClasses);
        
        public void TestStateContent(ButtonState state, jQueryObject html, string cssClasses) { base.StateContent(state, html, cssClasses); }
    }
}
