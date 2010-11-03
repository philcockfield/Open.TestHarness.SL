using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.Core.Controls.Buttons;

namespace Open.Testing.Views
{
    /// <summary>View for defining a new test-package to add to the side bar.</summary>
    public class AddPackageView : TestHarnessViewBase
    {
        #region Events
        /// <summary>Fires when the 'Add Package' view is shown (at start of slide animation).</summary>
        public static event EventHandler Showing;
        private static void FireShowing() { if (Showing != null) Showing(typeof(AddPackageView), new EventArgs()); }

        /// <summary>Fires when the 'Add Package' view is hidden (at end of slide animation).</summary>
        public static event EventHandler Hidden;
        private static void FireHidden() { if (Hidden != null) Hidden(typeof(AddPackageView), new EventArgs()); }
        #endregion

        #region Head
        private const string ContentUrl = "/Open.Core/TestHarness/AddPackage/";
        public static readonly string IconJs = Helper.Url.PrependDomain("/Open.Core/TestHarness/Images/Icon.JavaScript.png");
        public static readonly string IconMethod = Helper.Url.PrependDomain(ImagePaths.ApiIconPath + "Method.png");

        private const double SlideDuration = 0.25;
        private jQueryObject divInnerSlide;
        private string offLeft;

        private Textbox txtScriptUrl;
        private Textbox txtInitMethod;

        private IconTextButton btnAdd;
        private IconTextButton btnCancel;

        /// <summary>Constructor.</summary>
        public AddPackageView()
        {
            // Load the HTML content.
            RetrieveHtml(ContentUrl, delegate
                                         {
                                             divInnerSlide = jQuery.Select(CssSelectors.AddPackageInnerSlide);
                                             offLeft = divInnerSlide.GetCSS(Css.Left);
                                             InitializeTextboxes();
                                             InitializeButtons();
                                             SlideOn(null);
                                         });
        }
        #endregion

        #region Event Handlers
        private void OnAddClick(object sender, EventArgs e)
        {
            Log.Event("Add Click"); //TEMP 
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Log.Event("Cancel Click"); //TEMP 
        }
        #endregion

        #region Methods
        /// <summary>Inserts an instance of the view into the TestHarness' main canvas.</summary>
        public static AddPackageView AddToTestHarness()
        {
            // Create the view.
            AddPackageView view = new AddPackageView();

            // Add to the TestHarness.
            TestHarness.Reset();
            TestHarness.CanScroll = false;
            TestHarness.DisplayMode = ControlDisplayMode.Fill;
            TestHarness.AddControl(view);

            // Finish up.
            return view;
        }

        /// <summary>Slides the panel on screen.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void SlideOn(Action onComplete)
        {
            FireShowing();
            Slide("0px", onComplete);
        }

        /// <summary>Slides the panel off screen.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void SlideOff(Action onComplete)
        {
            Slide(offLeft, delegate
                               {
                                   FireHidden();
                                   Helper.Invoke(onComplete);
                               });
        }
        #endregion

        #region Internal
        private void InitializeTextboxes()
        {
            txtScriptUrl = InitializeTextbox(CssSelectors.AddPackageTxtScript, IconJs);
            txtInitMethod = InitializeTextbox(CssSelectors.AddPackageTxtMethod, IconMethod);
        }

        private static Textbox InitializeTextbox(string selector, string icon)
        {
            Textbox textbox = new Textbox();
            textbox.Padding.Change(10, 5);

            textbox.Insert(selector, InsertMode.Replace);
            textbox.LeftIcon = Helper.Url.PrependDomain(icon);

            return textbox;
        }


        private void InitializeButtons()
        {
            btnAdd = InitializeButton(CssSelectors.AddPackageBtnAdd, StringLibrary.Add, OnAddClick);
            btnCancel = InitializeButton(CssSelectors.AddPackageBtnCancel, StringLibrary.Cancel, OnCancelClick);
        }

        private static IconTextButton InitializeButton(string selector, string text, EventHandler handler)
        {
            // Create and insert the button.
            IconTextButton button = new IconTextButton();
            button.Text = text;
            button.CreateView().Insert(selector, InsertMode.Replace);

            // Wire up events.
            button.Click += handler;

            // Finish up.
            return button;
        }

        private void Slide(string left, Action onComplete)
        {
            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = left;

            // Perform animation.
            divInnerSlide.Animate(
                properties,
                Helper.Time.ToMsecs(SlideDuration),
                EffectEasing.Swing,
                delegate
                {
                    Helper.Invoke(onComplete);
                });
        }
        #endregion
    }
}
