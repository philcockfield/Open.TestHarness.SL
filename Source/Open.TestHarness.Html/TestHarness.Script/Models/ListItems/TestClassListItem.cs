using Open.Core.Lists;

namespace Open.TestHarness.Models
{
    /// <summary>A list-item node for a TestClass.</summary>
    public class TestClassListItem : ListItem
    {
        #region Head
        private readonly TestClassDef testClass;

        /// <summary>Constructor.</summary>
        /// <param name="testClass">The test-class this node represents.</param>
        public TestClassListItem(TestClassDef testClass)
        {
            // Setup initial conditions.
            this.testClass = testClass;

            // Set default values.
            Text = testClass.Type.Name;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package this node represents.</summary>
        public TestClassDef TestClass { get { return testClass; } }
        #endregion
    }
}