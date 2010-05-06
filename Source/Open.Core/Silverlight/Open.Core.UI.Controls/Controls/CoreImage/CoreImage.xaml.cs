using System.Windows.Controls;
using System.Windows.Media.Effects;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    public partial class CoreImage : UserControl
    {
        #region Head
        private readonly DataContextObserver dataContextObserver;
        private PropertyObserver<IDropShadowEffect> dropShadowObserver;
        private DropShadowEffect dropShadowEffect;

        /// <summary>Constructor.</summary>
        public CoreImage()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            if (dropShadowObserver != null) dropShadowObserver.Dispose();
            if (ViewModel != null)
            {
                dropShadowObserver = new PropertyObserver<IDropShadowEffect>(ViewModel.DropShadow)
                    .RegisterHandler(m => m.Opacity, UpdateShadow)
                    .RegisterHandler(m => m.Color, UpdateShadow)
                    .RegisterHandler(m => m.Direction, UpdateShadow)
                    .RegisterHandler(m => m.BlurRadius, UpdateShadow)
                    .RegisterHandler(m => m.ShadowDepth, UpdateShadow);
            }
            UpdateShadow();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IImage ViewModel
        {
            get { return DataContext as IImage; }
            set { DataContext = value; }
        }

        private bool HasShadow { get { return ViewModel != null && ViewModel.DropShadow.Opacity != 0; } }
        #endregion
        
        #region Methods
        /// <summary>Creates a new view-model for the control.</summary>
        public void CreateViewModel()
        {
            ViewModel = new CoreImageViewModel();
        }
        #endregion

        #region Internal
        private void UpdateShadow()
        {
            if (HasShadow)
            {
                // Update shadow values.
                ViewModel.DropShadow.CopyTo(GetShadow());
            }
            else
            {
                // Remove shadow.
                ClearEffect();
            }
        }

        private void ClearEffect()
        {
            if (dropShadowEffect == null) return;
            image.Effect = null;
            dropShadowEffect = null;
        }

        private DropShadowEffect GetShadow()
        {
            // Setup initial conditions.
            if (!HasShadow) return null;
            if (dropShadowEffect != null) return dropShadowEffect;

            // Create and initialize the drop-shadow.
            dropShadowEffect = new DropShadowEffect();
            ViewModel.DropShadow.CopyTo(dropShadowEffect);

            // Insert into visual tree.
            image.Effect = dropShadowEffect;

            // Finish up.
            return dropShadowEffect;
        }
        #endregion
    }
}