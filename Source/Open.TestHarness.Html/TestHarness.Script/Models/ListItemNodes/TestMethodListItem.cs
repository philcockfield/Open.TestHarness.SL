using Open.Core.Lists;

namespace Open.TestHarness.Models
{
    /// <summary>A list-item node for a single Test-Method.</summary>
    public class TestMethodListItem : ListItem
    {
        #region Head
        private readonly TestMethodInfo testMethod;

        /// <summary>Constructor.</summary>
        /// <param name="testMethod">The test-method this node represents.</param>
        public TestMethodListItem(TestMethodInfo testMethod)
        {
            // Setup initial conditions.
            this.testMethod = testMethod;

            // Set default values.
            Text = testMethod.DisplayName;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public TestMethodInfo TestMethod { get { return testMethod; } }
        #endregion
    }
}