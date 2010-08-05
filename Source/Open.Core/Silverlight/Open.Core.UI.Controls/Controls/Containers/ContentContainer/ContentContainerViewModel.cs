using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ContentContainerViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>A flexible content container that can safely work with bound UIElements or DataTemplates.</summary>
    [Export(typeof(IContentContainer))]
    public class ContentContainerViewModel : ViewModelBase, IContentContainer
    {
        #region Event Handlers
        /// <summary>Fires when the 'Content' value changes.</summary>
        public event EventHandler ContentChanged;
        private void FireContentChanged(){if (ContentChanged != null) ContentChanged(this, new EventArgs());}

        /// <summary>Fires when the 'ContentTemplate' value changes.</summary>
        public event EventHandler ContentTemplateChanged;
        private void FireContentTemplateChanged(){if (ContentTemplateChanged != null) ContentTemplateChanged(this, new EventArgs());}

        /// <summary>Fires when the 'Model' value changes.</summary>
        public event EventHandler ModelChanged;
        private void FireModelChanged(){if (ModelChanged != null) ModelChanged(this, new EventArgs());}
        #endregion

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
            set
            {
                if (Equals(value, ContentTemplate)) return;
                SetPropertyValue<T, DataTemplate>(m => m.ContentTemplate, value, m => m.RenderTemplate);
                FireContentTemplateChanged();
            }
        }

        /// <summary>Gets or sets a specific UI element to render (overrides 'ContentTemplate').</summary>
        public object Content
        {
            get { return GetPropertyValue<T, object>(m => m.Content); }
            set
            {
                if (Equals(value, Content)) return;
                SetPropertyValue<T, object>(m => m.Content, value, m => m.RenderTemplate);
                FireContentChanged();
            }
        }

        /// <summary>Gets or sets the model to bind the ContentTemplate to.</summary>
        public object Model
        {
            get { return GetPropertyValue<T, object>(m => m.Model); }
            set
            {
                if (Equals(value, Model)) return;
                SetPropertyValue<T, object>(m => m.Model, value, m => m.BindingModel);
                FireModelChanged();
            }
        }

        /// <summary>Gets or sets the background.</summary>
        public Brush Background
        {
            get { return Property.GetValue<T, Brush>(m => m.Background); }
            set { Property.SetValue<T, Brush>(m => m.Background, value); }
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
                            ? null // Explicitly set UI elements are inserted and removed manually by the control that this view-model is bound to.
                            : DefaultTemplate;
            }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new ContentContainer { ViewModel = this, IsTabStop = false };
        }
        #endregion
    }
}
