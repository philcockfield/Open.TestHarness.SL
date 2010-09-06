namespace Open.Core
{
    /// <summary>Represents a width and a height.</summary>
    public class Size
    {
        #region Head
        private readonly int width;
        private readonly int height;

        /// <summary>Constructor.</summary>
        /// <param name="width">The pixel width of the element.</param>
        /// <param name="height">The pixel height of the element.</param>
        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pixel width of the element.</summary>
        public int Width { get { return width; } }

        /// <summary>Gets or sets the pixel height of the element.</summary>
        public int Height { get { return height; } }
        #endregion
    }
}
