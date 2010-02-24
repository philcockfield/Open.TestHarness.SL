using Open.Core.Common;
using Open.Core.Common.Testing;
using Sample.ClassLibrary.Views;

namespace Sample.Test
{
    [ViewTestClass]
    public class MyControlViewTest
    {
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(MyControl control)
        {
            control.Width = 300;
            control.Height = 200;
            control.ViewModel = new MyControlViewModel();
        }

        [ViewTest]
        public void Change_Title(MyControl control)
        {
            // Change the view-model's title.
            control.ViewModel.Title = RandomData.LoremIpsum(2, 4);

            // Write some state information do the Output log.
            Output.WriteTitle("Title Changed");
            Output.WriteProperties(control.ViewModel);
            Output.Break();
        }
    }
}
