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

using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class DashboardSettingsTest : SilverlightTest
    {
        #region Tests
        [TestMethod]
        public void ShouldSaveAndRetrieveLoadedModules()
        {
            var settings = TestHarnessModel.Instance.Settings;

            settings.Clear();
            settings.LoadedModules.Length.ShouldBe(0);

            settings.LoadedModules = new[] 
                                { 
                                    new ModuleSetting("Root.dll", "MyModule.xap"), 
                                    new ModuleSetting("Test.EntryPoint.dll", "Tests.xap"), 
                                    new ModuleSetting("Test.View.dll", "Test.View.xap"), 
                                }; 
            settings.LoadedModules.Length.ShouldBe(3);

            settings.Save();
            settings.LoadedModules.Length.ShouldBe(3);
            settings.LoadedModules[0].AssemblyName.ShouldBe("Root.dll");
            settings.LoadedModules[0].XapFileName.ShouldBe("MyModule");

            var newSettings = new TestHarnessSettings(TestHarnessModel.Instance);
            newSettings.LoadedModules[0].AssemblyName.ShouldBe("Root.dll");
            newSettings.LoadedModules[0].XapFileName.ShouldBe("MyModule");

            settings.LoadedModules = new ModuleSetting[] {};
            settings.LoadedModules.Length.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldSaveAndRetrieve_RecentSelections()
        {
            var settings = TestHarnessModel.Instance.Settings;

            settings.Clear();
            settings.RecentSelections.Length.ShouldBe(0);

            settings.RecentSelections = new[]
                                            {
                                                new RecentSelectionSetting("ClassName1", "Core.Common", "File1.xap"), 
                                                new RecentSelectionSetting("ClassName2", "Core.UnCommon", "File2.xap"), 
                                            };
            settings.RecentSelections.Length.ShouldBe(2);

            settings.Save();
            settings.RecentSelections.Length.ShouldBe(2);

            settings.RecentSelections = new RecentSelectionSetting[] { };
            settings.RecentSelections.Length.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldClear()
        {
            var settings = TestHarnessModel.Instance.Settings;
            settings.LoadedModules = new[]
                                         {
                                             new ModuleSetting("Root.Name1", "File1.xap"), 
                                             new ModuleSetting("Root.Name2", "File2.xap"), 
                                             new ModuleSetting("Root.Name3.dll", "File3.xap"), 
                                         };
            settings.RecentSelections = new[]
                                        {
                                            new RecentSelectionSetting("ClassName1", "Core.Common", "File1.xap"), 
                                            new RecentSelectionSetting("ClassName2", "Core.UnCommon", "File2.xap"), 
                                        };

            EventArgs args = null;
            settings.Cleared += (sender, e) => args = e;

            settings.Clear();
            args.ShouldNotBe(null);

            settings.Clear();
            settings.Clear();
            settings.Save();
            settings.LoadedModules.Length.ShouldBe(0);
            settings.RecentSelections.Length.ShouldBe(0);
        }
        #endregion
    }
}
