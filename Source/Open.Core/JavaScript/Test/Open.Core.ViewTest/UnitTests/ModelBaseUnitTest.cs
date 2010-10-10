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
            Should.BeFalse(model.IsDisposed);
        }

        public void ShouldBeDisposed()
        {
            model.Dispose();
            Should.BeTrue(model.IsDisposed);
        }

        public void ShouldFireDisposedEventOnce()
        {
            int count = 0;
            model.Disposed += delegate { count++; };

            model.Dispose();
            model.Dispose();

            Should.Equal(count, 1);
        }
        #endregion
    }


}
