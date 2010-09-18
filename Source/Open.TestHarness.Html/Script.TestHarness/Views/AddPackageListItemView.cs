using System;
using Open.Core;
using Open.Core.Controls.Buttons;

namespace Open.Testing.Views
{
    /// <summary>List item view for adding a new test-package.</summary>
    public class AddPackageListItemView : TestHarnessViewBase
    {
        #region Head
        private readonly LinkButton addButton;

        public AddPackageListItemView()
        {
            // Setup initial conditions.
            SetCss(Css.Padding, "15px 10px");
            SetCss(Css.TextAlign, CssTextAlign.Right.ToString());

            // Insert the Add button.
            addButton = new LinkButton(StringLibrary.Add);
            Container.Append(addButton.Container);

            // Wire up events.
            addButton.Click += delegate { Common.Events.FireAddPackageClick(); };
        }
        #endregion
    }
}
