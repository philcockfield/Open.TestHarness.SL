using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>The logical controller for a view (visual UI control) contained with an HTML element.</summary>
    public interface IView
    {
        #region Events
        /// <summary>Fires when the IsVisible property changes.</summary>
        event EventHandler VisibilityChanged;

        /// <summary>Fires when the size changes.</summary>
        event EventHandler SizeChanged;
        #endregion

        #region Initialization
        /// <summary>Gets or sets whether the view has been initialized.</summary>
        bool IsInitialized { get; }

        /// <summary>Initializes the view.</summary>
        /// <param name="container">The containing element of the view.</param>
        void Initialize(jQueryObject container);

        /// <summary>Gets the element that the view is contained within.</summary>
        jQueryObject Container { get; }

        /// <summary>Destroys the view and clears resources.</summary>
        void Dispose();
        #endregion

        #region Styles
        /// <summary>Gets or sets the background CSS for the view.</summary>
        string Background { get; set; }

        /// <summary>Gets or sets whether the control is visible.</summary>
        bool IsVisible { get; set; }

        /// <summary>Gets or sets the opacity percentage (0-1).</summary>
        double Opacity { get; set; }

        /// <summary>Gets or sets the pixel width of the control.</summary>
        int Width { get; set; }

        /// <summary>Gets or sets the pixel height of the control.</summary>
        int Height { get; set; }

        /// <summary>Changes the size of the control (causing the SizeChanged event to fire only once).</summary>
        /// <param name="width">The pixel width of the control.</param>
        /// <param name="height">The pixel height of the control.</param>
        void SetSize(int width, int height);

        /// <summary>Gets the specified CSS value.</summary>
        /// <param name="attribute">The CSS attribute name.</param>
        /// <returns>The CSS value, or null if it has not been specified.</returns>
        string GetCss(string attribute);

        /// <summary>Assigns the specified CSS value.</summary>
        /// <param name="attribute">The CSS attribute name.</param>
        /// <param name="value">The CSS value.</param>
        void SetCss(string attribute, string value);
        #endregion
    }
}
