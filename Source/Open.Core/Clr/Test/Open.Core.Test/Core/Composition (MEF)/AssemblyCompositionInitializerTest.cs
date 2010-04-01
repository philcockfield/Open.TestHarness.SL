using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Composite;
using Open.Core.Composition;
using Open.Core.Common.Testing;
namespace Open.Core.Common.Test.Core.MEF
{
    [TestClass]
    public class AssemblyCompositionInitializerTest
    {
        #region Properties
        [Import]
        private IMyClass MyClassInstance { get; set; }

        [Import]
        private IEventBus MyEventBusInstance { get; set; }

        [TestInitialize]
        public void TestSetup()
        {
            MyClassInstance = null;
            MyEventBusInstance = null;
            AssemblyCompositionInitializer.Reset();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldHaveContainerByDefault()
        {
            AssemblyCompositionInitializer.Container.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldImportFromAssembly()
        {
            this.MyClassInstance.ShouldBe(null);
            this.MyEventBusInstance.ShouldBe(null);

            AssemblyCompositionInitializer.SatisfyImports(this, typeof(EventBus).Assembly);
            this.MyClassInstance.ShouldBeInstanceOfType<IMyClass>();
            this.MyEventBusInstance.ShouldBeInstanceOfType<IEventBus>();
        }


        [TestMethod]
        public void ShouldRegisterAssembly()
        {
            AssemblyCompositionInitializer.RegisterAssembly(null); // Doesn't cause error.

            Should.Throw<Exception>(() => AssemblyCompositionInitializer.SatisfyImports(this));

            AssemblyCompositionInitializer.RegisterAssembly(typeof(EventBus).Assembly);
            AssemblyCompositionInitializer.Assemblies.ShouldContain(typeof(EventBus).Assembly);

            AssemblyCompositionInitializer.SatisfyImports(this);
            this.MyClassInstance.ShouldBeInstanceOfType<IMyClass>();
            this.MyEventBusInstance.ShouldBeInstanceOfType<IEventBus>();
        }

        [TestMethod]
        public void ShouldRegisterAssemblyFromType()
        {
            AssemblyCompositionInitializer.Assemblies.Count().ShouldBe(0);
            AssemblyCompositionInitializer.RegisterAssembly<EventBus>();
            AssemblyCompositionInitializer.Assemblies.ShouldContain(typeof(EventBus).Assembly);
        }

        [TestMethod]
        public void ShouldAddReferencedAssembliesOnSatisfyImports()
        {
            AssemblyCompositionInitializer.Assemblies.Count().ShouldBe(0);
            AssemblyCompositionInitializer.SatisfyImports(this, typeof(EventBus).Assembly);

            AssemblyCompositionInitializer.Assemblies.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldDoNothingWhenNullInstancePassed()
        {
            AssemblyCompositionInitializer.SatisfyImports(null);
        }

        [TestMethod]
        public void ShouldReset()
        {
            var container = AssemblyCompositionInitializer.Container;
            AssemblyCompositionInitializer.SatisfyImports(this, typeof(EventBus).Assembly);
            AssemblyCompositionInitializer.Assemblies.Count().ShouldNotBe(0);

            AssemblyCompositionInitializer.Reset();
            AssemblyCompositionInitializer.Assemblies.Count().ShouldBe(0);
            AssemblyCompositionInitializer.Container.ShouldNotBe(container);
        }
        #endregion
    }

    public interface IMyClass{}

    [Export(typeof(IMyClass))]
    public class MyClass : IMyClass
    {
    }  
}
