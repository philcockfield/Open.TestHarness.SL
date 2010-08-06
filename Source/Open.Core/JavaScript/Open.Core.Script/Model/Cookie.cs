using System;

namespace Open.Core
{
    /// <summary>Stores a set of properties within a cookie.</summary>
    /// <remarks>
    ///     Requires: jquery.cookie.js<br/>
    ///     Source: http://plugins.jquery.com/project/cookie
    /// </remarks>
    public class Cookie
    {
        #region Head
        private string id;
        private int expires;
        private PropertyBag propertyBag;

        /// <summary>Constructor.</summary>
        /// <param name="cookieId">The unique identifier of the cookie.</param>
        public Cookie(string cookieId)
        {
            id = cookieId;
            CreatePropertyBag();
        }
        #endregion

        #region Properties
        /// <summary>Gets the unique identifier of the cookie.</summary>
        public string Id
        {
            get { return id; }
            private set { id = value; }
        }

        /// <summary>Gets or sets the lifespan of the cookie (days).</summary>
        /// <remarks>Zero (0) if this is a session only cookie.</remarks>
        public int Expires
        {
            get { return expires; }
            set
            {
                if (value < 0) value = 0;
                expires = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>Saves the properties to the cookie.</summary>
        public void Save()
        {
            Script.Literal("$.cookie({0}, {1}, {{ expires: {2} }})", Id, propertyBag.ToJson(), Expires);
        }

        /// <summary>Deletes the cookie (and all associated property values).</summary>
        public void Clear()
        {
            Script.Literal("$.cookie({0}, null)", Id);
            CreatePropertyBag();
        }
        #endregion

        #region Methods : Read / Write
        /// <summary>Stores the given value.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        /// <param name="value">The value to store.</param>
        public void Set(string key, object value) { propertyBag.Set(key, value); }

        /// <summary>Retrieve the specified value.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        /// <returns>The property value, or null if there is no corresponding value.</returns>
        public object Get(string key) { return propertyBag.Get(key); }

        /// <summary>Determines whether there is a value for the given key.</summary>
        /// <param name="key">The unique identifier of the property.</param>
        public bool HasValue(string key) { return propertyBag.HasValue(key); }
        #endregion

        #region Internal
        private void CreatePropertyBag()
        {
            string json = ReadCookie();
            propertyBag =  String.IsNullOrEmpty(json) 
                                    ? PropertyBag.Create() 
                                    : PropertyBag.FromJson(json);
        }

        private string ReadCookie()
        {
            return Script.Literal("$.cookie({0})", Id) as string;
        }
        #endregion
    }
}
