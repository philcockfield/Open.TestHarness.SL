using System;

namespace Open.Core
{
    /// <summary>Testing assertions.</summary>
    public static class Should
    {
        #region Head
        /// <summary>Asserts that an object is equal to another object (uses != comparison).</summary>
        /// <param name="subject">The value being compared.</param>
        /// <param name="value">The value to compare to.</param>
        public static void Equal(object subject, object value)
        {
            if (subject != value) ThrowError("The two values are not equal.");
        }

        /// <summary>Asserts that an object is not equal to another object (uses != comparison).</summary>
        /// <param name="subject">The value being compared.</param>
        /// <param name="value">The value to compare to.</param>
        public static void NotEqual(object subject, object value)
        {
            if (subject == value) ThrowError("The two values should not be equal.");
        }

        /// <summary>Asserts that an object is not null.</summary>
        /// <param name="subject">The value being examined.</param>
        public static void NotBeNull(object subject)
        {
          if (subject == null) ThrowError("Value should not be null.");
        }

        /// <summary>Asserts that an object is not null.</summary>
        /// <param name="subject">The value being examined.</param>
        public static void BeNull(object subject)
        {
            if (subject != null) ThrowError("Value should be null.");
        }

        /// <summary>Asserts that an value is True.</summary>
        /// <param name="value">The value being examined.</param>
        public static void BeTrue(bool value)
        {
            if (value != true) ThrowError("Value should be True.");
        }

        /// <summary>Asserts that an value is False.</summary>
        /// <param name="value">The value being examined.</param>
        public static void BeFalse(bool value)
        {
            if (value != false) ThrowError("Value should be False.");
        }
        #endregion

        #region Internal
        private static void ThrowError(string message)
        {
            throw new Exception(string.Format("AssertionException: " + message));
        }
        #endregion
    }
}
