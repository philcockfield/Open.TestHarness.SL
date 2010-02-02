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
using System.ComponentModel;
using System.IO;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Network;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.Network
{
    [TestClass]
    public class LoaderBaseTest : SilverlightTest
    {
        #region Tests
        [TestMethod]
        public void ShouldBeUnloadedAtCreation()
        {
            var loader = new SampleLoader();
            loader.State.ShouldBe(LoaderState.Unloaded);
        }

        [TestMethod]
        public void ShouldTakeTestableWebClientInBase()
        {
            var webClient = new SampleWebClient();
            var loader = new SampleLoader(webClient);
            loader.WebClient.ShouldBe(webClient);

            var defaultLoader = new SampleLoader();
            defaultLoader.WebClient.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldChangeStateToLoadingWhenLoading()
        {
            var loader = new SampleLoader(new SampleWebClient{Async = true});
            loader.State.ShouldBe(LoaderState.Unloaded);
            loader.Load();
            loader.State.ShouldBe(LoaderState.Loading);
        }

        [TestMethod]
        public void ShouldInvokePreload()
        {
            var loader = new SampleLoader(new SampleWebClient { Async = true });
            loader.Load();
            loader.PreLoadInvoked.ShouldBe(true);
            loader.State.ShouldBe(LoaderState.Loading);
        }

        [TestMethod]
        public void ShouldCancelPreload()
        {
            var loader = new SampleLoader(new SampleWebClient()){DoCancelPreload = true};
            loader.Load();
            loader.PreLoadInvoked.ShouldBe(false);
            loader.State.ShouldBe(LoaderState.Unloaded);
        }

        [TestMethod]
        public void ShouldInvokeLoadCallbackAfterLoad()
        {
            var loader = new SampleLoader(new SampleWebClient());

            PropertyChangedEventArgs argsProp = null;
            loader.PropertyChanged += (sender, e) => argsProp = e;

            EventArgs args = null;
            loader.Loaded += (sender, e) => args = e;

            loader.Load();
            loader.LoadCallbackInvoked.ShouldBe(true);
            loader.State.ShouldBe(LoaderState.Loaded);
            loader.Error.ShouldBe(null);

            args.ShouldNotBe(null);
            argsProp.PropertyName.ShouldBe(LoaderBase.PropLoaderState);
        }

        [TestMethod]
        public void ShouldInvokeCallbackFromLoadMethod()
        {
            var loader = new SampleLoader(new SampleWebClient());

            var callbackInvoked = false;
            loader.Load(() => callbackInvoked = true);

            callbackInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldReportLoadError()
        {
            var error = new Exception("Dummy error");
            var loader = new SampleLoader(new SampleWebClient { Error = error });

            PropertyChangedEventArgs argsProp = null;
            loader.PropertyChanged += (sender, e) => argsProp = e;

            EventArgs argsLoadFailed = null;
            loader.LoadFailed += (sender, e) => argsLoadFailed = e;

            loader.Load();
            loader.State.ShouldBe(LoaderState.LoadError);
            loader.Error.ShouldBe(error);
            
            argsProp.PropertyName.ShouldBe(LoaderBase.PropLoaderState);
            argsLoadFailed.ShouldNotBe(null);
        }

        [TestMethod][Asynchronous]
        public void ShouldReportLoadTime()
        {
            var loader = new SampleLoader(new SampleWebClient { Async = true, AsyncDelay = 0.2});
            loader.Load(() =>
                            {
                                (loader.LoadTime.TotalSeconds > 0.1).ShouldBe(true);
                                EnqueueTestComplete();
                            });
        }
        #endregion

        #region Sample Data
        private class SampleLoader : LoaderBase
        {
            #region Head
            public SampleLoader(){}
            public SampleLoader(TestableWebClient webClient) : base(webClient){}
            #endregion

            #region Properties
            public bool LoadCallbackInvoked { get; private set; }
            public bool PreLoadInvoked { get; private set; }
            public bool DoCancelPreload { get; set; }
            #endregion

            #region Methods - Override
            protected override bool OnPreload()
            {
                if (DoCancelPreload) return false;
                PreLoadInvoked = true;
                return true;
            }

            protected override void OnLoadCallback(TestableOpenReadCompletedEventArgs e)
            {
                LoadCallbackInvoked = true;
            }

            protected override Uri GetUri()
            {
                return new Uri("MyFileName.xap", UriKind.Relative);
            }
            #endregion
        }
        #endregion
    }
}
