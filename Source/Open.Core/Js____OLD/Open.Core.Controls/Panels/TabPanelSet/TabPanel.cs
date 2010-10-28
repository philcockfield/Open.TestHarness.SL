using System;
using jQueryApi;
using Open.Core.Controls.Buttons;

namespace Open.Core.Controls
{
    /// <summary>Represents of a panel in a tab-set.</summary>
    public class TabPanel : ModelBase
    {
        #region Head
        public const string PropIsVisible = "IsVisible";
        public const string PropTitle = "Title";
        public const string PropIsOverflowing = "IsOverflowing";

        /// <summary>Fires when the panel is selected.</summary>
        public event EventHandler Selected;
        private void FireSelected(){if (Selected != null) Selected(this, new EventArgs());}

        /// <summary>Fires when the visibility of the panel changes.</summary>
        public event EventHandler VisibilityChanged;
        private void FireVisibilityChanged(){if (VisibilityChanged != null) VisibilityChanged(this, new EventArgs());}

        internal TabPanel(string title, int buttonWidth)
        {
            // Initialize the button.
            Button = new ButtonBase();
            ButtonWidth = buttonWidth;
            Title = title;

            // Initialize the DIV.
            Div = Html.CreateDiv();
            Css.AbsoluteFill(Div);

            // Wire up events.
            Button.IsPressedChanged += delegate
                                           {
                                               SyncVisibility();
                                               if (Button.IsPressed) FireSelected();
                                           };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the title of the panel.</summary>
        public string Title
        {
            get { return (string) Get(PropTitle, "Untitled"); }
            set
            {
                if (Set(PropTitle, value, "Untitled"))
                {
                    Button.TemplateData["buttonText"] = value;
                    Button.UpdateLayout();
                }
            }
        }

        /// <summary>Gets the pixel width of the panel's button.</summary>
        public readonly int ButtonWidth;

        /// <summary>Gets the tab button.</summary>
        public readonly IButton Button;

        /// <summary>Gets the DIV content element.</summary>
        public readonly jQueryObject Div;

        /// <summary>True if this panel is selected.</summary>
        public bool IsSelected { get { return Button.IsPressed; } }

        /// <summary>Gets or sets whether the panel is visible.</summary>
        public bool IsVisible
        {
            get { return (bool) Get(PropIsVisible, true); }
            set
            {
                if (Set(PropIsVisible, value, true))
                {
                    SyncVisibility();
                    FireVisibilityChanged();
                    if (!value && IsSelected) Button.IsPressed = false;
                }
            }
        }

        /// <summary>Gets whether the tab is currently overflowing off the screen.</summary>
        public bool IsOverflowing
        {
            get { return (bool) Get(PropIsOverflowing, false); }
            set { Set(PropIsOverflowing, value, false); }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", GetType().Name, Title);
        }

        /// <summary>Causes the panel to be the currently selected view.</summary>
        public void Select()
        {
            Button.IsPressed = true;
        }
        #endregion

        #region Internal
        private void SyncVisibility()
        {
            Css.SetVisibility(Div, IsVisible && IsSelected);
        }
        #endregion
    }
}