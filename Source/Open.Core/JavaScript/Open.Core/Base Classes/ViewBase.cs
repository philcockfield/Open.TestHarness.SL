using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Base for classes that represent, manage and construct views ("UI").</summary>
    public abstract class ViewBase : ModelBase, IView
    {
        #region Events
        public event EventHandler IsEnabledChanged;
        protected void FireIsEnabledChanged()
        {
            OnIsEnabledChanged();
            if (IsEnabledChanged != null) IsEnabledChanged(this, new EventArgs());
        }

        public event EventHandler IsVisibleChanged;
        protected void FireIsVisibleChanged()
        {
            OnIsVisibleChanged();
            if (IsVisibleChanged != null) IsVisibleChanged(this, new EventArgs());
        }

        public event EventHandler SizeChanged;
        protected void FireSizeChanged()
        {
            OnSizeChanged();
            if (SizeChanged != null) SizeChanged(this, new EventArgs());
        }

        public event EventHandler GotFocus;
        private void FireGotFocus(){if (GotFocus != null) GotFocus(this, new EventArgs());}

        public event EventHandler LostFocus;
        private void FireLostFocus(){if (LostFocus != null) LostFocus(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropBackground = "Background";
        public const string PropIsVisible = "IsVisible";
        public const string PropOpacity = "Opacity";
        public const string PropWidth = "Width";
        public const string PropHeight = "Height";
        public const string PropIsEnabled = "IsEnabled";
        public const string PropIsFocused = "IsFocused";
        public const string PropCanFocus = "CanFocus";
        public const string PropTabIndex = "TabIndex";

        private readonly jQueryObject container;
        private bool tabIndexChanging;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        protected extern ViewBase();

        /// <summary>Constructor.</summary>
        /// <param name="container">The root HTML element of the control (if null a <DIV></DIV> is generated).</param>
        protected ViewBase(jQueryObject container)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(container)) container = Html.CreateDiv();
            this.container = container;

            // Wire up events.
            Container.Bind(Html.Focus, delegate(jQueryEvent e) { HandleFocusChanged(true); });
            Container.Bind(Html.Blur, delegate(jQueryEvent e) { HandleFocusChanged(false); });
        }

        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            Container.Unbind(Html.Focus);
            Container.Unbind(Html.Blur);
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void HandleFocusChanged(bool gotFocus)
        {
            IsFocused = gotFocus;
            if (gotFocus) { FireGotFocus(); } else { FireLostFocus(); }
        }
        #endregion

        #region Properties : IView
        public jQueryObject Container { get { return container; } }
        public string OuterHtml { get { return Html.ToHtml(Container); } }
        public string InnerHtml { get { return Container.GetHtml(); } }
        #endregion

        #region Properties : IView - State
        public bool IsEnabled
        {
            get { return (bool)Get(PropIsEnabled, true); }
            set { if (Set(PropIsEnabled, value, true)) FireIsEnabledChanged(); }
        }

        public bool IsVisible
        {
            get { return Container == null ? false : Css.IsVisible(Container); }
            set
            {
                if (value == IsVisible) return;
                SetCss(Css.Display, value ? Css.Block : Css.None);
                FireIsVisibleChanged();
                FirePropertyChanged(PropIsVisible);
            }
        }
        #endregion

        #region Properties : IView (Focus)
        public bool IsFocused
        {
            get { return (bool)Get(PropIsFocused, false); }
            private set { Set(PropIsFocused, value, false); }
        }

        public bool CanFocus
        {
            get { return TabIndex >= 0; }
            set 
            {
                // Setup initial conditions.
                if (value == CanFocus) return;

                // Update the value (stored in TabIndex).
                int tabIndex = TabIndex;
                if (value && tabIndex < 0) TabIndex = 0;
                if (!value && tabIndex >= 0) TabIndex = -1;

                // Finish up.
                FirePropertyChanged(PropCanFocus);
                IsFocused = false;
            }
        }

        public int TabIndex
        {
            get
            {
                object value = Container.GetAttribute(Html.TabIndex);
                return Script.IsNullOrUndefined(value) ? -1 : (int)value;
            }
            set
            {
                if (tabIndexChanging) return;
                if (value == TabIndex) return;
                if (value < 0)
                {
                    if (CanFocus)
                    {
                        tabIndexChanging = true;
                        CanFocus = false;
                        tabIndexChanging = false;
                    }
                    Container.RemoveAttr(Html.TabIndex);
                }
                else
                {
                    Container.Attribute(Html.TabIndex, value.ToString());
                }
                FirePropertyChanged(PropTabIndex);
            }
        }
        #endregion

        #region Properties : Style
        public string Background
        {
            get { return GetCss(Css.Background); }
            set
            {
                SetCss(Css.Background, value);
                FirePropertyChanged(PropBackground);
            }
        }

        public double Opacity
        {
            get { return Number.ParseFloat(GetCss(Css.Opacity)); }
            set
            {
                // Setup initial conditions.
                value = Helper.NumberDouble.WithinBounds(value, 0, 1);
                if (value == Opacity) return;

                // Update CSS.
                Css.SetOpacity(Container, value);

                // Finish up.
                FirePropertyChanged(PropOpacity);
            }
        }

        public int Width
        {
            get { return Container == null ? 0 : Container.GetWidth(); }
            set
            {
                if (value == Width) return;
                SetSizeInternal(value, SizeDimension.Width, true);
                FirePropertyChanged(PropWidth);
            }
        }

        public int Height
        {
            get { return Container == null ? 0 : Container.GetHeight(); }
            set
            {
                if (value == Height) return;
                SetSizeInternal(value, SizeDimension.Height, true);
                FirePropertyChanged(PropHeight);
            }
        }
        #endregion

        #region Methods : IView
        public void SetSize(int width, int height)
        {
            if (width == Width && height == Height) return;
            SetSizeInternal(width, SizeDimension.Width, false);
            SetSizeInternal(height, SizeDimension.Height, false);
            FireSizeChanged();
        }

        private void SetSizeInternal(int value, SizeDimension dimension, bool withEvent)
        {
            if (value < 0) value = 0;
            SetCss(
                dimension == SizeDimension.Width ? Css.Width : Css.Height, 
                value + Css.Px);
            if (withEvent) FireSizeChanged();
        }

        /// <summary>Invoked immediately before an Insert operation of mode 'Replace' is executed.</summary>
        /// <param name="replacedElement">The element being replaced with this control.</param>
        /// <remarks>Use this to extract any meta-data from the original element before it is removed from the DOM.</remarks>
        protected virtual void BeforeInsertReplace(jQueryObject replacedElement) { }

        /// <summary>Gives keyboard focus to the control (see also: CanFocus, IsFocused, TabIndex properties).</summary>
        /// <returns>True if the control can recieve focus (and therefore focus was applied), otherwise False.</returns>
        public bool Focus()
        {
            if (!CanFocus) return false;
            Container.Focus();
            return true;
        }

        /// <summary>Removes keyboard focus to the control (see also: CanFocus, IsFocused, TabIndex properties).</summary>
        /// <returns>True if the control can recieve focus (and therefore focus was removed), otherwise False.</returns>
        public bool Blur()
        {
            if (!CanFocus) return false;
            Container.Blur();
            return true;
        }
        #endregion

        #region Methods : Insert
        public void Insert(string cssSeletor, InsertMode mode)
        {
            switch (mode)
            {
                case InsertMode.Replace:
                    // Retreive the element to replace.
                    jQueryObject replaceElement = jQuery.Select(cssSeletor);
                    if (replaceElement == null) throw GetInsertException(cssSeletor, "No such element exists");

                    // Invoke the pre-method
                    BeforeInsertReplace(replaceElement);

                    // Perform the insertion.
                    Css.CopyClasses(replaceElement, Container);
                    Container.ReplaceAll(cssSeletor);
                    break;

                default: throw GetInsertException(cssSeletor, string.Format("The insert mode '{0}' is not supported.", mode.ToString()));
            }
        }

        private Exception GetInsertException(string cssSeletor, string message)
        {
            return new Exception(string.Format("Failed to insert [{0}] at '{1}'. {2}", GetType().Name, cssSeletor, message));
        }
        #endregion

        #region Methods : CSS
        public string GetCss(string attribute)
        {
            string value = Container.GetCSS(attribute);
            return string.IsNullOrEmpty(value) ? null : value;
        }
        public void SetCss(string attribute, string value) { Container.CSS(attribute, value); }

        public string GetAttribute(string attribute)
        {
            string value = Container.GetAttribute(attribute);
            return string.IsNullOrEmpty(value) ? null : value;
        }
        public void SetAttribute(string attribute, string value) { Container.Attribute(attribute, value); }
        #endregion

        #region Methods : Protected 
        protected virtual void OnIsEnabledChanged() { }
        protected virtual void OnIsVisibleChanged() { }
        protected virtual void OnSizeChanged() { }

        /// <summary>Inserts the HTML from the specified URL.</summary>
        /// <param name="url">The URL of the HTML content to retrieve.</param>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        protected void RetrieveHtml(string url, Action onComplete)
        {
            jQuery.Get(Helper.Url.PrependDomain(url), delegate(object data)
                                                          {
                                                              Container.Html(data.ToString());
                                                              Helper.Invoke(onComplete);
                                                          });
        }
        #endregion
    }
}
