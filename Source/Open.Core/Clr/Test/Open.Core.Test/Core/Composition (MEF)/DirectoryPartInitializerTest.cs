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
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Composition;

namespace Open.Core.Common.Test.Core.MEF
{
    [TestClass]
    public class DirectoryPartInitializerTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
            DirectoryPartInitializer.Reset();
            StubImport = null;
        }
        #endregion

        #region Properties
        public string Path { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        [Import(typeof(IStub), AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IStub StubImport { get; set; }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldInitialize()
        {
            DirectoryPartInitializer.Initialize(Path);

            DirectoryPartInitializer.IsInitialized.ShouldBe(true);
            DirectoryPartInitializer.Container.ShouldNotBe(null);
            DirectoryPartInitializer.Catalog.Path.ShouldBe(Path);
        }

        [TestMethod]
        public void ShouldInitializeToBaseDirectory()
        {
            DirectoryPartInitializer.Initialize();
            DirectoryPartInitializer.Catalog.Path.ShouldBe(Path);
        }

        [TestMethod]
        public void ShouldInitalizeOnFirstCallToSatisyImports()
        {
            DirectoryPartInitializer.IsInitialized.ShouldBe(false);
            DirectoryPartInitializer.SatisfyImports(this);
            DirectoryPartInitializer.IsInitialized.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotBeInitializedAfterReset()
        {
            DirectoryPartInitializer.Initialize(Path);

            DirectoryPartInitializer.Reset();
            DirectoryPartInitializer.IsInitialized.ShouldBe(false);
            DirectoryPartInitializer.Container.ShouldBe(null);
            DirectoryPartInitializer.Catalog.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldThrowIfPathNull()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryPartInitializer.Initialize(""));
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryPartInitializer.Initialize("   "));
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryPartInitializer.Initialize(null));
        }

        [TestMethod]
        public void ShouldImportParts()
        {
            StubImport.ShouldBe(null);
            DirectoryPartInitializer.SatisfyImports(this);
            StubImport.ShouldBeInstanceOfType<Stub>();
        }

        [TestMethod]
        public void ShouldImportMultipleTimes()
        {
            DirectoryPartInitializer.SatisfyImports(this);
            var import1 = StubImport;

            StubImport = null;

            DirectoryPartInitializer.SatisfyImports(this);
            var import2 = StubImport;

            // ---

            import1.ShouldBeInstanceOfType<Stub>();
            import2.ShouldBeInstanceOfType<Stub>();
            import1.ShouldNotBe(import2);
        }
        #endregion

        #region Stubs
        public interface IStub { }

        [Export(typeof(IStub))]
        public class Stub : IStub
        {
        }
        #endregion
    }
}
