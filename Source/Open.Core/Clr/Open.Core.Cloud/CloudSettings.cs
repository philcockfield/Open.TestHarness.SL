using System.ComponentModel.Composition;
using Open.Core.Composition;

namespace Open.Core.Cloud
{
    [Export(typeof(ICloudSettings))]
    public class CloudSettings : ICloudSettings
    {
        #region Head
        private static ICloudSettings current;
        #endregion

        #region Properties
        public string DataConnectionString { get; set; }
        public static ICloudSettings Current { get { return current ?? (current = new Importer().CloudSettings); } }
        #endregion

        private class Importer
        {
            public Importer()
            {
                AssemblyCompositionInitializer.SatisfyImports(this);
            }

            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public ICloudSettings CloudSettings { get; set; }
        }
    }
}
