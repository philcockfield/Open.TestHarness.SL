using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

// Source:  http://blogs.captechconsulting.com/blog/kevin-hazzard/fluent-xml-parsing-using-cs-dynamic-type-part-1
//              http://blogs.captechconsulting.com/blog/kevin-hazzard/fluent-xml-parsing-using-cs-dynamic-type-part-2

namespace Open.Core
{
    /// <summary>
    ///     A dynamic wrapper for XML content allowing the document 
    ///     structure be extracted by fluently dotting in to the content.
    /// </summary>
    public class DynamicXml : DynamicObject, IEnumerable
    {
        #region Head
        private const string Value = "Value";
        private const string Count = "Count";

        private readonly List<XElement> xmlElements;

        /// <summary>Constructor.</summary>
        /// <param name="xml">The raw XML content to wrap.</param>
        public DynamicXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            xmlElements = new List<XElement> { doc.Root };
        }
        #endregion

        #region Methods
        public IEnumerator GetEnumerator()
        {
            foreach (var element in xmlElements)
            {
                yield return new DynamicXml(element);
            }
        }

        protected DynamicXml(XElement element) { xmlElements = new List<XElement> { element }; }
        protected DynamicXml(IEnumerable<XElement> elements) { xmlElements = new List<XElement>(elements); }
        #endregion

        #region Methods : Override
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Setup initial conditions.
            result = null;

            // Handle the Value and Count special cases.
            switch (binder.Name)
            {
                case Value:
                    result = xmlElements[0].Value;
                    break;

                case Count:
                    result = xmlElements.Count;
                    break;

                default:
                    {
                        // Try to find a named attribute first.
                        var attr = xmlElements[0].Attribute(XName.Get(binder.Name));
                        if (attr != null)
                        {
                            // If a named attribute was found, return that NON-dynamic object.
                            result = attr;
                        }
                        else
                        {
                            // Find the named descendants.
                            var items = xmlElements.Descendants(XName.Get(binder.Name));
                            if (items != null && items.Count() > 0)
                            {
                                // Prepare a new dynamic object with the list of found descendants.
                                result = new DynamicXml(items);
                            }
                        }
                    }
                    break;
            }

            if (result == null)
            {
                // Element not found, create a new element here.
                xmlElements[0].AddFirst(new XElement(binder.Name));
                result = new DynamicXml(xmlElements[0].Descendants().First());
            }

            // Finish up.
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (binder.Name == Value)
            {
                // The Value property is the only one that may be modified. TryGetMember
                // creates new XML elements in this implementation.
                xmlElements[0].Value = value == null ? string.Empty : value.ToString();
                return true;
            }
            return false;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var firstIndex = (int)indexes[0];
            result = new DynamicXml(xmlElements[firstIndex]);
            return true;
        }        
        #endregion
    }
}
