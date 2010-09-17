using System.Collections;

namespace Open.Testing.Bootstrap
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            new Bootstrapper().Start();
        }
    }
}