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

using System.Collections.Generic;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class DashboardModelTest : SilverlightTest
    {
        #region Head

        [TestInitialize]
        public void TestInitialize()
        {
//            ViewTestClass.De
        }


        #endregion

        #region Tests
        [TestMethod]
        public void ShouldLoadModulesFromIsolatedStorage()
        {
            var settings = TestHarnessModel.Instance.Settings;
            settings.Clear();

            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.Count.ShouldBe(0);
            testHarness.RecentSelectionsModule.ShouldBe(null);

            settings.LoadedModules = new[]
                                         {
                                             new ModuleSetting("Root.Name1", "File1.xap"), 
                                             new ModuleSetting("Root.Name2", "File2.xap"), 
                                             new ModuleSetting("Root.Name3.dll", "File3.xap"), 
                                         };
            var sampleType = typeof (SampleViewTestClass1);
            settings.RecentSelections = new[]
                                            {
                                                new RecentSelectionSetting(sampleType.FullName, sampleType.Assembly.FullName, "File1.xap"), 
                                            };

            testHarness.Refresh();
            testHarness.Modules.Count.ShouldBe(4);
            testHarness.Modules.FirstOrDefault(item => item.DisplayName == "File1").ShouldNotBe(null);

            testHarness.RecentSelectionsModule.Classes.Count.ShouldBe(1);
            testHarness.RecentSelectionsModule.Classes[0].IsActivated.ShouldBe(false);

            settings.Clear();
            testHarness.Modules.Count.ShouldBe(0);
            testHarness.RecentSelectionsModule.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldDetermineIfAssemblyIsLoaded()
        {
            var testHarness = TestHarnessModel.Instance;
            TestHarnessModel.ResetSingleton();
            testHarness.Modules.RemoveAll();

            var assembly = GetType().Assembly;
            var module = new ViewTestClassesAssemblyModule(new ModuleSetting(assembly.FullName, "File.xap"));
            testHarness.Modules.Add(module);

            // --------

            testHarness.IsLoaded("File.xap").ShouldBe(false);
            module.LoadAssembly(assembly);
            testHarness.IsLoaded("File.xap").ShouldBe(true);

            TestHarnessModel.ResetSingleton();
        }

        [TestMethod]
        public void ShouldGetModuleFromAssemblyName()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            var assembly = GetType().Assembly;

            testHarness.Modules.Add(new ViewTestClassesAssemblyModule(new ModuleSetting(assembly.FullName, "File.xap")));
            testHarness.GetModule("File.xap").ShouldNotBe(null);
            testHarness.GetModule("File").ShouldNotBe(null);
            testHarness.GetModule("Not a name").ShouldBe(null);

            TestHarnessModel.ResetSingleton();
        }

        [TestMethod]
        public void ShouldUpdateCurrentModuleAndClass()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;

            var args = new List<string>();
            testHarness.PropertyChanged += (sender, e) => args.Add(e.PropertyName);

            var class1 = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");
            var class2 = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            var module1 = new ViewTestClassesAssemblyModule(new ModuleSetting(GetType().Assembly.FullName, "File.xap"));
            var module2 = new ViewTestClassesAssemblyModule(new ModuleSetting(GetType().Assembly.FullName, "File.xap"));

            module1.Classes.Add(class1);
            module2.Classes.Add(class2);

            testHarness.Modules.Add(module1);
            testHarness.Modules.Add(module2);

            testHarness.CurrentClass.ShouldBe(null);

            // -------------

            class1.IsCurrent = true;
            testHarness.CurrentClass.ShouldBe(class1);
            args.ShouldContain(TestHarnessModel.PropCurrentClass);
            args.Clear();

            class2.IsCurrent = true;
            testHarness.CurrentClass.ShouldBe(class2);
            args.ShouldContain(TestHarnessModel.PropCurrentClass);
            args.Clear();

            class1.IsCurrent.ShouldBe(false);
            module1.CurrentClass.ShouldBe(null);

            class2.IsCurrent = false;
            testHarness.CurrentClass.ShouldBe(null);
            args.ShouldContain(TestHarnessModel.PropCurrentClass);
            args.Clear();

            TestHarnessModel.ResetSingleton();
        }
        #endregion
    }
}
