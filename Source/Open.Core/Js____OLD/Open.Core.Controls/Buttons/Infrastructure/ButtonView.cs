using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Helpers;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Base class for buttons.</summary>
    public abstract class ButtonView : ViewBase, IButtonView
    {
        #region Event Handlers
        /// <summary>Fires when the 'State' property changes.</summary>
        public event EventHandler StateChanged;
        private void FireStateChanged() { if (StateChanged != null) StateChanged(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropState = "State";
        public const string PropIsMouseOver = "IsMouseOver";
        public const string PropIsMouseDown = "IsMouseDown";
        public const string PropDisabledOpacity = "DisabledOpacity";
        private const double DefaultDisabledOpacity = 0.3;

        public const string ClassButton = "button";
        public const string ClassNormal = "normal";
        public const string ClassOver = "over";
        public const string ClassDown = "down";
        public const string ClassPressed = "pressed";

        public static readonly ButtonState[] AllStates = new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver, ButtonState.MouseDown, ButtonState.Pressed };
        public static readonly ButtonState[] DownAndPressed = new ButtonState[] { ButtonState.MouseDown, ButtonState.Pressed };
        public static readonly ButtonState[] NotDownOrPressed = new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver };

        private readonly IButton model;
        private readonly ButtonEventController eventController;
        private readonly ButtonContentController contentController;
        private readonly jQueryObject divContent;
        private TemplateLoader templateLoader;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        protected extern ButtonView();

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button (if ommited a default 'ButtonModel' is created and used).</param>
        [AlternateSignature]
        protected extern ButtonView(IButton model);

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button</param>
        /// <param name="container">The HTML container of the button.</param>
        protected ButtonView(IButton model, jQueryObject container) : base(InitContainer(container))
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(model)) model = new ButtonBase();
            this.model = model;
            Focus.BrowserHighlighting = false;

            // Setup CSS.
            Css.InsertLink(Css.Urls.CoreButtons);
            Container.AddClass(ClassButton);
            Container.AddClass(Css.Classes.NoSelect);

            // Insert the content and mask containers.
            divContent = CreateContainer("buttonContent");

            // Create child controllers.
            eventController = new ButtonEventController(this);
            contentController = new ButtonContentController(this, divContent);

            // Wire up events.
            Model.LayoutInvalidated += OnLayoutInvalidated;
            Helper.ListenPropertyChanged(Model, OnModelPropertyChanged);
            eventController.PropertyChanged += OnEventControllerPropertyChanged;
            GotFocus += delegate { UpdateLayout(); };
            LostFocus += delegate { UpdateLayout(); };
            IsEnabledChanged += delegate { UpdateLayout(); };
            Container.Keydown(delegate(jQueryEvent e)
                                  {
                                      OnKeyPress(Int32.Parse(e.Which));
                                  });

            // Finish up.
            SyncCanFocus();
        }


        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            Model.LayoutInvalidated -= OnLayoutInvalidated;
            Helper.UnlistenPropertyChanged(Model, OnModelPropertyChanged);
            eventController.Dispose();
            contentController.Dispose();
            base.OnDisposed();
        }

        private static jQueryObject InitContainer(jQueryObject container)
        {
            if (Script.IsNullOrUndefined(container)) container = Html.CreateDiv();
            return container;
        }
        #endregion

        #region Event Handlers
        private void OnLayoutInvalidated(object sender, EventArgs e) { UpdateLayout(); }

        private void OnKeyPress(int keyCode)
        {
            if (!IsEnabled) return;
            if (!Focus.IsFocused) return;
            if (!Model.InvokeKeyCodes.Contains(keyCode)) return;
            Model.InvokeClick(false);
        }

        private void OnEventControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name == PropState)
            {
                UpdateLayout();
                FireStateChanged();
            }
            FirePropertyChanged(e.Property.Name);
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.Property.Name;
            if (name == ButtonBase.PropWidth) SyncDimension(SizeDimension.Width);
            if (name == ButtonBase.PropHeight) SyncDimension(SizeDimension.Height);
            if (name == ButtonBase.PropIsEnabled)
            {
                UpdateLayout();
                FirePropertyChanged(PropIsEnabled);
            }
            if (name == ButtonBase.PropCanFocus) SyncCanFocus();
        }
        #endregion

        #region Properties
        // Use enabled state to of the button's model.
        public override bool IsEnabled
        {
            get { return model.IsEnabled; }
            set { model.IsEnabled = value; }
        }
        #endregion

        #region Properties : IButtonView
        public IButton Model { get { return model; } }
        public ButtonState State { get { return eventController.State; } }
        public bool IsMouseOver { get { return eventController.IsMouseOver; } }
        public bool IsMouseDown { get { return eventController.IsMouseDown; } }
        #endregion

        #region Properties : Internal
        /// <summary>Gets or sets the opacity of the button when it is disabled.</summary>
        protected double DisabledOpacity
        {
            get { return (double)Get(PropDisabledOpacity, DefaultDisabledOpacity); }
            set
            {
                value = Helper.NumberDouble.WithinBounds(value, 0, 1);
                if (Set(PropDisabledOpacity, value, DefaultDisabledOpacity))
                {
                    UpdateLayout();
                }
            }
        }
        #endregion

        #region Methods - Layout
        protected override void OnUpdateLayout()
        {
            Css.SetOpacity(Container, Model.IsEnabled ? 1 : DisabledOpacity);

            OnRendering();
            contentController.UpdateLayout();
            OnRendered();

            SyncSize();
        }

        /// <summary>Invoked immediately before the button has rendered it's state.</summary>
        protected virtual void OnRendering() { }

        /// <summary>Invoked immediately after the button has rendered it's state.</summary>
        protected virtual void OnRendered() { }

        /// <summary>Syncs the size of the button with the model (if it has size values.  See 'NoSize' constant).</summary>
        protected void SyncSize()
        {
            SyncDimension(SizeDimension.Width);
            SyncDimension(SizeDimension.Height);
        }

        /// <summary>Syncs the specified of the button with the model (if it has size values.  See 'NoSize' constant).</summary>
        /// <param name="dimension">The size dimension to sync.</param>
        protected void SyncDimension(SizeDimension dimension)
        {
            // Setup initial conditions.
            ISize size = Model as ISize;
            if (size == null) return;

            // Width.
            if (dimension == SizeDimension.Width)
            {
                if (size.Width != ButtonBase.NoSize) Width = size.Width;
            }

            // Height.
            if (dimension == SizeDimension.Height)
            {
                if (size.Height != ButtonBase.NoSize) Height = size.Height;
            }
        }
        #endregion

        #region Methods : Protected
        /// <summary>Clears all CSS and Template content which has been added to the button.</summary>
        protected void ClearContent()
        {
            contentController.Clear();
        }

        /// <summary>Adds a template to the list of resoures to download (if it's not already available in the page).</summary>
        /// <param name="selector">The CSS selector for the template.</param>
        /// <param name="url">The URL to download the template(s) from.</param>
        /// <remarks>Aftering adding one or more Templates Url's call the 'DownloadTemplates' method.</remarks>
        protected void AddRequiredTemplate(string selector, string url)
        {
            if (Helper.Template.IsAvailable(selector)) return;
            if (templateLoader == null) templateLoader = new TemplateLoader();
            templateLoader.AddUrl(url);
        }

        /// <summary>Downloads the set of required templates that were added via the 'AddRequiredTemplate' method.</summary>
        /// <param name="onComplete">Action which is invoked when the templates have completed downloading.</param>
        protected void DownloadTemplates(Action onComplete)
        {
            if (templateLoader == null)
            {
                Helper.Invoke(onComplete);
                return;
            }
            templateLoader.LoadComplete += delegate
                                               {
                                                   Helper.Invoke(onComplete);
                                                   templateLoader = null;
                                               };
            templateLoader.Start();
        }
        #endregion

        #region Methods : Protected (CSS)
        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        [AlternateSignature]
        public extern void CssForState(int layer, ButtonState state, string cssClasses);

        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        /// <param name="enabledCondition">The enabled-related conditions for which button content applies.</param>
        /// <param name="focusCondition">The focus-related conditions for which button content applies.</param>
        public void CssForState(int layer, ButtonState state, string cssClasses, EnabledCondition enabledCondition, FocusCondition focusCondition)
        {
            CssForStates(layer, new ButtonState[] { state }, cssClasses, enabledCondition, focusCondition);
        }

        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The state(s) the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        [AlternateSignature]
        public extern void CssForStates(int layer, ButtonState[] states, string cssClasses);

        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The state(s) the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        /// <param name="enabledCondition">The enabled-related conditions for which button content applies.</param>
        /// <param name="focusCondition">The focus-related conditions for which button content applies.</param>
        public void CssForStates(int layer, ButtonState[] states, string cssClasses, EnabledCondition enabledCondition, FocusCondition focusCondition)
        {
            contentController.AddCss(layer, new ButtonStateCss(states, cssClasses, enabledCondition, focusCondition));
        }
        #endregion

        #region Methods : Protected (Template)
        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        [AlternateSignature]
        public extern void TemplateForState(int layer, ButtonState state, string templateSelector);

        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        /// <param name="enabledCondition">The enabled-related conditions for which button content applies.</param>
        /// <param name="focusCondition">The focus-related conditions for which button content applies.</param>
        public void TemplateForState(int layer, ButtonState state, string templateSelector, EnabledCondition enabledCondition, FocusCondition focusCondition)
        {
            TemplateForStates(layer, new ButtonState[] { state }, templateSelector, enabledCondition, focusCondition);
        }

        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The states the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        [AlternateSignature]
        public extern void TemplateForStates(int layer, ButtonState[] states, string templateSelector);

        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The states the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        /// <param name="enabledCondition">The enabled-related conditions for which button content applies.</param>
        /// <param name="focusCondition">The focus-related conditions for which button content applies.</param>
        public void TemplateForStates(int layer, ButtonState[] states, string templateSelector, EnabledCondition enabledCondition, FocusCondition focusCondition)
        {
            contentController.AddTemplate(layer, new ButtonStateTemplate(states, new Template(templateSelector), enabledCondition, focusCondition));
        }
        #endregion

        #region Internal
        private jQueryObject CreateContainer(string cssClass)
        {
            jQueryObject div = Html.CreateDiv();
            Css.AbsoluteFill(div);
            div.AppendTo(Container);
            Css.AddClasses(div, cssClass);
            return div;
        }

        private void SyncCanFocus()
        {
            Focus.CanFocus = Model.CanFocus;
        }
        #endregion
    }
}