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

        protected CloudTestBase()
        {
            if (!isMefInitialized)
            {
                AssemblyCompositionInitializer.RegisterAssembly<TableEntityBase>();
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
