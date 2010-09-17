using System;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>View for defining a new test-package to add to the side bar.</summary>
    public class AddPackageView : ViewBase
    {
        #region Head

        /// <summary>Constructor.</summary>
        public AddPackageView()
        {
            Container.Append("TBD: Add package - Textbox for JS file and Init entry-point method."); //TEMP 
            SetCss(Css.Color, Color.Black(0.1));
            SetCss(Css.FontSize, "60pt");
            Css.AddClasses(Container, "titleFont");
        }
        #endregion
    }
}
