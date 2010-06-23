using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Open.Core.Common;
using T = Open.Core.UI.Controls.NamedControlListItemViewModel;

namespace Open.Core.UI.Controls
{
    public class NamedControlListItemViewModel : ViewModelBase, INamedControlListItem
    {
        #region Head
        internal NamedControlListItemViewModel(NamedControlListViewModel parent, IViewFactory control)
        {
            Parent = parent;
            Control = control;
        }
        #endregion

        #region Properties : INamedControlListItem
        public IViewFactory Control { get; private set; }
        public string Title
        {
            get { return GetPropertyValue<T, string>(m => m.Title); }
            set { SetPropertyValue<T, string>(m => m.Title, value); }
        }

        public Thickness Margin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.Margin, new Thickness(0)); }
            set { SetPropertyValue<T, Thickness>(m => m.Margin, value, new Thickness(0)); }
        }
        #endregion

        #region Properties : ViewModel
        public NamedControlListViewModel Parent { get; private set; }
        public IFontSettings Font { get { return Parent.TitleFont; } }
        public Thickness ItemMargin { get; set; }
        #endregion

        #region Methods
        public void UpdateState(bool isLast)
        {
            var bottom = isLast ? 0 : Parent.ItemSpacing;
            if (ItemMargin.Bottom == bottom) return;
            ItemMargin = new Thickness(0, 0, 0, bottom);
            OnPropertyChanged<T>(m => m.ItemMargin);
        }
        #endregion
    }
}
