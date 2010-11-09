using System;
using System.Collections;
using Open.Core.Controls.HtmlPrimitive;

namespace Open.Core
{
    internal class PropertyWriter
    {
        #region Head
        private readonly LogWriter writer;

        /// <summary>Constructor.</summary>
        public PropertyWriter(LogWriter writer)
        {
            if (Script.IsNullOrUndefined(writer)) throw new Exception("[Null] Log writer not supplied.");
            this.writer = writer;
        }
        #endregion

        #region Methods
        public void WriteProperties(object instance, string title) { Write(instance, title, false); }
        public void WriteDictionary(Dictionary instance, string title) { Write(instance, title, true); }

        private void Write(object instance, string title, bool isDictionary)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(instance)) { writer.Write(instance, null); return; }
            if (writer.View == null) return;

            // Format the title (generate default title if the caller did not specify it.  If 'null' was explicitly passed no title is shown).
            string titleIcon = null;
            if (Script.IsUndefined(title))
            {
                title = string.Format("{0}:", isDictionary ? "Dictionary" :  instance.GetType().Name);
                titleIcon = LogWriter.ClassIcon;
            }

            // Create the property list.
            IHtmlList htmlList = writer.View.InsertList(title, null, null, titleIcon);
            new PropertyListBuilder(instance, isDictionary).Write(htmlList);
        }
        #endregion
    }
}
