using System;
using System.Collections;
using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;
using Open.Core.Helpers;

namespace Open.Core
{
    public class PropertyListBuilder 
    {
        #region Head
        public const string KeyGetter = "get_";
        public const string KeyPrivate = "_";
        private int maxLength = 80;
       private readonly object instance;
        private bool isDictionary;

        /// <summary>Constructor.</summary>
        public PropertyListBuilder(object instance, bool isDictionary)
        {
            this.instance = instance;
            IsDictionary = isDictionary;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the instance object is a Dictionary.</summary>
        public bool IsDictionary
        {
            get { return isDictionary; }
            set { isDictionary = value; }
        }

        /// <summary>Gets or sets the instance object that the list is being generated for.</summary>
        public object Instance{get { return instance; }}

        /// <summary>Gets or sets the maximum string length of values, after which they are shortened with elipses.</summary>
        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; }
        }

        private static StringHelper String { get { return Helper.String; } }
        #endregion

        #region Methods
        /// <summary>Writes to the given list.</summary>
        /// <param name="htmlList">The HTML list.</param>
        public void Write(IHtmlList htmlList)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(instance)) return;

            // Create the property list.
            htmlList.Container.AddClass(LogCssClasses.PropertyList);

            // Insert the property values.
            foreach (DictionaryEntry entry in Dictionary.GetDictionary(instance))
            {
                string propName = isDictionary ? entry.Key : GetPropertyName(entry.Key);
                if (propName == null) continue;

                object value = isDictionary ?
                                                FormatPropertyValue(entry.Value, false) : 
                                                GetPropertyValue(instance, entry);
                string item = string.Format(
                    "<span class='{0}'>{1}:</span>&nbsp;{2}",
                    LogCssClasses.PropertyName,
                    propName,
                    value);
                htmlList.Add(item);
            }            
        }
        #endregion

        #region Internal
        private static string GetPropertyName(string key)
        {
            if (!key.StartsWith(KeyGetter)) return null;
            key = String.RemoveStart(key, KeyGetter);
            if (key.StartsWith(KeyPrivate)) return null;
            return String.ToSentenceCase(key);
        }

        private string GetPropertyValue(object instance, DictionaryEntry property)
        {
            // Retrieve the value.
            object value = null;
            bool hasError = false;
            try
            {
                Function func = Helper.Reflection.GetFunction(instance, property.Key);
                if (func != null) value = func.Call(instance);
            }
            catch (Exception e)
            {
                hasError = true;
                value = "ERROR: " + e.Message;
            }

            // Finish up.
            return FormatPropertyValue(value, hasError);
        }

        private string FormatPropertyValue(object value, bool hasError)
        {
            // Setup initial conditions.
            bool hasValue = !Script.IsNullOrUndefined(value);

            // Prepare the CSS.
            string cssClass = LogCssClasses.PropertyValue;
            if (hasError) cssClass += " " + LogCssClasses.PropertyError;

            // Prepare the text version of the value.
            string text = hasValue
                              ? String.FormatToString(value)
                              : "<null>".HtmlEncode();
            if (hasValue)
            {
                text = FormatKnownValues(value, text);
                text = Shorten(text.HtmlEncode());
            }

            // Format into SPAN.
            return string.Format("<span class='{0}'>{1}</span>", cssClass, text);
        }

        private string Shorten(string text)
        {
            if (text.Length < MaxLength) return text;
            return string.Format(
                "{0}... <span class='{1}'>({2} characters)</span>", 
                text.Substr(0, MaxLength), 
                LogCssClasses.TotalChars,
                text.Length);
        }

        private static string FormatKnownValues(object value, string text)
        {
            try
            {
                // Look for array list.
                ArrayList list = value as ArrayList;
                if (list != null) return string.Format("[{0}:{1}]", TypeName(value), list.Count);

                // JQuery.
                string jQueryObjText = JQueryObject(value);
                if (jQueryObjText != null) return jQueryObjText;

                // Convert [object] to Type Name.
                if (text == "[object Object]") text = string.Format("[{0}]", TypeName(value));
            }
            catch (Exception)
            {
                // Ignore - return original value.
            }

            // Finish up.
            return text;
        }

        private static string TypeName(object value)
        {
            string name = String.RemoveStart(value.GetType().Name, "_");
            return String.ToSentenceCase(name);
        }

        private static string JQueryObject(object value)
        {
            if (value is string) return null;
            try
            {
                jQueryObject obj = (jQueryObject)value;
                if (Script.IsNullOrUndefined(obj.Length)) return null;
                return string.Format("[jQueryObject.Length:{0}]", obj.Length);
            }
            catch (Exception)
            {
                // Ignore.
                return null;
            }
        }
        #endregion
    }
}
