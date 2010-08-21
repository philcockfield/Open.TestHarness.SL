using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with JSON.</summary>
    public class JsonHelper
    {
        /// <summary>Serialized the given object to a JSON string.</summary>
        /// <param name="value">The object to serialize.</param>
        public string Serialize(object value)
        {
            return Script.Literal("JSON.stringify( {0} )", value) as string;
        }

        /// <summary>Parses the given JSON into an object.</summary>
        /// <param name="json">The JSON to parse.</param>
        public object Parse(string json)
        {
            return Script.Literal("JSON.parse( json )");
        }
    }
}
