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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls
{
    [TestClass]
    public class ResourcesBaseTest
    {
        [TestMethod]
        public void ShouldHaveResourceDictionary()
        {
            Templates.Instance.Dictionary.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldApplyTemplateFromControlInstance()
        {
            var control = new CollapsingContent{Template = null};
            control.Template.ShouldBe(null);

            Templates.Instance.ApplyTemplate(control);
            control.Template.ShouldNotBe(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ShouldThrowExceptionWhenTemplateNotFound()
        {
            var control = new TitleContainer();
            Templates.Instance.ApplyTemplate(control);
        }

        [TestMethod]
        public void ShouldGetControlTemplate()
        {
            StubTemplates.Instance.GetControlTemplate("myControlTemplate").ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldThrowIfControlTemplateNotFound()
        {
            Should.Throw<NotFoundException>(() => StubTemplates.Instance.GetControlTemplate("Does Not Exist"));
        }

        [TestMethod]
        public void ShouldGetDataTemplate()
        {
            StubTemplates.Instance.GetDataTemplate("myDataTemplate").ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldThrowIfDataTemplateNotFound()
        {
            Should.Throw<NotFoundException>(() => StubTemplates.Instance.GetDataTemplate("Does Not Exist"));
        }

        [TestMethod]
        public void ShouldGetResourceDictionarySingletonFromInferredAssembly()
        {
            var stub = new StubTemplates();
            var dictionary = stub.GetResourceDictionarySingleton(StubTemplates.Path);
            dictionary.ShouldNotBe(null);

            stub.GetResourceDictionarySingleton(StubTemplates.Path).ShouldBe(dictionary);
        }
    }
}
