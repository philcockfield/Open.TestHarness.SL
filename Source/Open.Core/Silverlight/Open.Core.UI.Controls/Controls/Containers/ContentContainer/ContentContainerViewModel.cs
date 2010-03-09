using System.ComponentModel.Composition;
using System.Windows;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ContentContainerViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>A flexible content container that can safely work with bound UIElements or DataTemplates.</summary>
    [Export(typeof(IContentContainer))]
    public class ContentContainerViewModel : ViewModelBase, IContentContainer
    {
        #region Head
        private static readonly DataTemplate defaultTemplate = Templates.Instance.Dictionary["ContentContainer.DefaultTemplate"] as DataTemplate;

        protected override void OnDisposed()
        {
            base.OnDisposed();
            OnPropertyChanged<T>(m => m.RenderTemplate);
        }
        #endregion

        #region Properties - IContentContainer
        /// <summary>Gets or sets the data-template used to render the content (this is overridden by 'Content' if set).</summary>
        public DataTemplate ContentTemplate
        {
            get { return GetPropertyValue<T, DataTemplate>(m => m.ContentTemplate); }
            set { SetPropertyValue<T, DataTemplate>(m => m.ContentTemplate, value, m => m.RenderTemplate); }
        }

        /// <summary>Gets or sets a specific UI element to render (overrides 'ContentTemplate').</summary>
        public object Content
        {
            get { return GetPropertyValue<T, object>(m => m.Content); }
            set
            {
                SetPropertyValue<T, object>(m => m.Content, value, m => m.RenderTemplate);
            }
        }

        /// <summary>Gets or sets the model to bind the ContentTemplate to.</summary>
        public object Model
        {
            get { return GetPropertyValue<T, object>(m => m.Model); }
            set { SetPropertyValue<T, object>(m => m.Model, value, m => m.BindingModel); }
        }
        #endregion

        #region Properties - ViewModel Specific
        public object BindingModel { get { return Model ?? this; } }
        public DataTemplate DefaultTemplate { get { return defaultTemplate; } }
        public DataTemplate RenderTemplate
        {
            get
            {
                // Setup initial conditions.
                if (IsDisposed) return null;

                // Override the 'ContentTemplate' if an explicit 'Content' value has been specified.
                if (Content == null) return ContentTemplate;
                return Content.GetType().IsA<UIElement>() 
                    ? null // Explicitly set UI elements are inserted and removed manually by control listeneing to this view-model.
                    : DefaultTemplate;
            }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new ContentContainer { ViewModel = this };
        }
        #endregion
    }
}
