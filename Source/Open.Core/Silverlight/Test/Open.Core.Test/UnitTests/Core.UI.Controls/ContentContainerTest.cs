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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Test;

using T = Open.Core.UI.Controls.ContentContainerViewModel;

namespace Open.Core.Test.UnitTests.Core.UI.Controls
{
    [TestClass]
    public class ContentContainerTest
    {
        #region Head
        private ContentContainerViewModel viewModel;
        private ContentContainer view;

        [TestInitialize]
        public void TestSetup()
        {
            viewModel = new ContentContainerViewModel();
            view = viewModel.CreateView() as ContentContainer;
        }
        #endregion
        
        #region Tests
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IContentContainer ViewModelImport { get; set; }

        [TestMethod]
        public void ShouldImport()
        {
            CompositionInitializer.SatisfyImports(this);
            ViewModelImport.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldCreateViewModelFromView()
        {
            view = new ContentContainer();
            view.ViewModel.ShouldBe(null);

            var model = view.CreateViewModel();
            model.ShouldBeInstanceOfType<IContentContainer>();
            view.ViewModel.ShouldBe(model);
        }

        [TestMethod]
        public void ShouldFirePropertyChanged()
        {
            viewModel.ShouldFirePropertyChanged<T>(
                () => viewModel.Content = "Hello",
                m => m.Content, 
                m => m.RenderTemplate);
            viewModel.ShouldFirePropertyChanged<T>(
                () => viewModel.ContentTemplate = SampleTemplates.Placeholder1, 
                m => m.ContentTemplate,
                m => m.RenderTemplate);
        }

        [TestMethod]
        public void ShouldHaveDefaultTemplate()
        {
            viewModel.DefaultTemplate.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldReturnContentTemplateAsRenderTemplate()
        {
            var sampleTemplate = SampleTemplates.Placeholder1;

            viewModel.ContentTemplate = sampleTemplate;
            viewModel.RenderTemplate.ShouldBe(sampleTemplate);
        }

        [TestMethod]
        public void ShouldReturnDefaultAsRenderTemplate()
        {
            viewModel.Content = "Hello";
            viewModel.RenderTemplate.ShouldBe(viewModel.DefaultTemplate);
        }


        [TestMethod]
        public void ShouldOverrideContentTemplate()
        {
            viewModel.ContentTemplate = SampleTemplates.Placeholder1;
            viewModel.Content = "Hello";
            viewModel.RenderTemplate.ShouldBe(viewModel.DefaultTemplate);

            viewModel.Content = new Border();
            viewModel.RenderTemplate.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReturnNullFromRenderTemplateWhenDisposed()
        {
            viewModel.ContentTemplate = SampleTemplates.Placeholder1;
            viewModel.RenderTemplate.ShouldNotBe(null);
            viewModel.Dispose();
            viewModel.RenderTemplate.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFireChangeEvents()
        {
            var contentChangedCount = 0;
            var contentTemplateChangedCount = 0;
            var modelChangedCount = 0;

            viewModel = new ContentContainerViewModel();
            viewModel.ContentChanged += delegate { contentChangedCount++; };
            viewModel.ContentTemplateChanged += delegate { contentTemplateChangedCount++; };
            viewModel.ModelChanged += delegate { modelChangedCount++; };

            var content = new Border();
            var template = new DataTemplate();
            var model = "My Model";

            viewModel.Content = content;
            viewModel.Content = content;
            viewModel.Content = null;
            viewModel.Content = null;
            contentChangedCount.ShouldBe(2);

            viewModel.ContentTemplate = template;
            viewModel.ContentTemplate = template;
            viewModel.ContentTemplate = null;
            viewModel.ContentTemplate = null;
            contentTemplateChangedCount.ShouldBe(2);

            viewModel.Model = model;
            viewModel.Model = model;
            viewModel.Model = null;
            viewModel.Model = null;
            contentTemplateChangedCount.ShouldBe(2);
        }
        #endregion
    }
}
