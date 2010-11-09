using System;
using System.Collections;
using jQueryApi;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls
{
    public class LogViewTest
    {
        #region Head
        private bool isInitialized = false;
        private ILog log;
        private ILogView view;

        private jQueryObject container;

        public void ClassInitialize()
        {
            TestHarness.DisplayMode = ControlDisplayMode.FillWithMargin;
            container = TestHarness.AddElement();

            log = new LogWriter();

            jQuery.Get("/Open.Core/Controls/Log", delegate(object data)
                                    {
                                        Log.Info(data.ToString().HtmlEncode());
                                        container.Append(data.ToString());

                                        view = new LogView(container.Children().First());
                                        log.View = view;
                                    });
        }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Methods
        public void Toggle__AutoScroll()
        {
            view.AutoScroll = !view.AutoScroll;
            Log.Info("AutoScroll: " + view.AutoScroll);
        }

        public void Log__Info() { log.Info("Foo"); }
        public void Log__Warning() { log.Warning("Warning"); }
        public void Log__Error() { log.Error("Error"); }
        public void Log__Icon() { log.WriteIcon("SilkLightning", Icons.SilkLightning, null); }
        public void Log__LineBreak() { log.LineBreak(); }
        public void Log__NewSection() { log.NewSection(); }

        public void Write_20_Entries()
        {
            for (int i = 1; i <= 20; i++)
            {
                log.Info("Item " + i);
            }
        }

        public void Clear()
        {
            log.Clear();
        }
        #endregion
    }
}
