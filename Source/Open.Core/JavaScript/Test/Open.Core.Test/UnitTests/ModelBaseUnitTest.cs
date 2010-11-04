using System;
using Open.Core.Test.Samples;

namespace Open.Core.Test.UnitTests
{
    public class ModelBaseUnitTest
    {
        #region Head

        private SampleModel model;

        public void ClassInitialize() { }
        public void ClassCleanup() { }

        public void TestInitialize()
        {
            model = new SampleModel();
        }

        public void TestCleanup()
        {
            model.Dispose();
        }
        #endregion

        #region Tests
        public void ShouldNotBeDisposed()
        {
            Assert.That(model.IsDisposed).IsFalse();
        }

        public void ShouldBeDisposed()
        {
            model.Dispose();
            Assert.That(model.IsDisposed).IsTrue();
        }

        public void ShouldFireDisposedEventOnce()
        {
            int count = 0;
            model.Disposed += delegate { count++; };

            model.Dispose();
            model.Dispose();

            Assert.That(count).Is(1);
        }
        #endregion
    }


}
