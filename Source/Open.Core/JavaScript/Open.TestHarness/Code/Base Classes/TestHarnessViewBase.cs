using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core;

namespace Open.Testing
{
    /// <summary>The base class for views within the TestHarness.</summary>
    public abstract class TestHarnessViewBase : ViewBase
    {
        #region Head
        private Common common;

        [AlternateSignature]
        protected extern TestHarnessViewBase();
        protected TestHarnessViewBase(jQueryObject container) : base(container)
        {
        }
        #endregion

        #region Properties : Protected
        /// <summary>Gets the common global properties (via the DI Container).</summary>
        protected Common Common { get { return common ?? (common = Common.GetFromContainer()); } }
        #endregion
    }
}
