using Open.Core.Lists;

namespace Open.Testing.Models
{
    /// <summary>A list-item node for a single Test-Method.</summary>
    public class MethodListItem : ListItem
    {
        #region Head
        private readonly MethodInfo method;

        /// <summary>Constructor.</summary>
        /// <param name="method">The test-method this node represents.</param>
        public MethodListItem(MethodInfo method)
        {
            // Setup initial conditions.
            this.method = method;

            // Set default values.
            Text = method.DisplayName;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public MethodInfo Method { get { return method; } }
        #endregion
    }
}