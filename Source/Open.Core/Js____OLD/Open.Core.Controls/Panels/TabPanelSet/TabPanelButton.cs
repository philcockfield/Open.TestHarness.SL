using System;
using Open.Core.Controls.Buttons;

namespace Open.Core.Controls
{
    /// <summary>A single tab button for a single panel.</summary>
    public class TabPanelButton : ButtonView
    {
        #region Head
        /// <summary>Constructor.</summary>
        public TabPanelButton(TabPanel panel) : base(panel.Button)
        {
            IsLoaded = false;
            Panel = panel;
            SetCss(Css.Position, Css.Absolute);
            Width = panel.ButtonWidth;
            Height = 24;
            Model.CanToggle = true;

            Helper.Template.GetAsync(TabPanelSetView.TemplateUrl, "#tmplTabPanelSet", delegate(Template template)
                            {
                                // Setup button.
                                TemplateForStates(0, AllStates, "#tmplTabPanelBtn_Bg");
                                TemplateForState(1, ButtonState.MouseOver, "#tmplTabPanelBtn_BgOver");
                                TemplateForStates(1, DownAndPressed, "#tmplTabPanelBtn_BgPressed");

                                // Text
                                TemplateForStates(2, AllStates, "#tmplTabPanelBtn_Text");
                                CssForStates(2, NotDownOrPressed, "c_tabPanelButton text normal");
                                CssForStates(2, DownAndPressed, "c_tabPanelButton text pressed");

                                // Wire up events.
                                panel.PropertyChanged += OnModelPropertyChanged;

                                // Finish up.
                                SyncVisibility();
                                UpdateLayout();

                                // Finish up.
                                FireLoaded();
                            });
        }
        #endregion

        #region Event Handlers
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name == TabPanel.PropIsVisible) SyncVisibility();
            if (e.Property.Name == TabPanel.PropIsOverflowing) UpdateLayout();
        }
        #endregion

        #region Properties
        /// <summary>Gets the panel that this button represents.</summary>
        public readonly TabPanel Panel;
        #endregion

        #region Methods
        protected override bool OnIsVisibleChanging(bool isVisible)
        {
            return Panel.IsOverflowing ? false : isVisible;
        }
        #endregion

        #region Internal
        private void SyncVisibility()
        {
            IsVisible = Panel.IsVisible;
        }
        #endregion
    }
}