// DiContainerTest.cs
//

using System;
using System.Collections;

namespace Open.Core.Test.UnitTests
{
    public class DiContainerTest
    {
        #region Head
        private DiContainer container;

        public void TestInitialize()
        {
            container = new DiContainer();
        }

        public void TestCleanup()
        {
            container.Dispose();
        }
        #endregion

        #region Methods
        public void ShouldNotHaveSingleton()
        {
            Assert.That(container.GetSingleton(typeof(IMyInterface))).IsNull();
        }

        public void ShouldHaveSingletonAfterRegistering()
        {
            MyClass instance = new MyClass();
            container.RegisterSingleton(typeof(IMyInterface), instance);

            object retreived = container.GetSingleton(typeof (IMyInterface));
            Assert.That(retreived).Is(instance);
        }

        public void ShouldReplaceSingleton()
        {
            MyClass instance1 = new MyClass();
            MyClass instance2 = new MyClass();

            container.RegisterSingleton(typeof(IMyInterface), instance1);
            container.RegisterSingleton(typeof(IMyInterface), instance2);

            object retreived = container.GetSingleton(typeof(IMyInterface));
            Assert.That(retreived).IsNot(instance1);
            Assert.That(retreived).Is(instance2);
        }

        public void ShouldReturnNullWhenNoKeySpecified()
        {
            Assert.That(container.GetSingleton(null)).IsNull();
        }

        public void ShouldNotContainSingleton()
        {
            Assert.That(container.ContainsSingleton(typeof(IMyInterface))).IsFalse();
        }

        public void ShouldContainSingleton()
        {
            container.RegisterSingleton(typeof(IMyInterface), new MyClass());
            Assert.That(container.ContainsSingleton(typeof(IMyInterface))).IsTrue();
        }

        public void ShouldUnregisterSingleton()
        {
            MyClass instance1 = new MyClass();
            container.RegisterSingleton(typeof(IMyInterface), instance1);
            container.RegisterSingleton(typeof(IMyInterface), instance1);
            Assert.That(container.GetSingleton(typeof(IMyInterface))).Is(instance1);

            container.UnregisterSingleton(typeof(IMyInterface));
            Assert.That(container.GetSingleton(typeof(IMyInterface))).Is(null);
        }

        public void ShouldCreateSingletonInstance()
        {
            Assert.That(container.GetSingleton(typeof(IMyInterface))).IsNull();

            MyClass instance1 = new MyClass();
            object retrieved = container.GetOrCreateSingleton(typeof(IMyInterface), delegate { return instance1; });

            Assert.That(retrieved).Is(instance1);
            Assert.That(container.GetSingleton(typeof(IMyInterface))).Is(instance1);
        }
        #endregion
    }

    public class MyClass : IMyInterface { }
    public class IMyInterface { }

}
