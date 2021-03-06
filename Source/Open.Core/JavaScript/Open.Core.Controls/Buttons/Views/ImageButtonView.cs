using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>The view for the ImageButton.</summary>
    internal class ImageButtonView : ButtonView
    {
        #region Head
        private const string SelectorTemplate = "#btnImage";
        private const string SelectorBgTemplate = "#btnImage_Bg";
        private const string SelectorDivIcon = "div.btnImage.icon";
        private readonly ImageButtonBase buttonModel;

        public ImageButtonView(ImageButtonBase model) : base(model)
        {
            // Setup initial conditions.
            buttonModel = model;

            // Ensure the required templates are downloaded.
            AddRequiredTemplate(ButtonTemplates.CommonBg, ButtonTemplates.TemplateUrl(ButtonTemplate.Common));
            AddRequiredTemplate(SelectorTemplate, ButtonTemplates.TemplateUrl(ButtonTemplate.Image));
            DownloadTemplates(delegate
                                  {
                                      // Setup templates.
                                      SetupContent();

                                      // Wire up events.
                                      buttonModel.PropertyChanged += OnModelPropertyChanged;
                                  });
        }

        protected override void OnDisposed()
        {
            buttonModel.PropertyChanged -= OnModelPropertyChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string name = e.Property.Name;
            if (name == ButtonBase.PropWidth) SyncDimension(SizeDimension.Width);
            if (name == ButtonBase.PropHeight) SyncDimension(SizeDimension.Height);
            if (name == ImageButton.PropBackgroundHighlighting) SetupContent();
        }
        #endregion

        #region Properties
        private jQueryObject DivIcon { get { return Container.Find(SelectorDivIcon); } }
        #endregion

        #region Methods
        protected override void OnRendered()
        {
            SyncSrc();
            base.OnRendered();
        }
        #endregion

        #region Internal
        private void SetupContent()
        {
            // Setup initial conditions.
            ClearContent();

            // Add background (if required).
            if (buttonModel.BackgroundHighlighting)
            {
                ButtonState[] highlightStates = new ButtonState[] { ButtonState.MouseOver, ButtonState.MouseDown, ButtonState.Pressed };
                TemplateForStates(0, highlightStates, SelectorBgTemplate);
                CssForState(0, ButtonState.MouseOver, ClassOver);
                CssForState(0, ButtonState.MouseDown, ClassDown);
                CssForState(0, ButtonState.Pressed, ClassPressed);
            }

            // Add IMG level.
            TemplateForStates(1, AllStates, SelectorTemplate);

            // Finish up.
            UpdateLayout();
        }

        private void SyncSrc()
        {
            DivIcon.CSS(
                        "background-image", 
                        string.Format("url({0})", buttonModel.GetCurrentImage(State, Focus.IsFocused)));
        }
        #endregion
    }
}
