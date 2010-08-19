namespace Open.Core
{
    /// <summary>Represents a width and a height.</summary>
    public class Size
    {
        #region Head
        private readonly double width;
        private readonly double height;

        /// <summary>Constructor.</summary>
        /// <param name="width">The pixel width of the element.</param>
        /// <param name="height">The pixel height of the element.</param>
        public Size(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pixel width of the element.</summary>
        public double Width { get { return width; } }

        /// <summary>Gets or sets the pixel height of the element.</summary>
        public double Height { get { return height; } }
        #endregion
    }
}
