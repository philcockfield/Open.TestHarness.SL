using System;

namespace Open.Core
{
    /// <summary>Stores properties in a backing JavaScript object.</summary>
    public class PropertyBag
    {
        #region Head
        private readonly object backingObject;

        /// <summary>Constructor.</summary>
        private PropertyBag(string json)
        {
            if (Script.IsNullOrUndefined(json))
            {
                backingObject = Script.Literal(" {}");
            }
            else
            {
                backingObject = Script.Literal("JSON.parse( json )");
            }
        }
        #endregion

        #region Methods
        /// <summary>Retrieve the specified value.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        /// <returns>The property value, or null if there is no corresponding value.</returns>
        public object Get(string key)
        {
            string script = string.Format("this._backingObject.{0}", key);
            return Script.Eval(script);
        }

        /// <summary>Stores the given value.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        /// <param name="value">The value to store.</param>
        public void Set(string key, object value)
        {
            string script = string.Format("this._backingObject.{0} = {1}", key, value);
            Script.Eval(script);
        }

        /// <summary>Determines whether there is a value for the given key.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        public bool HasValue(string key)
        {
            return !Script.IsNullOrUndefined(Get(key));
        }

        /// <summary>Converts the property-bag to a JSON string.</summary>
        public string ToJson()
        {
            return Script.Literal("JSON.stringify( this._backingObject )") as string;
        }
        #endregion

        #region Methods : Static
        /// <summary>Factory method.  Create an empty property bag.</summary>
        public static PropertyBag Create() { return new PropertyBag(null); }

        /// <summary>Reconstructs a property-bag from the given JSON string.</summary>
        /// <param name="json">The JSON string to parse.</param>
        public static PropertyBag FromJson(string json) { return new PropertyBag(json); }
       #endregion
    }
}
