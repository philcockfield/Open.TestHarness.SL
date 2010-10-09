using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>The view for the ImageButton.</summary>
    internal class ImageButtonView : ButtonView
    {
        #region Head
        private const string SelectorTemplate = "#btnImage";
        private const string SelectorBgTemplate = "#btnImage_Bg";
        private const string SelectorImage = "img.btnImage";
        private readonly ImageButton buttonModel;

        public ImageButtonView(ImageButton model) : base(model)
        {
            // Setup initial conditions.
            buttonModel = model;
            Css.InsertLink(Css.Urls.CoreButtons);

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
            if (name == ButtonModel.PropWidth) SyncDimension(SizeDimension.Width);
            if (name == ButtonModel.PropHeight) SyncDimension(SizeDimension.Height);
            if (name == ImageButton.PropBackgroundHighlighting) SetupContent();
        }
        #endregion

        #region Properties
        private jQueryObject Image { get { return Container.Find(SelectorImage); } }
        #endregion

        #region Methods
        protected override void OnRendered()
        {
            SyncSrc();
            Image.Load(delegate { CenterImage(); });
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
            string path = buttonModel.GetCurrentImage(State, Focus.IsFocused);
            jQueryObject image = Image;
            image.Attribute(Html.Src, path);
        }

        private void CenterImage()
        {
            Css.Center(Image, Container, buttonModel.ImageOffset);
        }
        #endregion
    }
}
