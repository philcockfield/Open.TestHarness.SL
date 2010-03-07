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
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Controls
{
    [ViewTestClass]
    public class DropShadowViewTest
    {
        #region Head


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DropShadow control)
        {
            control.ViewModel = new DropShadowViewModel();
            Direction__Down(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Direction__Down(DropShadow control)
        {
            control.ViewModel.Direction = Direction.Down;
            control.SetSize(300, 15);

            //            control.Width = 100;
        }

        [ViewTest]
        public void Direction__Up(DropShadow control)
        {
            control.ViewModel.Direction = Direction.Up;
            control.SetSize(300, 15);
        }

        [ViewTest]
        public void Direction__Left(DropShadow control)
        {
            control.ViewModel.Direction = Direction.Left;
            control.SetSize(15, 300);
        }

        [ViewTest]
        public void Direction__Right(DropShadow control)
        {
            control.ViewModel.Direction = Direction.Right;
            control.SetSize(15, 300);
        }

        [ViewTest]
        public void Toggle_Color(DropShadow control)
        {
            control.ViewModel.Color = control.ViewModel.Color == Colors.Black ? Colors.Orange : Colors.Black;
        }

        [ViewTest]
        public void Toggle_Opacity(DropShadow control)
        {
            control.ViewModel.Opacity = control.ViewModel.Opacity == 0 ? 0.3 : 0;
        }

        //[ViewTest]
        //public void Toggle_Size(DropShadow control)
        //{
        //    control.ViewModel.Size = control.ViewModel.Size == 15 ? 80 : 15;
        //    Output.Write("Size: " + control.ViewModel.Size);
        //}
        #endregion
    }
}
