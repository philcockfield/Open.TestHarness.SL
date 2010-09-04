using Open.Core.Lists;

namespace Open.TestHarness.Models
{
    /// <summary>A list-item node for a TestPackage.</summary>
    public class TestPackageListItem : ListItem
    {
        #region Head
        private readonly TestPackageInfo testPackage;

        /// <summary>Constructor.</summary>
        /// <param name="testPackage">The test-package this node represents.</param>
        public TestPackageListItem(TestPackageInfo testPackage)
        {
            // Setup initial conditions.
            this.testPackage = testPackage;

            // Set default values.
            Text = testPackage.Name;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public TestPackageInfo TestPackage { get { return testPackage; } }
        #endregion
    }
}