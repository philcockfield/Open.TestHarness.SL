using System;
using System.Runtime.CompilerServices;
using jQueryApi;

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
        public static readonly ButtonState[] AllStates = new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver, ButtonState.MouseDown, ButtonState.Pressed };

        private readonly IButton model;
        private readonly ButtonEventController eventController;
        private readonly ButtonContentController contentController;
        private readonly jQueryObject divMask;
        private readonly jQueryObject divContent;

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
            if (Script.IsNullOrUndefined(model)) this.model = new ButtonModel();
            Container.AddClass(Css.Classes.NoSelect);

            // Make the container position relative (if it doesn't have an explicit position set).
            if (string.IsNullOrEmpty(Container.GetCSS(Css.Position)))
            {
                Container.CSS(Css.Position, Css.Relative);
            }

            // Insert the content and mask containers.
            divContent = CreateContainer();
            divMask = CreateContainer();
            divMask.CSS(Css.Background, Css.Transparent); // NB: Mask prevents mouse events firing when child-elements of the content are mouse over/out.

            // Create child controllers.
            eventController = new ButtonEventController(this, divMask);
            contentController = new ButtonContentController(this, divContent);

            // Wire up events.
            eventController.PropertyChanged += OnEventControllerPropertyChanged;
        }

        /// <summary>Finalize.</summary>
        protected override void OnDisposed()
        {
            eventController.Dispose();
            contentController.Dispose();
            base.OnDisposed();
        }

        private static jQueryObject InitContainer(jQueryObject container)
        {
            if (Script.IsNullOrUndefined(container)) container = Html.CreateSpan();
            return container;
        }
        #endregion

        #region Event Handlers
        private void OnEventControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name == PropState)
            {
                UpdateLayout();
                FireStateChanged();
            }
            FirePropertyChanged(e.Property.Name);
        }
        #endregion

        #region Properties : IButtonView
        public IButton Model { get { return model; } }
        public ButtonState State { get { return eventController.State; } }
        public bool IsMouseOver { get { return eventController.IsMouseOver; } }
        public bool IsMouseDown { get { return eventController.IsMouseDown; } }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the button.</summary>
        public void UpdateLayout()
        {
            contentController.UpdateLayout();
        }

        /// <summary>Clears the visual cache of HTML and updates the layout.</summary>
        public void Invalidate()
        {
            contentController.InvalidateCache();
            UpdateLayout();
        }
        #endregion

        #region Methods : Protected
        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="content">The content for a given state.</param>
        protected void AddStateContent(int layer, ButtonStateContent content)
        {
            contentController.Add(layer, content);
        }

        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        protected ButtonStateContent AddStateCss(int layer, ButtonState state, string cssClasses)
        {
            return AddStatesCss(layer, new ButtonState[] { state }, cssClasses);
        }

        /// <summary>Sets the CSS class(es) to use for a given layer.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The state(s) the CSS classes apply to.</param>
        /// <param name="cssClasses">A string containing one or more CSS class names.</param>
        protected ButtonStateContent AddStatesCss(int layer, ButtonState[] states, string cssClasses)
        {
            ButtonStateContent content = new ButtonStateContent(states, cssClasses);
            AddStateContent(layer, content);
            return content;
        }

        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="state">The state the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        protected ButtonStateContent AddStateTemplate(int layer, ButtonState state, string templateSelector)
        {
            return AddStatesCss(layer, new ButtonState[] { state }, templateSelector);
        }

        /// <summary>Creates a Template with the specified selector and adds it as content for the given state.</summary>
        /// <param name="layer">The layer the state is rendered on (0 lowest, higher values fall in front of lower values)</param>
        /// <param name="states">The states the template applies to.</param>
        /// <param name="templateSelector">The CSS selector where the template can be found.</param>
        protected ButtonStateContent AddStatesTemplate(int layer, ButtonState[] states, string templateSelector)
        {
            ButtonStateContent content = new ButtonStateContent(states, null, new Template(templateSelector));
            AddStateContent(layer, content);
            return content;
        }
        #endregion

        #region Internal
        private jQueryObject CreateContainer()
        {
            jQueryObject div = Html.CreateDiv();

            Css.AbsoluteFill(div);

            div.AppendTo(Container);
            return div;
        }
        #endregion

    }
}