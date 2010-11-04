using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.Core.Controls.Buttons;
using Open.Testing.Models;

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

        private readonly IconTextButton addButton;
        private readonly IconTextButton cancelButton;
        private bool isInitialized = false;
        private bool isShowing = false;

        /// <summary>Constructor.</summary>
        public AddPackageView()
        {
            // Create buttons.
            addButton = CreateButton(StringLibrary.Add);
            cancelButton = CreateButton(StringLibrary.Cancel);

            // Load the HTML content.
            RetrieveHtml(ContentUrl, delegate
                                         {
                                             // Setup children.
                                             divInnerSlide = jQuery.Select(CssSelectors.AddPackageInnerSlide);
                                             offLeft = divInnerSlide.GetCSS(Css.Left);
                                             InitializeTextboxes();

                                             // Insert buttons.
                                             addButton.CreateView().Insert(CssSelectors.AddPackageBtnAdd, InsertMode.Replace);
                                             cancelButton.CreateView().Insert(CssSelectors.AddPackageBtnCancel, InsertMode.Replace);

                                             // Wire up events.
                                             Keyboard.Keydown += OnKeydown;

                                             // Finish up.
                                             isInitialized = true;
                                             UpdateState();
                                             SlideOn(null);
                                         });
        }

        protected override void OnDisposed()
        {
            Keyboard.Keydown -= OnKeydown;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnKeydown(object sender, KeyEventArgs e)
        {
            if (isShowing && e.Key == Key.Esc)
            {
                CancelButton.InvokeClick(false);
            }
        }
        #endregion

        #region Properties
        public IButton AddButton { get { return addButton; } }
        public IButton CancelButton{get { return cancelButton; }}
        public bool IsPopulated { get { return txtScriptUrl.HasText && txtInitMethod.HasText; } }
        #endregion

        #region Methods
        /// <summary>Gets the package-info singleton from the currently text settings.</summary>
        /// <returns>The package-info, or null if the control is not populated.</returns>
        public PackageInfo GetPackageInfo()
        {
            if (!IsPopulated) return null;
            return PackageInfo.SingletonFromUrl(txtInitMethod.Text, txtScriptUrl.Text);
        }

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
            if (!isInitialized) return;
            FireShowing();
            Slide("0px", delegate
                             {
                                 isShowing = true;
                                 txtScriptUrl.Focus.Apply();
                                 Helper.Invoke(onComplete);
                             });
        }

        /// <summary>Slides the panel off screen.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void SlideOff(Action onComplete)
        {
            if (!isInitialized) return;
            Slide(offLeft, delegate
                               {
                                   isShowing = false;
                                   FireHidden();
                                   Helper.Invoke(onComplete);
                               });
        }

        /// <summary>Updates the state of the control and it's children.</summary>
        public void UpdateState()
        {
            if (!isInitialized) return;
            addButton.IsEnabled = IsPopulated;
        }
        #endregion

        #region Internal
        private void InitializeTextboxes()
        {
            txtScriptUrl = InitializeTextbox(CssSelectors.AddPackageTxtScript, IconJs);
            txtInitMethod = InitializeTextbox(CssSelectors.AddPackageTxtMethod, IconMethod);
        }

        private Textbox InitializeTextbox(string selector, string icon)
        {
            // Create and insert the textbox.
            Textbox textbox = new Textbox();
            textbox.Padding.Change(10, 5);
            textbox.LeftIcon = Helper.Url.PrependDomain(icon);
            textbox.Insert(selector, InsertMode.Replace);

            // Wire up events.
            textbox.TextChanged += delegate { UpdateState(); };
            textbox.EnterPress += delegate { AddButton.InvokeClick(false); };

            // Finish up.
            return textbox;
        }

        private static IconTextButton CreateButton(string text)
        {
            // Create and insert the button.
            IconTextButton button = new IconTextButton();
            button.Text = text;
            button.CanFocus = false;

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
