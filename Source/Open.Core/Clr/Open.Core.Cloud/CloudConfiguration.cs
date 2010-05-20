//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Open.Core.Cloud
{
    public static class CloudConfiguration
    {
        /// <summary>Initializes the cloud storage account.</summary>
        /// <remarks>
        ///     Sourced from: http://blogs.msdn.com/jnak/archive/2010/01/06/walkthrough-windows-azure-table-storage-nov-2009-and-later.aspx
        /// </remarks>
        public static void InitializeCloudStorageAccount()
        {
            // This code sets up a handler to update CloudStorageAccount instances when their corresponding
            // configuration settings change in the service configuration file.
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
                                {
                                    // Provide the configSetter with the initial value
                                    configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                                    RoleEnvironment.Changed += (sender, arg) =>
                                                                {
                                                                    if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                                                                        .Any((change) => (change.ConfigurationSettingName == configName)))
                                                                    {
                                                                        // The corresponding configuration setting has changed, propagate the value
                                                                        if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)))
                                                                        {
                                                                            // In this case, the change to the storage account credentials in the
                                                                            // service configuration is significant enough that the role needs to be
                                                                            // recycled in order to use the latest settings. (for example, the 
                                                                            // endpoint has changed)
                                                                            RoleEnvironment.RequestRecycle();
                                                                        }
                                                                    }
                                                                };
                                });
        }
    }
}
