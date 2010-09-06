using System.Collections;
using Open.Core.Test.ViewTests;
using Open.Testing;

namespace Open.Core.Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            TestHarness.RegisterClass(typeof(LoadHelperTest));
        }
    }
}
