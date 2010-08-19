namespace Open.Core
{
    /// <summary>Utility methods for working with reflection.</summary>
    public class ReflectionHelper
    {
        /// <summary>Determines whether the given object is a string.</summary>
        /// <param name="value">The object to examine.</param>
        public bool IsString(object value)
        {
            return value.GetType().Name == "String";
        }
    }
}
