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
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Test;

namespace Open.Core.Test.ViewTests.Core.Controls.Containers
{
    [ViewTestClass]
    public class ContentContainerViewTest
    {
        #region Head
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IContentContainer ViewModel { get; set; }

        private readonly Placeholder sampleControl = new Placeholder {Name = "Sample.Placeholder", Text = "Content = Placeholder", Color = Colors.Green};
        private ContentContainer contentContainer;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ContentControl control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 400;
            control.Height = 300;
            control.StretchContent();

            contentContainer = ViewModel.CreateView() as ContentContainer;
            control.Content = contentContainer;
//            control.ViewModel = ViewModel;


        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_Content__Placeholder(ContentControl control)
        {
            ViewModel.Content = sampleControl;
            Write();
        }

        [ViewTest]
        public void Set_Content__String(ContentControl control)
        {
            ViewModel.Model = null;
            ViewModel.Content = "My String";
            Write();
        }

        [ViewTest]
        public void Change_Content_String(ContentControl control)
        {
            ViewModel.Content = RandomData.LoremIpsum(3, 5);
            Write();
        }

        [ViewTest]
        public void Set_Content__Null(ContentControl control)
        {
            ViewModel.Content = null;
            Write();
        }

        [ViewTest]
        public void Set_ContentTemplate__With_Model(ContentControl control)
        {
            ViewModel.Model = new Mock { Text = "Model Text" };
            ViewModel.ContentTemplate = SampleTemplates.PlaceholderText;
            Write();
        }

        [ViewTest]
        public void Set_ContentTemplate__No_Model(ContentControl control)
        {
            ViewModel.Model = null;
            ViewModel.ContentTemplate = SampleTemplates.PlaceholderText;
            Write();
        }

        [ViewTest]
        public void Set_ContentTemplate__Null(ContentControl control)
        {
            ViewModel.Model = null;
            ViewModel.ContentTemplate = null;
            Write();
        }

        [ViewTest]
        public void Change_Model_Text(ContentControl control)
        {
            var model = ViewModel.Model as Mock;
            if (model == null) return;
            model.Text = RandomData.LoremIpsum(3, 5);
        }

        [ViewTest]
        public void Write(ContentControl control)
        {
            Write();
        }

        private void Write( )
        {
            Output.WriteProperties(ViewModel);
            Output.Break();
        }
        #endregion

        public class Mock : ModelBase
        {
            public string Text
            {
                get { return GetPropertyValue<Mock, string>(m => m.Text); }
                set { SetPropertyValue<Mock, string>(m => m.Text, value); }
            }
        }
    }
}
