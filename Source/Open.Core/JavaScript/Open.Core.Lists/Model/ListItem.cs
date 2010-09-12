namespace Open.Core.Lists
{
    /// <summary>An implementation of a TreeNode for inclusion within the ListTree control.</summary>
    public class ListItem : TreeNode, IListItem, IHtmlFactory
    {
        #region Head
        public const string PropText = "Text";
        public const string PropCanSelect = "CanSelect";
        public const string PropRightIconSrc = "RightIconSrc";
        #endregion

        #region Properties : IListItem
        public string Text
        {
            get { return (string) Get(PropText, null); }
            set { Set(PropText, value, null); }
        }

        public bool CanSelect
        {
            get { return (bool) Get(PropCanSelect, true); }
            set
            {
                if (Set(PropCanSelect, value, true))
                {
                    IsSelected = false;
                }
            }
        }

        public string RightIconSrc
        {
            get { return (string) Get(PropRightIconSrc, null); }
            set { Set(PropRightIconSrc, value, null); }
        }
        #endregion

        #region Methods
        /// <summary>Allows deriving classes to provide custom HTML for the item.</summary>
        /// <returns>A string of HTML, or Null if the default HTML is to be used.</returns>
        /// <remarks>
        ///     To reuse default behavior, use the following classes on child-elements of the returned HTML:
        ///     - itemLabel:    The display text of the item.
        /// </remarks>
        public virtual string CreateHtml() { return null; }

        public override string ToString() { return string.Format("{0} {1}", base.ToString(), Text); }

        protected override void OnIsSelectedChanged()
        {
            if (!CanSelect && IsSelected) IsSelected = false;
            base.OnIsSelectedChanged();
        }
        #endregion

        #region Methods : Static
        /// <summary>Creates a new tree node (factory).</summary>
        /// <param name="text">The value of the 'Text' property.</param>
        public static ListItem Create(string text)
        {
            ListItem node = new ListItem();
            node.Text = text;
            return node;
        }
        #endregion
    }
}
