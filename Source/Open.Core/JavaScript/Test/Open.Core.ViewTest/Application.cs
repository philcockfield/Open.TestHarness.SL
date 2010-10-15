using System.Collections;
using Open.Core.Test.UnitTests;
using Open.Core.Test.ViewTests.Controls;
using Open.Core.Test.ViewTests.Controls.Buttons;
using Open.Core.Test.ViewTests.Controls.HtmlPrimitive;
using Open.Core.Test.ViewTests.Controls.Input;
using Open.Core.Test.ViewTests.Controls.Panels;
using Open.Core.Test.ViewTests.Core;
using Open.Core.Test.ViewTests.Lists;
using Open.Testing;

namespace Open.Core.Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            // Unit Tests.
            TestHarness.RegisterClass(typeof(DiContainerTest));
            TestHarness.RegisterClass(typeof(ModelBaseUnitTest));
            TestHarness.RegisterClass(typeof(ViewBaseUnitTest));
            TestHarness.RegisterClass(typeof(CollectionHelperUnitTest));
            TestHarness.RegisterClass(typeof(PropertyBindingUnitTest));

            // View Tests
            TestHarness.RegisterClass(typeof(CollapsePanelTest));
            TestHarness.RegisterClass(typeof(PartTest));
            TestHarness.RegisterClass(typeof(IconTest));
            TestHarness.RegisterClass(typeof(TemplateTest));
            TestHarness.RegisterClass(typeof(ViewBaseTest));
            TestHarness.RegisterClass(typeof(ListTreeViewTest));
            TestHarness.RegisterClass(typeof(ListViewTest));
            TestHarness.RegisterClass(typeof(ListItemViewTest));
            TestHarness.RegisterClass(typeof(LogTest));
            TestHarness.RegisterClass(typeof(HtmlListTest));
            TestHarness.RegisterClass(typeof(SpacingTest));
            TestHarness.RegisterClass(typeof(ButtonTest));
            TestHarness.RegisterClass(typeof(Button_LibraryTest));
            TestHarness.RegisterClass(typeof(IconTextButtonTest));
            TestHarness.RegisterClass(typeof(ImageButtonTest));
            TestHarness.RegisterClass(typeof(TextboxTest));
        }
    }
}
