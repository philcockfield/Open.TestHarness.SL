using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.Composite;
using Open.Core.Common.Testing;

namespace Open.Core.Test
{
    [ViewTestClass]
    public class TempTest
    {

        [Import]
        public IEventBus EventBus { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize()
        {
            CompositionInitializer.SatisfyImports(this);

            EventBus.Subscribe<MyEvent>(OnEventFired);
        }

        public void OnEventFired(MyEvent e)
        {
            Output.Write("!! --- " + e.Text);    
        }

        [ViewTest]
        public void Fire_Event()
        {
            EventBus.Publish(new MyEvent{Text = "Foo"});

            EventBus.ShouldFire<MyEvent>(() =>
                                             {
                                                 // Do something
                                             });

        }




        public class MyEvent
        {
            public string Text { get; set; }
        }



    }
}
