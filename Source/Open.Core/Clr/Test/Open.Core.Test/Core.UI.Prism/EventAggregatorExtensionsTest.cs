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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using CAL = Microsoft.Practices.Composite.Events;

namespace Open.Core.Common.Test.Core.UI.Prism
{
    [TestClass]
    public class EventAggregatorExtensionsTest
    {
        [TestMethod]
        public void ShouldGetEventAggregatorFromContainer()
        {
            var container = new UnityContainer();
            var ea = new EventAggregator(container);

            container.RegisterInstance(typeof(IEventAggregator), ea, new ContainerControlledLifetimeManager());
            container.GetEventAggregator().ShouldBe(ea);
        }

        [TestMethod]
        public void ShouldAutoCreateEventAggregator()
        {
            var container = new UnityContainer();
            container.TryResolve<IEventAggregator>().ShouldBe(null);

            var ea = container.GetEventAggregator(true);
            ea.ShouldBeInstanceOfType<EventAggregator>();

            container.TryResolve<IEventAggregator>().ShouldBe(ea);
            container.TryResolve<IEventAggregator>().ShouldBe(ea);
            container.TryResolve<IEventAggregator>().ShouldBe(ea);
        }

        [TestMethod]
        public void ShouldAutoCreateEventAggregatorFromParameterlessMethod()
        {
            var container = new UnityContainer();
            container.TryResolve<IEventAggregator>().ShouldBe(null);

            var ea = container.GetEventAggregator(true);
            ea.ShouldBeInstanceOfType<EventAggregator>();
        }

        [TestMethod]
        public void ShouldThrowWhenEventAggregatorIsNotInContainer()
        {
            var container = new UnityContainer();
            Should.Throw<NotFoundException>(() => container.GetEventAggregator(false));
        }

        [TestMethod]
        public void ShouldNotThrowWhenEventAggregatorIsNotInContainer()
        {
            var container = new UnityContainer();
            container.GetEventAggregator(true);

            container = new UnityContainer();
            container.GetEventAggregator();
        }
    }
}
