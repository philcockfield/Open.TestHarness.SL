using Open.Core;

namespace Open.Testing
{
    /// <summary>The base class for controllers within the TestHarness.</summary>
    public abstract class TestHarnessControllerBase : ControllerBase
    {
        #region Head
        private Common common;
        #endregion

        #region Properties : Protected
        /// <summary>Gets the common global properties (via the DI Container).</summary>
        protected Common Common { get { return common ?? (common = Common.GetFromContainer()); } }
        #endregion
    }
}
