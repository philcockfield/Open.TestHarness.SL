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
        protected readonly jQueryObject clickMask;
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
        /// <param name="clickMask">The element used catch mouse events.</param>
        protected ButtonView(IButton model, jQueryObject container, jQueryObject clickMask) : base(InitContainer(container))
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(model)) model = new ButtonModel();
            this.model = model;
            this.clickMask = clickMask;

            // Setup CSS on container.
            Container.AddClass(ClassButton);
            Container.AddClass(Css.Classes.NoSelect);
            Focus.BrowserHighlighting = false;

            // Make the container position relative (if it doesn't have an explicit position set).))
            if (string.IsNullOrEmpty(Container.GetCSS(Css.Position)))
            {
                Container.CSS(Css.Position, Css.Relative);
            }

            // Setup the event monitor.
            if (Script.IsNullOrUndefined(clickMask)) clickMask = Container;
            eventController = new ButtonEventController(this, clickMask);

            // Wire up events.
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
            Helper.UnlistenPropertyChanged(Model, OnModelPropertyChanged);
            if (eventController != null) eventController.Dispose();
            base.OnDisposed();
        }

        private static jQueryObject InitContainer(jQueryObject container)
        {
            if (Script.IsNullOrUndefined(container)) container = Html.CreateDiv();
            return container;
        }
        #endregion

        #region Event Handlers
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
            if (name == ButtonModel.PropWidth) SyncDimension(SizeDimension.Width);
            if (name == ButtonModel.PropHeight) SyncDimension(SizeDimension.Height);
            if (name == ButtonModel.PropIsEnabled)
            {
                UpdateLayout();
                FirePropertyChanged(PropIsEnabled);
            }
            if (name == ButtonModel.PropCanFocus) SyncCanFocus();
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
        /// <summary>Updates the visual state of the button.</summary>
        public void UpdateLayout()
        {
            Css.SetOpacity(Container, Model.IsEnabled ? 1 : DisabledOpacity);
            OnUpdateLayout();
            SyncSize();
        }

        /// <summary>Implement layout logic in deriving classes.</summary>
        protected virtual void OnUpdateLayout(){}

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
                if (size.Width != ButtonModel.NoSize) Width = size.Width;
            }

            // Height.
            if (dimension == SizeDimension.Height)
            {
                if (size.Height != ButtonModel.NoSize) Height = size.Height;
            }
        }
        #endregion

        #region Methods : Protected
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

        #region Internal
        protected jQueryObject CreateAndAppendContainer(string cssClass)
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