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
using Open.Core.UI;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.Primitives
{
    [Tag("b")]
    [TestClass]
    public class BackgroundModelTest
    {
        #region Head

        [Import]
        public ExportFactory<IBackground> BackgroundFactory { get; set; }

        private BackgroundModel model;
        private IBackground abstractModel;

        public BackgroundModelTest()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        [TestInitialize]
        public void TestSetup()
        {
            abstractModel = BackgroundFactory.CreateExport().Value;
            model = abstractModel as BackgroundModel;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveDefaultBorder()
        {
            var defaultBorder = model.Border;
            defaultBorder.ShouldNotBe(null);

            var newBorder = new BorderModel();
            model.Border = newBorder;
            model.Border.ShouldBe(newBorder);

            model.Border = null;
            model.Border.ShouldBe(defaultBorder);
            model.Border.ShouldBe(defaultBorder);
        }

        [TestMethod]
        public void ShouldShareCornerRadiusValueWithBorder()
        {
            var radius1 = new CornerRadius(1, 2, 3, 4);

            model.CornerRadius = radius1;
            model.Border.CornerRadius.ShouldBe(radius1);

            var radius2 = new CornerRadius(9,8,7,6);
            model.Border.CornerRadius = radius2;
            model.CornerRadius.ShouldBe(radius2);
        }

        [TestMethod]
        public void ShouldFireCornerRadiusChangedWhenRadiusChangesOnBorder()
        {
            model.ShouldFirePropertyChanged<IBackground>(() =>
                                                             {
                                                                 model.Border.CornerRadius = new CornerRadius(5);
                                                             }, m => m.CornerRadius);
        }

        [TestMethod]
        public void ShouldFireCornerRadiusChangedWhenRadiusChangesOnCustomBorder()
        {
            model.Border = new BorderModel();
            model.ShouldFirePropertyChanged<IBackground>(() =>
                            {
                                model.Border.CornerRadius = new CornerRadius(5);
                            }, m => m.CornerRadius);
        }

        [TestMethod]
        public void ShouldCreateView()
        {
            var view = model.CreateView();
            view.ShouldBeInstanceOfType<Background>();
            view.DataContext.ShouldBe(abstractModel);
        }
        #endregion
    }
}
