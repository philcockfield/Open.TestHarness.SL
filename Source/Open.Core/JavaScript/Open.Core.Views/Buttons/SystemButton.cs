using System;
using jQueryApi;
using HtmlUtil = Open.Core.Html;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple button that renders with like the native Browser/OS.</summary>
    public class SystemButton : ButtonBase
    {
        #region Head
        public const string Untitled = "Untitled";
        public const string DefaultPadding = "5px 30px";

        public const string PropHtml = "Html";
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
            jQueryObject htmButton = HtmlUtil.CreateElement(HtmlUtil.Button);
            htmButton.Attribute(HtmlUtil.Type, HtmlUtil.Submit);
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
                Container.RemoveAttr(HtmlUtil.Disabled);
            } else {
                Container.Attribute(HtmlUtil.Disabled, true.ToString());
            }
            base.OnIsEnabledChanged();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the HTML content of the button.</summary>
        public string Html
        {
            get { return (string)Get(PropHtml, Untitled); }
            set { if (Set(PropHtml, value, Untitled)) SyncHtml(); }
        }

        /// <summary>Gets or sets the button type (HTML attribute).</summary>
        public string Type
        {
            get { return (string) Get(PropType, HtmlUtil.Submit); }
            set { if (Set(PropType, value, HtmlUtil.Submit)) SyncType(); }
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
        protected override void BeforeInsertReplace(jQueryObject replacedElement)
        {
            Value = replacedElement.GetAttribute(HtmlUtil.Value);
            base.BeforeInsertReplace(replacedElement);
        }

        private void SyncHtml() { htmButton.Html(Html); FireSizeChanged(); }
        private void SyncType() { htmButton.Attribute(HtmlUtil.Type, Type); }
        private void SyncValue() { htmButton.Attribute(HtmlUtil.Value, Value); }
        private void SyncPadding() { htmButton.CSS(Css.Padding, Padding); FireSizeChanged();  }
        private void SyncFontSize() { htmButton.CSS(Css.FontSize, FontSize); FireSizeChanged(); }
        #endregion
    }
}