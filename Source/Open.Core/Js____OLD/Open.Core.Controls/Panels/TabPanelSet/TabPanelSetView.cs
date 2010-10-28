using System;
using jQueryApi;
using Urls = Open.Core.Controls.Urls; // Required for Script#

namespace Open.Core.Controls
{
    internal class TabPanelSetView : ViewBase
    {
        #region Head
        public const string PropIsOverflowing = "IsOverflowing";
        public const string SelectorOverflowing = "div.overflowing";

        internal const string TemplateUrl = Urls.Controls + "/TabPanelSet";
        private readonly TabPanelSet model;
        private jQueryObject divOverflowing;

        public TabPanelSetView(TabPanelSet model)
        {
            // Setup initial conditions.
            IsLoaded = false;
            this.model = model;

            // Load the template.
            Helper.Template.GetAsync(TemplateUrl, "#tmplTabPanelSet", delegate(Template template)
                                            {
                                                template.AppendTo(Container, model);
                                                divOverflowing = Container.Find(SelectorOverflowing);
                                                SyncOverflowingVisibility();
                                                IsLoaded = true;
                                                FireLoaded();
                                            });
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the tabs-set is in a state of tabs overflowing the width of the control.</summary>
        public bool IsOverflowing
        {
            get { return (bool) Get(PropIsOverflowing, false); }
            set
            {
                if (Set(PropIsOverflowing, value, false)) SyncOverflowingVisibility();
            }
        }
        #endregion

        #region Internal
        private void SyncOverflowingVisibility()
        {
            Css.SetDisplay(divOverflowing, IsOverflowing);
        }
        #endregion
    }
}