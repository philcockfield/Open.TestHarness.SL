using System;
using System.Collections;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A complex button which can render multiple layers of HTML from Templates.</summary>
    public class LayeredButtonView : ButtonView
    {
        #region Head
        private readonly ButtonContentController contentController;
        private readonly jQueryObject divContent;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        public extern LayeredButtonView();

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button (if ommited a default 'ButtonModel' is created and used).</param>
        [AlternateSignature]
        public extern LayeredButtonView(IButton model);

        /// <summary>Constructor.</summary>
        /// <param name="model">The logical model of the button</param>
        /// <param name="container">The HTML container of the button.</param>
        public LayeredButtonView(IButton model, jQueryObject container) : base(model, container, Html.CreateDiv())
        {
            // Setup initial conditions.
            clickMask.AddClass(Css.Classes.AbsoluteFill);
            
            // Insert containers.
            divContent = CreateAndAppendContainer("buttonContent");
            clickMask.AppendTo(Container);

            // Create controllers
            contentController = new ButtonContentController(this, divContent);
        }

        protected override void OnDisposed()
        {
            contentController.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Methods
        protected override void OnUpdateLayout()
        {
            OnRendering();
            contentController.UpdateLayout();
            OnRendered();
            base.OnUpdateLayout();
        }

        /// <summary>Invoked immediately before the button has rendered it's state.</summary>
        protected virtual void OnRendering() { }

        /// <summary>Invoked immediately after the button has rendered it's state.</summary>
        protected virtual void OnRendered() { }

        /// <summary>Clears all CSS and Template content which has been added to the button.</summary>
        protected void ClearContent()
        {
            contentController.Clear();
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
    }
}
