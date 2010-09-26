using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple button that renders with like the native Browser/OS.</summary>
    public class SystemButton : ButtonView
    {
        #region Head
        public const string Untitled = "Untitled";
        public const string DefaultPadding = "5px 30px";

        public const string PropHtmlContent = "HtmlContent";
        public const string PropType = "Type";
        public const string PropValue = "Value";
        public const string PropPadding = "Padding";
        public const string PropFontSize = "FontSize";

        private readonly jQueryObject htmButton;


        /// <summary>Constructor.</summary>
        public SystemButton() : base(InitHtml())
        {
            // Setup initial conditions.
            htmButton = Container;
            SyncHtml();
            SyncType();
            SyncValue();
            SyncPadding();
            SyncFontSize();

            // Wire up events.
            PropertyChanged += OnPropertyChanged;
        }

        private static jQueryObject InitHtml()
        {
            jQueryObject htmButton = Html.CreateElement(Html.Button);
            htmButton.Attribute(Html.Type, Html.Submit);
            return htmButton;
        }
        #endregion

        #region Event Handlers
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.Property.Name;
            if (name == PropCanToggle && CanToggle) throw new Exception(string.Format("[{0}] cannot be made to toggle.", GetType().Name));
        }

        protected override void OnIsEnabledChanged()
        {
            if (IsEnabled) {
                Container.RemoveAttr(Html.Disabled);
            } else {
                Container.Attribute(Html.Disabled, true.ToString());
            }
            base.OnIsEnabledChanged();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the HTML content of the button.</summary>
        public string HtmlContent
        {
            get { return (string)Get(PropHtmlContent, Untitled); }
            set { if (Set(PropHtmlContent, value, Untitled)) SyncHtml(); }
        }

        /// <summary>Gets or sets the button type (HTML attribute).</summary>
        public string Type
        {
            get { return (string)Get(PropType, Html.Submit); }
            set { if (Set(PropType, value, Html.Submit)) SyncType(); }
        }

        /// <summary>Gets or sets the underlying value of a button.</summary>
        public string Value
        {
            get { return (string) Get(PropValue, null); }
            set { if (Set(PropValue, value, null)) SyncValue(); }
        }

        public string Padding
        {
            get { return (string)Get(PropPadding, DefaultPadding); }
            set { if (Set(PropPadding, value, DefaultPadding)) SyncPadding(); }
        }

        public string FontSize
        {
            get { return (string) Get(PropFontSize, null); }
            set { if (Set(PropFontSize, value, null)) SyncFontSize(); }
        }
        #endregion

        #region Internal
        protected override void BeforeInsertReplace(jQueryObject e)
        {
            Value = e.GetAttribute(Html.Value);
            Type = e.GetAttribute(Html.Type);
            HtmlContent = e.GetHtml();
            base.BeforeInsertReplace(e);
        }

        private void SyncHtml() { htmButton.Html(HtmlContent); FireSizeChanged(); }
        private void SyncType() { htmButton.Attribute(Html.Type, Type); }
        private void SyncValue() { htmButton.Attribute(Html.Value, Value); }
        private void SyncPadding() { htmButton.CSS(Css.Padding, Padding); FireSizeChanged();  }
        private void SyncFontSize() { htmButton.CSS(Css.FontSize, FontSize); FireSizeChanged(); }
        #endregion
    }
}