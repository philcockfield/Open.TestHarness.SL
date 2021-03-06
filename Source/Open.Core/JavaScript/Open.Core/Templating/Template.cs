using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>A compiled jQuery template.</summary>
    /// <remarks>
    ///     Use the method 'Helper.Template.Get(selector)' to download and create an instances of the template.
    ///     
    ///     Source for jQuery plugin: http://github.com/nje/jquery-tmpl
    ///     Documentation: http://api.jquery.com/category/plugins/templates/
    ///     Introductory articles: 
    ///         http://weblogs.asp.net/scottgu/archive/2010/10/04/jquery-templates-data-link-and-globalization-accepted-as-official-jquery-plugins.aspx
    ///         http://blog.reybango.com/2010/07/09/not-using-jquery-javascript-templates-youre-really-missing-out/
    ///         http://blog.reybango.com/2010/07/14/jquery-javascript-templates-tutorial-inline-expressions-and-code-blocks/
    ///     
    ///     Data-Linking: http://github.com/jquery/jquery-datalink
    /// </remarks>
    public class Template
    {
        #region Head
        private readonly string id;
        private readonly string selector;

        /// <summary>Constructor.</summary>
        /// <param name="selector">The CSS selector for the script block containing the template HTML.</param>
        public Template(string selector)
        {
            // Setup initial conditions.
            id = Helper.CreateId();
            this.selector = selector;
            if (TemplateHtml == null) throw new Exception(string.Format("A template at the selector '{0}' does not exist.", selector));

            // Compile the template for later use.
            Script.Literal("$.template({0}, {1})", id, TemplateHtml);
        }
        #endregion

        #region Properties
        /// <summary>Gets the CSS selector for the script block containing the template HTML.</summary>
        public string Selector { get { return selector; } }

        /// <summary>Gets the source template HTML.</summary>
        public string TemplateHtml
        {
            get
            {
                jQueryObject template = jQuery.Select(selector);
                return template.Length == 0 ? null : template.GetHtml();
            }
        }
        #endregion

        #region Methods
        public override string ToString() { return Helper.String.FormatToString(TemplateHtml); }

        /// <summary>Processes the template with the specified data and appends the result to the given target.</summary>
        /// <param name="target">The target to append HTML to.</param>
        /// <param name="data">The source data for the template to read from.</param>
        public void AppendTo(jQueryObject target, object data)
        {
            Script.Literal("$.tmpl( {0}, {1} ).appendTo( {2} )", TemplateHtml, data, target);
        }

        /// <summary>Renders the template to HTML using the specified data.</summary>
        /// <param name="data">The source data for the template to read from.</param>
        public string ToHtml(object data) { return WithinDiv(data).GetHtml(); }

        /// <summary>Redners the template to a jQueryObject using the specified data.</summary>
        /// <param name="data">The source data for the template to read from.</param>
        public jQueryObject ToElement(object data) { return WithinDiv(data).Children().First(); }
        #endregion

        #region Internal
        jQueryObject WithinDiv(object data)
        {
            jQueryObject div = Html.CreateDiv();
            AppendTo(div, data);
            return div;
        }
        #endregion
    }
}
