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
    public class DirectoryCompositionInitializerTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
            DirectoryCompositionInitializer.Reset();
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
            DirectoryCompositionInitializer.Initialize(Path);

            DirectoryCompositionInitializer.IsInitialized.ShouldBe(true);
            DirectoryCompositionInitializer.Container.ShouldNotBe(null);
            DirectoryCompositionInitializer.Catalog.Path.ShouldBe(Path);
        }

        [TestMethod]
        public void ShouldInitializeToBaseDirectory()
        {
            DirectoryCompositionInitializer.Initialize();
            DirectoryCompositionInitializer.Catalog.Path.ShouldBe(Path);
        }

        [TestMethod]
        public void ShouldInitalizeOnFirstCallToSatisyImports()
        {
            DirectoryCompositionInitializer.IsInitialized.ShouldBe(false);
            DirectoryCompositionInitializer.SatisfyImports(this);
            DirectoryCompositionInitializer.IsInitialized.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotBeInitializedAfterReset()
        {
            DirectoryCompositionInitializer.Initialize(Path);

            DirectoryCompositionInitializer.Reset();
            DirectoryCompositionInitializer.IsInitialized.ShouldBe(false);
            DirectoryCompositionInitializer.Container.ShouldBe(null);
            DirectoryCompositionInitializer.Catalog.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldThrowIfPathNull()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryCompositionInitializer.Initialize(""));
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryCompositionInitializer.Initialize("   "));
            Should.Throw<ArgumentOutOfRangeException>(() => DirectoryCompositionInitializer.Initialize(null));
        }

        [TestMethod]
        public void ShouldImportParts()
        {
            StubImport.ShouldBe(null);
            DirectoryCompositionInitializer.SatisfyImports(this);
            StubImport.ShouldBeInstanceOfType<Stub>();
        }

        [TestMethod]
        public void ShouldImportMultipleTimes()
        {
            DirectoryCompositionInitializer.SatisfyImports(this);
            var import1 = StubImport;

            StubImport = null;

            DirectoryCompositionInitializer.SatisfyImports(this);
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
