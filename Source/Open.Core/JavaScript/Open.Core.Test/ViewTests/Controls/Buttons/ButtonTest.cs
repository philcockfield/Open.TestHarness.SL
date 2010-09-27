using System;
using System.Collections;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class ButtonTest
    {
        #region Head
        public const string TemplateUrl = "/Samples/ButtonTemplate";
        public const string CssUrl = "/Content/Css/Sample.Button.css";

        private MyButtonView view;
        private IButton model;
        private bool isInitialized;
        private const string NotInitializedWarning = "Test class not initialized.";

        public void ClassInitialize()
        {
            Log.Info("Downloading sample button templates at: " + Html.ToHyperlink(TemplateUrl));
            Css.InsertLink(CssUrl);
            Helper.Template.Download(TemplateUrl, delegate
                                                      {
                                                          Log.Success("Templates downloaded");
                                                          view = new MyButtonView();
                                                          model = view.Model;
                                                          model.Click += delegate { Log.Info("!! Click"); };
                                                          model.IsPressedChanged += delegate
                                                                                        {
                                                                                            if (model.IsPressed)
                                                                                            {
                                                                                                Log.Warning("!! IsPressedChanged | IsPressed: " + model.IsPressed);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Log.Success("!! IsPressedChanged | IsPressed: " + model.IsPressed);
                                                                                            }
                                                                                        };
                                                          //            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { Log.Info("!! View.PropertyChanged: " + args.Property.Name); };
                                                          
                                                          TestHarness.AddControl(view);
                                                          isInitialized = true;
                                                      });
        }
        #endregion

        #region Methods
        public void Toggle_Model__IsEnabled()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            model.IsEnabled = !model.IsEnabled;
            Log.Info("Model Toggled - IsEnabled: " + model.IsEnabled);
        }

        public void Toggle__CanToggle()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            model.CanToggle = !model.CanToggle;
            Log.Info("Toggled - CanToggle: " + model.CanToggle);
        }

        public void Toggle__IsPressed()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            model.IsPressed = !model.IsPressed;
            Log.Info("Toggled - IsPressed: " + model.IsPressed);
        }

        public void Invalidate()
        {
            view.Invalidate();
        }

        public void Write_Properties()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            Log.Title("Model");
            LogButtons.WriteButtonModel(model);
            Log.LineBreak();
            Log.Title("View");
            LogButtons.WriteButtonView(view);
            Log.LineBreak();
            Log.Info(view.OuterHtml.HtmlEncode());
        }
        #endregion
    }

    public class MyButtonView : ButtonView
    {
        public MyButtonView() 
        {
            // Setup initial conditions.
            SetSize(180, 45);

            // Create state content.
            ButtonStateContent normal = new ButtonStateContent(new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver }, "btn_sample_defaultText", new Template("#btnSample_Normal"));
            ButtonStateContent down = new ButtonStateContent(new ButtonState[] { ButtonState.MouseDown }, null, new Template("#btnSample_Down"));
            ButtonStateContent overlay = new ButtonStateContent(ButtonView.AllStates, "btn_sample_overlay", new Template("#btnSample_Overlay"));

            // Assign states.
            AddStatesTemplate(0, AllStates, "#btnSample_Background");
            AddStateContent(1, normal);
            AddStateCss(1, ButtonState.MouseOver, "btn_sample_over");
            AddStateContent(1, down);
            AddStateTemplate(1, ButtonState.Pressed, "#btn_Sample_Pressed");
            AddStateContent(2, overlay);

            // Finish up.
            UpdateLayout();
        }
    }
}
