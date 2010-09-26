using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Base class for buttons.</summary>
    public abstract class ButtonView : ViewBase, IButtonView
    {
        #region Events

        //TEMP 
        //public event EventHandler Click;
        //private void FireClick() { if (Click != null) Click(this, new EventArgs()); }

        //public event EventHandler IsPressedChanged;
        //private void FireIsPressedChanged() { if (IsPressedChanged != null) IsPressedChanged(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropModel = "Model";
        private readonly ButtonEventManager eventManager;
        private readonly IButton model;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        protected extern ButtonView();

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button</param>
        [AlternateSignature]
        protected extern ButtonView(IButton model);

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button</param>
        /// <param name="container">The HTML container of the button.</param>
        protected ButtonView(IButton model, jQueryObject container) : base(InitContainer(container))
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(model)) this.model = new ButtonModel();
            eventManager = new ButtonEventManager(this);

            // Wire up events.
            eventManager.PropertyChanged += OnEventManagerPropertyChanged;
        }

        private static jQueryObject InitContainer(jQueryObject container)
        {
            if (Script.IsNullOrUndefined(container)) container = Html.CreateSpan();
            return container;
        }

        /// <summary>Finalize.</summary>
        protected override void OnDisposed()
        {
            eventManager.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnEventManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FirePropertyChanged(e.Property.Name);
        }
        #endregion

        #region Properties : IButtonView
        public IButton Model { get { return model; } }
        public ButtonState State { get { return eventManager.State; } }
        public bool IsMouseOver { get { return eventManager.IsMouseOver; } }
        public bool IsMouseDown { get { return eventManager.IsMouseDown; } }
        #endregion

        #region Methods
        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="html">The HTML element to use.</param>
        [AlternateSignature]
        protected virtual extern void StateContent(ButtonState state, jQueryObject html);

        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="cssClasses">The CSS class(es) to apply (space delimited).</param>
        [AlternateSignature]
        protected virtual extern void StateContent(ButtonState state, string cssClasses);

        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="html">The HTML element to use.</param>
        /// <param name="cssClasses">The CSS class(es) to apply (space delimited).</param>
        protected virtual void StateContent(ButtonState state, jQueryObject html, string cssClasses)
        {
            // TODO - StateContent
        }
        #endregion
    }
}