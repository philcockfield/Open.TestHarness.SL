using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Represents a definition of a property.</summary>
    public class PropertyDef
    {
        #region Head
        private readonly Type declaringType;
        private readonly string name;
        private string formattedName;
        private static Dictionary singletons;

        /// <summary>Constructor.</summary>
        /// <param name="declaringType">The type of the object that exposes the property.</param>
        /// <param name="name">the name of the property.</param>
        public PropertyDef(Type declaringType, string name)
        {
            this.declaringType = declaringType;
            this.name = name;
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of the object that exposes the property.</summary>
        public Type DeclaringType { get { return declaringType; } }

        /// <summary>Gets the name of the property.</summary>
        public string Name { get { return name; } }

        /// <summary>Gets the property name with the same casing that is using in the emitted JavaScript.</summary>
        public string JavaScriptName { get { return formattedName ?? (formattedName = Helper.String.ToCamelCase(Name)); } }

        /// <summary>Gets the fully qualified name of the property..</summary>
        public string FullName { get { return DeclaringType.FullName + ":" + Name; } }
        #endregion

        #region Properties : Private
        private static Dictionary Singletons { get { return singletons ?? (singletons = new Dictionary()); } }
        #endregion

        #region Methods
        /// <summary>Gets a singleton version of the property definition.</summary>
        /// <param name="declaringType">The type of the object that exposes the property.</param>
        /// <param name="name">the name of the property.</param>
        public static PropertyDef GetSingletonDef(Type declaringType, string name)
        {
            // Return existing singleton.
            string key = string.Format("{0}:{1}", declaringType.FullName, name);
            if (Singletons.ContainsKey(key)) return Singletons[key] as PropertyDef;

            // Create and store as singleton.
            PropertyDef def = new PropertyDef(declaringType, name);
            Singletons[key] = def;

            // Finish up.
            return def;
        }
        #endregion
    }
}
