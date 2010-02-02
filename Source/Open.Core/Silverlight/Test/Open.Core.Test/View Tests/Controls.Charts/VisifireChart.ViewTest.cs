//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
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
using Open.Core.UI.Charts;
using Visifire.Charts;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Charts
{
    [ViewTestClass]
    public class VisifireChartViewTest
    {
        #region Head

        private VisifireChartViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(VisifireChart control)
        {
            control.Width = 500;
            control.Height = 500;

            viewModel = new VisifireChartViewModel
                            {
                                AxisTitleX = "My X Axis",
                                AxisTitleY = "My Y Axis",
                            };
            control.ViewModel = viewModel;

            Load_DataPoints(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Load_DataPoints(VisifireChart control)
        {
            viewModel.StartConfiguration();
            viewModel.DataPoints.RemoveAll();

            for (var i = 0; i < 6; i++)
            {
                var dataPoint = new DataPoint { YValue = RandomData.Random.Next(10, 100) };
                viewModel.DataPoints.Add(dataPoint);
            }
            viewModel.EndConfiguration();
        }

        [ViewTest]
        public void Change__ChartType(VisifireChart control)
        {
            viewModel.ChartType = viewModel.ChartType.NextValue<RenderAs>();
        }

        [ViewTest]
        public void Change__Axis_Titles(VisifireChart control)
        {
            viewModel.AxisTitleX = RandomData.LoremIpsum(2, 5);
            viewModel.AxisTitleY = RandomData.LoremIpsum(2, 5);
        }

        [ViewTest]
        public void Toggle__IsAnimated(VisifireChart control)
        {
            viewModel.IsAnimated = !viewModel.IsAnimated;
        }
        #endregion
    }
}
