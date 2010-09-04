using Open.Core.Lists;

namespace Open.TestHarness.Models
{
    /// <summary>A list-item node for a TestClass.</summary>
    public class ClassListItem : ListItem
    {
        #region Head
        private readonly ClassInfo classInfo;

        /// <summary>Constructor.</summary>
        /// <param name="classInfo">The test-class this node represents.</param>
        public ClassListItem(ClassInfo classInfo)
        {
            // Setup initial conditions.
            this.classInfo = classInfo;

            // Set default values.
            Text = classInfo.DisplayName;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public ClassInfo ClassInfo { get { return classInfo; } }
        #endregion
    }
}