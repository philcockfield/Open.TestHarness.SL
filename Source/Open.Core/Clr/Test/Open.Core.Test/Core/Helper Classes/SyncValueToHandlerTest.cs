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
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Helper_Classes
{
    [TestClass]
    public class SyncValueToHandlerTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldThrowOnNullConstructorParameters()
        {
            var source = new Source();
            var target = new Target();

            Should.Throw<ArgumentNullException>(() => new SyncValueToHandler<Source>(null, source, target));
            Should.Throw<ArgumentNullException>(() => new SyncValueToHandler<Source>(m => m.Text, null, target));
            Should.Throw<ArgumentNullException>(() => new SyncValueToHandler<Source>(m => m.Text, source, null));
        }

        [TestMethod]
        public void ShouldThrowIfPropertyNotDecoratedWithAttribute()
        {
            var source = new Source();
            Should.Throw<ArgumentException>(() => new SyncValueToHandler<Source>(m => m.Number, source, new Target()));
        }

        [TestMethod]
        public void ShouldThrowIfTargetPropertyNotExist()
        {
            var source = new Source();
            Should.Throw<ArgumentException>(() => new SyncValueToHandler<Source>(m => m.NoTarget, source, new Target()));
        }

        [TestMethod]
        public void ShouldStoreProperties()
        {
            var source = new Source();
            var target = new Target();
            var sourceProperty = source.GetType().GetProperty("Text");
            var targetProperty = target.GetType().GetProperty("MyText");

            var handler = new SyncValueToHandler<Source>(m => m.Text, source, target);

            handler.SourceProperty.ShouldBe(sourceProperty);
            handler.TargetProperty.ShouldBe(targetProperty);
            handler.TargetInstance.ShouldBe(target);
            handler.SourceInstance.ShouldBe(source);
        }

        [TestMethod]
        public void ShouldChangeTargetPropertyWhenSourceChanged()
        {
            var source = new Source();
            var target = new Target();
            var handler = new SyncValueToHandler<Source>(m => m.Text, source, target);
            
            source.Text.ShouldBe(null);
            target.MyText.ShouldBe(null);

            source.Text = "Hello";
            target.MyText.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldReportDisposedWhenSourceIsDisposed()
        {
            var source = new Source();
            var target = new Target();

            var handler = new SyncValueToHandler<Source>(m => m.Text, source, target);
            handler.IsSourceDisposed.ShouldBe(false);

            source.Dispose();
            handler.IsSourceDisposed.ShouldBe(true);
            handler.IsTargetDisposed.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldReportDisposedWhenTargetIsDisposed()
        {
            var source = new Source();
            var target = new Target();

            var handler = new SyncValueToHandler<Source>(m => m.Text, source, target);
            handler.IsSourceDisposed.ShouldBe(false);

            target.Dispose();
            handler.IsSourceDisposed.ShouldBe(false);
            handler.IsTargetDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotUpdateWhenDisposed()
        {
            var source = new Source();
            var target = new Target();

            var handler = new SyncValueToHandler<Source>(m => m.Text, source, target);
            handler.IsSourceDisposed.ShouldBe(false);

            // ---

            source.Text = "Harry";
            target.MyText.ShouldBe("Harry");

            // ---

            handler.Dispose();
            handler.IsDisposed.ShouldBe(true);

            // ---

            source.Text = "Bob";
            target.MyText.ShouldBe("Harry");
        }
        #endregion

        #region Stubs
        public class Source : ModelBase
        {
            private string text;

            [SyncValueTo("MyText")]
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged<Source>(m => m.Text); }
            }

            public int Number { get; set; }

            [SyncValueTo("NotOnTarget")]
            public int NoTarget { get; set; }
        }

        public class Target : DependencyObject, INotifyDisposed
        {
            #region Head
            public const string PropMyText = "MyText";
            #endregion

            #region Dispose | Finalize
            ~Target()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool isDisposing)
            {
                // Setup initial conditions.
                if (IsDisposed) return;

                // Perform disposal or managed resources.
                if (isDisposing)
                {
                    // Dispose of managed resources.
                }

                // Finish up.
                IsDisposed = true;
                OnDisposed();
            }

            /// <summary>Gets whether the object has been disposed.</summary>
            public bool IsDisposed { get; private set; }

            /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
            public event EventHandler Disposed;

            private void OnDisposed()
            {
                if (Disposed != null) Disposed(this, new EventArgs());
            }
            #endregion

            #region Properties
            public string MyText
            {
                get { return (string)(GetValue(MyTextProperty)); }
                set { SetValue(MyTextProperty, value); }
            }
            public static readonly DependencyProperty MyTextProperty =
                DependencyProperty.Register(
                    PropMyText,
                    typeof(string),
                    typeof(Target),
                    new PropertyMetadata(null));
            #endregion
        }
        #endregion
    }
}
