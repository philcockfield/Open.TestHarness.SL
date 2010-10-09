using System;
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
                                                          view.Text = "My Button Text";
//                                                          model.TemplateData["buttonText"] = "My Button Text";

                                                          //            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { Log.Info("!! View.PropertyChanged: " + args.Property.Name); };
                                                          WireClickEvents(model);
                                                          view.UpdateLayout();

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

        public void Toggle__CanFocus()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            model.CanFocus = !model.CanFocus;
            Log.Info("Toggled - CanFocus: " + model.CanFocus);
        }


        public void Write_Properties()
        {
            if (!isInitialized) { Log.Warning(NotInitializedWarning); return; }
            Log.Title("Model");
            WriteLogForButtons.WriteButtonModel(model);
            Log.LineBreak();
            Log.Title("View");
            WriteLogForButtons.WriteButtonView(view);
            Log.LineBreak();
            Log.Info(view.OuterHtml.HtmlEncode());
        }
        #endregion

        #region Methods : Static
        public static void WireClickEvents(IButton button)
        {
            button.Click += delegate { Log.Info("!! Click"); };
            button.IsPressedChanged += delegate
                                    {
                                        if (button.IsPressed)
                                        {
                                            Log.Warning("!! IsPressedChanged | IsPressed: " + button.IsPressed);
                                        }
                                        else
                                        {
                                            Log.Success("!! IsPressedChanged | IsPressed: " + button.IsPressed);
                                        }
                                    };
        }
        #endregion
    }

    public class MyButtonView : ButtonView
    {
        public MyButtonView() 
        {
            // Setup initial conditions.
            SetSize(180, 45);

            // Assign states.
            // All.
            TemplateForStates(0, AllStates, "#btnSample_Background");

            // Normal.
            TemplateForStates(1, new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver }, "#btnSample_Normal", EnabledCondition.EnabledOnly, FocusCondition.UnfocusedOnly);
            CssForStates(1, new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver }, "btn_sample_defaultText");

            // Pressed.
            CssForState(1, ButtonState.MouseOver, "btn_sample_over");
            TemplateForState(1, ButtonState.MouseDown, "#btnSample_Down");
            TemplateForState(1, ButtonState.Pressed, "#btn_Sample_Pressed");

            // Focused.
            CssForStates(0, AllStates, "btn_sample_focus", EnabledCondition.Either, FocusCondition.FocusedOnly);
            TemplateForStates(1, new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver }, "#btn_Sample_Focused", EnabledCondition.EnabledOnly, FocusCondition.FocusedOnly);

            // Disabled.
            TemplateForStates(3, AllStates, "#btnSample_Disabled", EnabledCondition.DisabledOnly, FocusCondition.Either);

            // Overlay.
            CssForStates(2, AllStates, "btn_sample_overlay");
            TemplateForStates(2, AllStates, "#btnSample_Overlay");

            // Finish up.
            UpdateLayout();
        }

        public string Text
        {
            get { return Model.TemplateData["buttonText"] as string; }
            set { Model.TemplateData["buttonText"] = value; }
        }

        public static void DownloadContent(Action onComplete)
        {
            Css.InsertLink(ButtonTest.CssUrl);
            Log.Info("Downloading template for button: " + Html.ToHyperlink(ButtonTest.TemplateUrl));
            Helper.Template.Download(ButtonTest.TemplateUrl, delegate
                                    {
                                        Log.Info("Template Downloaded. " + Html.ToHyperlink(ButtonTest.TemplateUrl));
                                        Helper.Invoke(onComplete);
                                    });
        }
    }
}
