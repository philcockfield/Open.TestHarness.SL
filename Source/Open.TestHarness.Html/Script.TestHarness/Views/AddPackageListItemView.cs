using System;
using Open.Core;
using Open.Core.Controls.Buttons;

namespace Open.Testing.Views
{
    /// <summary>List item view for adding a new test-package.</summary>
    public class AddPackageListItemView : TestHarnessViewBase
    {
        #region Head
        private ImageButton addButton;
        private const int itemHeight = 34;

        public AddPackageListItemView()
        {
            // Setup initial conditions.
            Height = itemHeight;
            Position = CssPosition.Relative;

            // Setup the 'Add' button.
            InsertAddButton();
        }
        #endregion

        #region Internal
        private void InsertAddButton()
        {
            // Setup initial conditions.
            addButton = Common.Buttons.AddPackage as ImageButton;
            addButton.SetSize(itemHeight, itemHeight);
            ButtonView view = addButton.CreateView() as ButtonView;

            // Initialize CSS.
            view.SetCss(Css.Position, Css.Absolute);
            view.SetCss(Css.Right, 0 + Css.Px);
            view.SetCss(Css.Top, 0 + Css.Px);

            // Finish up.
            Container.Append(view.Container);
        }
        #endregion
    }
}
