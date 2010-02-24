using Open.Core.Common;

using T = Sample.ClassLibrary.Views.MyControlViewModel;

namespace Sample.ClassLibrary.Views
{
    /// <summary>Logical representation of the 'MyControl' sample control.</summary>
    public class MyControlViewModel : ViewModelBase
    {
        /// <summary>Gets or sets the display title of the control.</summary>
        public string Title
        {
            get { return GetPropertyValue<T, string>(m => m.Title, "Untitled"); }
            set { SetPropertyValue<T, string>(m => m.Title, value, "Untitled"); }
        }
    }
}
