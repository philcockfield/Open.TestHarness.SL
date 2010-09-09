using System;
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
        #endregion

        #region Head
        public const string PropBackground = "Background";
        public const string PropIsVisible = "IsVisible";
        public const string PropOpacity = "Opacity";
        public const string PropWidth = "Width";
        public const string PropHeight = "Height";
        public const string PropIsEnabled = "IsEnabled";

        private bool isInitialized;
        private jQueryObject container;
        #endregion

        #region Properties : IView
        public bool IsInitialized { get { return isInitialized; } }
        #endregion

        #region Properties
        /// <summary>Gets the element that the view is contained within.</summary>
        public jQueryObject Container { get { return container; } }
        #endregion

        #region Properties : State
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

        #region Methods
        public void Initialize(jQueryObject container)
        {
            // Setup initial conditions.)
            if (IsInitialized) throw new Exception("View is already initialized.");

            // Store reference to the container.
            this.container = container;

            // Finish up.
            OnInitialize(this.container);
            isInitialized = true;
        }

        /// <summary>Deriving implementation of Initialize.</summary>
        protected virtual void OnInitialize(jQueryObject container) { }

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

            string attr = dimension == SizeDimension.Width ? Css.Width : Css.Height;

            SetCss(
                dimension == SizeDimension.Width ? Css.Width : Css.Height, 
                value + Css.Px);


            if (withEvent) FireSizeChanged();
        }

        public void Insert(string cssSeletor, InsertMode mode)
        {
            switch (mode)
            {
                case InsertMode.Replace:
                    Html.ReplaceWith(cssSeletor, Container, true);
                    break;

                default: throw new Exception(string.Format("Failed to insert [{0}] at '{1}'.  The insert mode '{2}' is not supported.", GetType().Name, cssSeletor, mode.ToString()));
            }
        }
        #endregion

        #region Methods : CSS
        public string GetCss(string attribute)
        {
            string value = Container == null ? null : Container.GetCSS(attribute);
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public void SetCss(string attribute, string value)
        {
            if (Container == null) throw new Exception("Cannot set CSS on view until it is initialized.");
            Container.CSS(attribute, value);
        }
        #endregion

        #region Methods : Protected 
        protected virtual void OnIsEnabledChanged() { }
        protected virtual void OnIsVisibleChanged() { }
        protected virtual void OnSizeChanged() { }
        #endregion
    }
}
