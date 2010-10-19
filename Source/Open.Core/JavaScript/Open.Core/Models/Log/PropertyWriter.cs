using System;
using System.Collections;
using Open.Core.Controls.HtmlPrimitive;
using Open.Core.Helpers;

namespace Open.Core
{
    internal class PropertyWriter
    {
        #region Head
        public const string KeyGetter = "get_";
        public const string KeyPrivate = "_";
        private const int MaxLength = 80;
        
        private readonly LogWriter writer;


        /// <summary>Constructor.</summary>
        public PropertyWriter(LogWriter writer)
        {
            this.writer = writer;
        }
        #endregion

        #region Properties
        private static StringHelper String { get { return Helper.String; } }
        #endregion

        #region Methods
        public void Write(object instance, string title)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(instance)) { writer.Write(instance, null); return; }

            // Format the title (generate default title if the caller did not specify it.  If 'null' was explicitly passed no title is shown).
            string titleIcon = null;
            if (Script.IsUndefined(title))
            {
                title = string.Format("{0}:", instance.GetType().Name);
                titleIcon = LogWriter.ClassIcon;
            }

            // Create the property list.
            IHtmlList list = writer.View.InsertList(title, null, null, titleIcon);
            list.Container.AddClass(LogCssClasses.PropertyList);

            // Insert the property values.
            foreach (DictionaryEntry entry in Dictionary.GetDictionary(instance))
            {
                string propName = GetPropertyName(entry.Key);
                if (propName == null) continue;

                string item = string.Format(
                                    "<span class='{0}'>{1}:</span>&nbsp;{2}",
                                    LogCssClasses.PropertyName,
                                    propName,
                                    GetPropertyValue(instance, entry));
                list.Add(item);
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

        private static string GetPropertyValue(object instance, DictionaryEntry property)
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
            bool hasValue = !Script.IsNullOrUndefined(value);

            // Prepare the CSS.
            string cssClass = LogCssClasses.PropertyValue;
            if (hasError) cssClass += " " + LogCssClasses.PropertyError;

            // Prepare the text version of the value.
            string text = hasValue
                              ? String.FormatToString(value.ToString())
                              : "<null>";
            text = text.HtmlEncode();
            if (hasValue) text = Shorten(FormatKnownValues(value, text));

            // Format into SPAN.
            return string.Format("<span class='{0}'>{1}</span>", cssClass, text);
        }

        private static string Shorten(string text)
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
            if (text.ToString() == "[object Object]") text = TypeName(value);
            return text;
        }

        private static string TypeName(object value)
        {
            string name = String.RemoveStart(value.GetType().Name, "_");
            return string.Format("[{0}]", String.ToSentenceCase(name));
        }
        #endregion
    }
}
