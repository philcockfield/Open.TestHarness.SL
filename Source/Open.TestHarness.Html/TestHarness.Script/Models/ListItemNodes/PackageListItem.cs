using Open.Core.Lists;

namespace Open.Testing.Models
{
    /// <summary>A list-item node for a TestPackage.</summary>
    public class PackageListItem : ListItem
    {
        #region Head
        private readonly PackageInfo testPackage;

        /// <summary>Constructor.</summary>
        /// <param name="testPackage">The test-package this node represents.</param>
        public PackageListItem(PackageInfo testPackage)
        {
            // Setup initial conditions.
            this.testPackage = testPackage;

            // Set default values.
            Text = testPackage.Name;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public PackageInfo TestPackage { get { return testPackage; } }
        #endregion
    }
}