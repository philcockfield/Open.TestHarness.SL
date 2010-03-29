using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A flexible content container that can safely work with bound UIElements or DataTemplates.</summary>
    public partial class ContentContainer : UserControl
    {
        #region Head
        private DataContextObserver dataContextObserver;
        private PropertyObserver<IContentContainer> viewModelObserver;

        /// <summary>Constructor.</summary>
        public ContentContainer()
        {
            InitializeComponent();
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
            if (ViewModel != null) OnDataContextChanged();
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            // Setup initial conditions.
            if (viewModelObserver != null) viewModelObserver.Dispose();

            if (ViewModel == null)
            {
                ClearContentElement();
            }
            else
            {
                // Wire up events.
                viewModelObserver = new PropertyObserver<IContentContainer>(ViewModel)
                    .RegisterHandler(m => m.Content, m => OnChanged())
                    .RegisterHandler(m => m.ContentTemplate, m => OnChanged());
                ViewModel.Disposed += delegate { ClearContentElement(); };
            }
        }

        private void OnChanged()
        {
            // Setup initial conditions.
            if (ViewModel == null)
            {
                ClearContentElement();
                return;
            }

            // Update the explicit UI content element.
            var element = ViewModel.Content as UIElement;
            if (element == null)
            {
                ClearContentElement();
            }
            else
            {
                if (!elementContainer.Children.Contains(element))
                {
                    ClearContentElement();
                    element.RemoveFromVisualTree();
                    elementContainer.Children.Add(element);
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IContentContainer ViewModel
        {
            get { return DataContext as IContentContainer; }
            set { DataContext = value; }
        }
        #endregion

        #region Methods
        /// <summary>Creates a fresh view-model and assigns it to the ViewModel property.</summary>
        /// <remarks>
        ///     Typically the a view-model is bound to this property, and so for performance reasons
        ///     a default view-model is not created.  This method allows a "View First" usage of the control.
        /// </remarks>
        public IContentContainer CreateViewModel()
        {
            ViewModel = new ContentContainerViewModel();
            return ViewModel;
        }
        #endregion

        #region Internal
        private void ClearContentElement()
        {
            foreach (var child in elementContainer.Children.ToList())
            {
                elementContainer.Children.Remove(child);
            }
            elementContainer.UpdateLayout();
        }
        #endregion
    }
}