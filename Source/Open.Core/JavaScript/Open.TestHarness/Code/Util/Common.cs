using Open.Core;
using Open.Testing.Internal;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing
{
    /// <summary>Provides access to common global object.</summary>
    public class Common
    {
        #region Properties
        public DiContainer Container { get { return DiContainer.DefaultContainer; } }
        public TestHarnessEvents Events { get { return Container.GetSingleton(typeof(ITestHarnessEvents)) as TestHarnessEvents; } }
        public ShellView Shell { get { return Container.GetSingleton(typeof(ShellView)) as ShellView; } }
        public CommonButtons Buttons { get { return Container.GetSingleton(typeof(CommonButtons)) as CommonButtons; } }
        #endregion

        #region Methods : Static
        /// <summary>Retrieves the common object from the container.</summary>
        public static Common GetFromContainer()
        {
            return DiContainer.DefaultContainer.GetSingleton(typeof(Common)) as Common;
        }
        #endregion
    }
}
