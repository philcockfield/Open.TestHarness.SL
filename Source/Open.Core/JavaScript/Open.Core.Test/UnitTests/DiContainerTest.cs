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
            Should.BeNull(container.GetSingleton(typeof(IMyInterface)));
        }

        public void ShouldHaveSingletonAfterRegistering()
        {
            MyClass instance = new MyClass();
            container.RegisterSingleton(typeof(IMyInterface), instance);

            object retreived = container.GetSingleton(typeof (IMyInterface));
            Should.Equal(retreived, instance);
        }

        public void ShouldReplaceSingleton()
        {
            MyClass instance1 = new MyClass();
            MyClass instance2 = new MyClass();

            container.RegisterSingleton(typeof(IMyInterface), instance1);
            container.RegisterSingleton(typeof(IMyInterface), instance2);

            object retreived = container.GetSingleton(typeof(IMyInterface));
            Should.NotEqual(retreived, instance1);
            Should.Equal(retreived, instance2);
        }

        public void ShouldReturnNullWhenNoKeySpecified()
        {
            Should.BeNull(container.GetSingleton(null));
        }

        public void ShouldNotContainSingleton()
        {
            Should.BeFalse(container.ContainsSingleton(typeof(IMyInterface)));
        }

        public void ShouldContainSingleton()
        {
            container.RegisterSingleton(typeof(IMyInterface), new MyClass());
            Should.BeTrue(container.ContainsSingleton(typeof(IMyInterface)));
        }

        public void ShouldUnregisterSingleton()
        {
            MyClass instance1 = new MyClass();
            container.RegisterSingleton(typeof(IMyInterface), instance1);
            container.RegisterSingleton(typeof(IMyInterface), instance1);
            Should.Equal(container.GetSingleton(typeof (IMyInterface)), instance1);

            container.UnregisterSingleton(typeof(IMyInterface));
            Should.Equal(container.GetSingleton(typeof(IMyInterface)), null);
        }

        public void ShouldCreateSingletonInstance()
        {
            Should.BeNull(container.GetSingleton(typeof(IMyInterface)));

            MyClass instance1 = new MyClass();
            object retrieved = container.GetOrCreateSingleton(typeof(IMyInterface), delegate { return instance1; });

            Should.Equal(retrieved, instance1);
            Should.Equal(container.GetSingleton(typeof(IMyInterface)), instance1);
        }
        #endregion
    }

    public class MyClass : IMyInterface { }
    public class IMyInterface { }

}
