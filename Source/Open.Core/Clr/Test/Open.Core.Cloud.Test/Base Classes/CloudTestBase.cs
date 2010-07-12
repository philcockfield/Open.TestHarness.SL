using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Open.Core.Cloud.TableStorage;
using Open.Core.Composition;

namespace Open.Core.Cloud.Test
{
    public abstract class CloudTestBase
    {
        #region Head
        private static bool isMefInitialized;

        // Example connection string: "AccountName=azurestorage;AccountKey=base64key;DefaultEndpointsProtocol=https"

        protected CloudTestBase()
        {
            if (!isMefInitialized)
            {
                AssemblyCompositionInitializer.RegisterAssembly<PersistClassAttribute>();
                isMefInitialized = true;
            }

            AssemblyCompositionInitializer.SatisfyImports(this);
            ((CloudSettings)CloudSettings).DataConnectionString = TableStorageConstants.DevelopmentConnectionString;
        }
        #endregion

        #region Properties
        [Import]
        protected ICloudSettings CloudSettings { get; private set; }
        #endregion
    }
}
