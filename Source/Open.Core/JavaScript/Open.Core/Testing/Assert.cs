using System;

namespace Open.Core
{
    /// <summary>BDD style testing assertions.</summary>
    public class Assert
    {
        #region Head
        private readonly object subject;

        private Assert(object subject)
        {
            this.subject = subject;
        }
        #endregion

        #region Methods : Static
        /// <summary>Factory method for starting an assertion.</summary>
        /// <param name="value">The value being examined.</param>
        /// <returns>A new assertion object allowing to you call an assert.</returns>
        public static Assert That(object value)
        {
            return new Assert(value);
        }
        #endregion

        #region Methods (Assertions)
        /// <summary>Asserts that an object is equal to another object (uses != comparison).</summary>
        /// <param name="value">The value to compare to.</param>
        public void Is(object value)
        {
            bool isSame = (bool)Script.Literal("{0} === {1}", subject, value);
            if (!isSame) ThrowError(string.Format("The two values '{0}' and '{1}' are not equal.", Format(subject), Format(value)));
        }

        /// <summary>Asserts that an object is not equal to another object (uses != comparison).</summary>
        /// <param name="value">The value to compare to.</param>
        public void IsNot(object value)
        {
            if (subject == value) ThrowError(string.Format("The two values '{0}' and '{1}' should not be equal.", Format(subject), Format(value)));
        }

        /// <summary>Asserts that an object is not null.</summary>
        public void IsNotNull()
        {
            if (subject == null) ThrowError("Value should not be null.");
        }

        /// <summary>Asserts that an object is not null.</summary>
        public void IsNull()
        {
            if (subject != null) ThrowError(string.Format("The value '{0}' should actually be null.", Format(subject)));
        }

        /// <summary>Asserts that an value is True.</summary>
        public void IsTrue()
        {
            if (!(subject is bool)) ThrowError("Value is not a boolean.");
            if ((bool)subject != true) ThrowError("Value should be True.");
        }

        /// <summary>Asserts that an value is False.</summary>
        public void IsFalse()
        {
            if (!(subject is bool)) ThrowError("Value is not a boolean.");
            if ((bool)subject != false) ThrowError("Value should be False.");
        }
        #endregion

        #region Internal
        private static void ThrowError(string message)
        {
            throw new Exception(string.Format("AssertionException: " + message));
        }

        private static string Format(object value)
        {
            return Helper.String.FormatToString(value);
        }
        #endregion
    }
}
