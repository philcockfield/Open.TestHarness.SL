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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Unity
{
    [TestClass]
    public class UnityExtensionsTest
    {
        #region Tests
        [TestMethod]
        public void ShouldReturnNullWhenResolvingToItemNotInContainer()
        {
            var container = new UnityContainer();
            container.TryResolve<IStub2>().ShouldBe(null);
            container.TryResolve<IStub2>((Action)null).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldResolveInstanceAddedToContainer()
        {
            var container = new UnityContainer();

            var stub2 = new Stub2();
            container.RegisterInstance(typeof(IStub2), stub2, new ContainerControlledLifetimeManager());

            var result = container.TryResolve<IStub2>();
            result.ShouldBe(stub2);
        }

        [TestMethod]
        public void ShouldInvokeActionWhenFailedToResolveItem()
        {
            var container = new UnityContainer();

            var processCount = 0;
            Action action = () => { processCount++; };

            var stub2 = new Stub2();
            container.RegisterInstance(typeof(IStub2), stub2, new ContainerControlledLifetimeManager());

            // ---

            container.TryResolve<IStub1>(action).ShouldBe(null);
            processCount.ShouldBe(1);

            processCount = 0;
            container.TryResolve<IStub2>(action).ShouldNotBe(null);
            processCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldResolveNamedInstance()
        {
            var container = new UnityContainer();

            var stub2 = new Stub2();
            container.RegisterInstance(typeof(IStub2), "name", stub2, new ContainerControlledLifetimeManager());

            // ---

            container.TryResolve<IStub2>().ShouldBe(null);
            container.TryResolve<IStub2>("name").ShouldBe(stub2);
            container.TryResolve<IStub2>("name", () => { }).ShouldBe(stub2);

            // ---

            var processCount = 0;
            Action action = () => { processCount++; };

            container.TryResolve<IStub1>("name", action).ShouldBe(null);
            processCount.ShouldBe(1);

            processCount = 0;
            container.TryResolve<IStub2>("wrong-name", action).ShouldBe(null);
            processCount.ShouldBe(1);

            processCount = 0;
            container.TryResolve<IStub2>("name").ShouldBe(stub2);
            processCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldRegisterSingleton()
        {
            var container = new UnityContainer();
            container.TryResolve<IStub1>().ShouldBe(null);

            var instance = new Stub1();
            container.RegisterSingleton<IStub1>(instance);

            container.TryResolve<IStub1>().ShouldBe(instance);
            container.TryResolve<IStub1>().ShouldBe(instance);
        }

        
        [TestMethod]
        public void ShouldRegisterNamedSingleton()
        {
            var container = new UnityContainer();
            container.TryResolve<IStub1>("name").ShouldBe(null);

            var instance = new Stub1();
            container.RegisterSingleton<IStub1>(instance, "name");

            container.TryResolve<IStub1>("name").ShouldBe(instance);
            container.TryResolve<IStub1>("name").ShouldBe(instance);
            container.TryResolve<IStub1>().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldCreateSingletonWhenTryingToResolve()
        {
            var container = new UnityContainer();
            
            var instance = container.TryResolveSingleton<IStub1>(() => new Stub1());
            instance.ShouldBeInstanceOfType<Stub1>();
            container.TryResolve<IStub1>().ShouldBe(instance);
            container.TryResolveSingleton<IStub1>(() => new Stub1()).ShouldBe(instance);
        }

        [TestMethod]
        public void ShouldCreateNamedSingletonWhenTryingToResolve()
        {
            var container = new UnityContainer();
            var instance = container.TryResolveSingleton<IStub1>("name", () => new Stub1());

            instance.ShouldBeInstanceOfType<Stub1>();
            container.TryResolve<IStub1>("name").ShouldBe(instance);
            container.TryResolve<IStub1>("name").ShouldBe(instance);
            container.TryResolve<IStub1>().ShouldBe(null);
            
            container.TryResolveSingleton<IStub1>("name", () => new Stub1()).ShouldBe(instance);
            container.TryResolveSingleton<IStub1>("name", () => new Stub1()).ShouldBe(instance);
        }
        #endregion

        #region Sample Data
        public class Stub1 : IStub1 { }
        public class Stub2 : IStub2 { }

        public interface IStub1 { }
        public interface IStub2 { }
        #endregion
    }
}
