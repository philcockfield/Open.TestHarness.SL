using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A simple way of rendering an 'IconImage' in XAML.</summary>
    public partial class ImageIcon : UserControl
    {
        #region Head
        /// <summary>Constructor.</summary>
        public ImageIcon()
        {
            InitializeComponent();
            OnSourceChanged();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the enum representing the icon image.</summary>
        public IconImage Source
        {
            get { return (IconImage)(GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>Gets or sets the enum representing the icon image.</summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageIcon>(m => m.Source),
                typeof(IconImage),
                typeof(ImageIcon),
                new PropertyMetadata(IconImage.SilkAccept, (s, e) => ((ImageIcon)s).OnSourceChanged()));
        private void OnSourceChanged()
        {
            iconImage.Source = Source.ToImageSource();
        }
        #endregion
    }
}