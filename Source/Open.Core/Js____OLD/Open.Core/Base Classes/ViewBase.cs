using System;
using System.Html;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Base for classes that represent, manage and construct views ("UI").</summary>
    public abstract class ViewBase : ModelBase, IView, ISize
    {
        #region Events
        public event EventHandler Loaded;
        protected void FireLoaded()
        {
            IsLoaded = true;
            if (Loaded != null) Loaded(this, new EventArgs());
        }

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
        internal void FireGotFocus(){if (GotFocus != null) GotFocus(this, new EventArgs());}

        public event EventHandler LostFocus;
        internal void FireLostFocus() { if (LostFocus != null) LostFocus(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropIsLoaded = "IsLoaded";
        public const string PropPosition = "Position";
        public const string PropBackground = "Background";
        public const string PropIsVisible = "IsVisible";
        public const string PropOpacity = "Opacity";
        public const string PropWidth = "Width";
        public const string PropHeight = "Height";
        public const string PropIsEnabled = "IsEnabled";

        private const string CssStatic = "static";

        private readonly jQueryObject container;
        private IFocus focus;

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
        }

        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            Helper.Dispose(focus);
            base.OnDisposed();
        }
        #endregion

        #region Properties : IView
        public jQueryObject Container { get { return container; } }
        public Element Element { get { return Container.GetElement(0); } }
        public string OuterHtml { get { return Html.OuterHtml(Container); } }
        public string InnerHtml { get { return Container.GetHtml(); } }
        public IFocus Focus { get { return focus ?? (focus = new ViewFocus(this, Container)); } }
        #endregion

        #region Properties : IView - State
        /// <remarks>
        ///     Implementers: This is set to True by default.  When performing an async
        ///     initialization during the constructor set this to False, then fire the 'Loaded'
        ///     event upon completion (causing IsLoaded to be set to true).
        /// </remarks>
        public bool IsLoaded
        {
            get { return (bool) Get(PropIsLoaded, true); }
            protected set { Set(PropIsLoaded, value, true); }
        }

        public virtual bool IsEnabled
        {
            get { return (bool)Get(PropIsEnabled, true); }
            set { if (Set(PropIsEnabled, value, true)) FireIsEnabledChanged(); }
        }

        public bool IsVisible
        {
            get { return (bool) Get(PropIsVisible, true); }
            set
            {
                if (Set(PropIsVisible, value, true))
                {
                    // Update the CSS.
                    SyncVisibility(value);

                    // Finish up.
                    FireIsVisibleChanged();
                }
            }
        }
        #endregion

        #region Properties : IView - Style
        public CssPosition Position
        {
            get
            {
                string value = GetCss(Css.Position);
                return string.IsNullOrEmpty(value) || value == CssStatic
                                           ? CssPosition.None
                                           : (CssPosition)Enum.Parse(typeof(CssPosition), value);
            }
            set
            {
                SetCss(Css.Position, value == CssPosition.None ? CssStatic : value.ToString().ToLocaleLowerCase());
            }
        }

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

        public virtual int Height
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
            Css.SetDimension(Container, dimension, value);
            if (withEvent) FireSizeChanged();
        }

        public void UpdateLayout()
        {
            SyncVisibility(IsVisible); 
            OnUpdateLayout();
        }
        protected virtual void OnUpdateLayout() { }
        #endregion

        #region Methods : Insert
        public void Insert(string cssSeletor, InsertMode mode)
        {
            // Retreive the target element used for the insertion.
            jQueryObject element = jQuery.Select(cssSeletor);
            if (element == null) throw GetInsertException(cssSeletor, "No such element exists");

            // Invoke the pre-method.
            OnBeforeInsert(element, mode);

            switch (mode)
            {
                case InsertMode.Replace:
                    // Perform the insertion.
                    Css.CopyClasses(element, Container);
                    Container.ReplaceAll(cssSeletor);
                    break;

                default: throw GetInsertException(cssSeletor, string.Format("The insert mode '{0}' is not supported.", mode.ToString()));
            }

            // Finish up.
            UpdateLayout();
            OnAfterInsert(mode);
        }

        /// <summary>Invoked immediately before the insertion occurs via the 'Insert' method.</summary>
        /// <param name="targetElement">The element being replaced.</param>
        /// <param name="mode">The strategy used for the insertion.</param>
        /// <remarks>Use this to extract any meta-data from the original element before it is removed from the DOM.</remarks>
        protected virtual void OnBeforeInsert(jQueryObject targetElement, InsertMode mode) { }

        /// <summary>Invoked immediately after the insertion occurs via the 'Insert' method.</summary>
        /// <param name="mode">The strategy used for the insertion.</param>
        protected virtual void OnAfterInsert(InsertMode mode) { }


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
        protected virtual void OnSizeChanged() { }

        /// <summary>
        ///     Invoked when the 'IsVisible' property is causing the visibility to change.
        /// </summary>
        /// <param name="isVisible">The IsVisible value.</param>
        /// <returns>The IsVisible value (overridden value if change required).</returns>
        /// <remarks>
        ///     Override this method to intercept the visibility value which is ultmately used
        ///     to chance the CSS value.  For instance, this is useful when you want to retain
        ///     the 'IsVisible' property value as a logical value, but cause the control to be hidden
        ///     like when a tab should be shown (logically), but is overflowing off the screen so
        ///     should be hidden.
        /// </remarks>
        protected virtual bool OnIsVisibleChanging(bool isVisible) { return isVisible; }
        protected virtual void OnIsVisibleChanged() { }

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

        /// <summary>Changes the element that is used for focus control.</summary>
        /// <param name="element">The child element that is the primary reciever of focus.</param>
        /// <remarks>
        ///     By default this is the 'Container', however if a child element within 
        ///     the control is the primary reciever of focus use this method to 
        ///     assign it as such.
        /// </remarks>
        protected void ChangeFocusElement(jQueryObject element)
        {
            focus = new ViewFocus(this, element);
        }
        #endregion

        #region Internal
        private void SyncVisibility(bool isVisible)
        {


//            Log.Info("SYNC: " + isVisible + ":" + this.GetType().Name); //TEMP 

            isVisible = OnIsVisibleChanging(isVisible); // Allow deriving class to override the display value.
            Css.SetDisplay(Container, isVisible);
        }
        #endregion
    }
}
