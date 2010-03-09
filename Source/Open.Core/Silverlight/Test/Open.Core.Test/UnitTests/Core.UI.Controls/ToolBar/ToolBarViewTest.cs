using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [TestClass]
    public class ToolBarViewTest
    {
        #region Head
        [ImportMany]
        public IEnumerable<Lazy<IToolBarView, IIdentifiable>> Views { get; set; }

        [TestInitialize]
        public void TestSetup()
        {
            
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveMultipleToolBarViewImports()
        {
            CompositionInitializer.SatisfyImports(this);
            (Views.Count() > 1).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldSetDataContextOnCreatedView()
        {
            var toolbar = new ToolBarModel();
            var view = toolbar.CreateView();
            view.DataContext.ShouldBe(toolbar);
        }

        [TestMethod]
        public void ShouldGetDefaultView()
        {
            var toolbar = new ToolBarModel();
            toolbar.CreateView().ShouldBeInstanceOfType<ToolBarView>();
        }

        [TestMethod]
        public void ShouldGetOveriddenView()
        {
            var toolbar = new ToolBarModel {ViewImportKey = ToolBarViewOverride.ExportKey};
            toolbar.CreateView().ShouldBeInstanceOfType<ToolBarViewOverride>();
        }

        [TestMethod]
        public void ShouldNotReturnView()
        {
            var toolbar = new ToolBarModel { ViewImportKey = null };
            toolbar.CreateView().ShouldBe(null);
        }
        #endregion
    }
}
