namespace Open.Core
{
    /// <summary>Defines an object that has size (width and height).</summary>
    public interface ISize
    {
        /// <summary>Gets or sets the pixel width of the control.</summary>
        int Width { get; set; }

        /// <summary>Gets or sets the pixel height of the control.</summary>
        int Height { get; set; }

        /// <summary>Changes the size of the control (causing the SizeChanged event to fire only once).</summary>
        /// <param name="width">The pixel width of the control.</param>
        /// <param name="height">The pixel height of the control.</param>
        void SetSize(int width, int height);
    }
}
