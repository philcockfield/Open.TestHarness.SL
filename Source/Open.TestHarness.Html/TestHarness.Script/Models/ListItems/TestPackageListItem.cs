using Open.Core.Lists;

namespace Open.TestHarness.Models
{
    /// <summary>A list-item node for a TestPackage.</summary>
    public class TestPackageListItem : ListItem
    {
        #region Head
        private readonly TestPackageDef testPackage;

        /// <summary>Constructor.</summary>
        /// <param name="testPackage">The test-package this node represents.</param>
        public TestPackageListItem(TestPackageDef testPackage)
        {
            // Setup initial conditions.
            this.testPackage = testPackage;

            // Set default values.
            Text = "FOO! TestPackage"; //TEMP 
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public TestPackageDef TestPackage { get { return testPackage; } }
        #endregion
    }
}