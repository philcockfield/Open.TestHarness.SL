using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a single item within a list.</summary>
    public class ListItem : ViewBase
    {
        #region Head
        private readonly object model;

        /// <summary>Constructor.</summary>
        /// <param name="liElement">The containing <li></li> element.</param>
        /// <param name="model">The data model for the list item.</param>
        public ListItem(jQueryObject liElement, object model)
        {
            this.model = model;
            Initialize(liElement);
        }
        #endregion

        #region Methods
        /// <summary>Initializes the list-item.</summary>
        /// <param name="liElement">The containing <li></li> element.</param>
        protected override void OnInitialize(jQueryObject liElement)
        {
            object text = Type.GetProperty(model, "text");
            liElement.Append(
                            string.Format("<span>{0}</span>", 
                            Script.IsNullOrUndefined(text) ? "No Text" : text)); 
        }
        #endregion
    }
}
