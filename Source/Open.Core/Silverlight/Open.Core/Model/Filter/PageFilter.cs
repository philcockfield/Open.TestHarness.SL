namespace Open.Core.Common
{
    /// <summary>Defines the size and position of a page of data to return from a query.</summary>
    public class PageFilter : IPageFilter
    {
        #region Head
        private int skipCount;
        private int takeCount = int.MaxValue;
        #endregion

        #region Properties
        /// <summary>Gets or sets the number of items to skip before taking the page (this behavior matches the Skip method in LINQ).</summary>
        public int SkipCount
        {
            get { return skipCount; }
            set { skipCount = value.WithinBounds(0, int.MaxValue); }
        }

        /// <summary>Gets or sets the number of items to include in the page (this behavior matches the Take method in LINQ).</summary>
        public int TakeCount
        {
            get { return takeCount; }
            set { takeCount = value.WithinBounds(0, int.MaxValue); }
        }
        #endregion
    }
}
