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
using System.Collections.Generic;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;
using System.Threading;
using System.Windows.Media;

namespace Open.Core.Common.Test.Core.Common.Testing.Controls
{
    [ViewTestClass]
    public class OutputViewTest
    {
        #region Head
        private IOutput writer;


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(OutputLog control)
        {
            control.Width = 600;
            control.Height = 350;

            writer = new OutputWriter();
            control.Writer = writer;

            Output.Write(Colors.Green, "Written from initial ViewTest.");
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Write__Short(OutputLog control)
        {
            writer.Write(RandomData.LoremIpsum(5));
        }

        [ViewTest]
        public void Write__Long(OutputLog control)
        {
            writer.Write(RandomData.LoremIpsum(20, 50));
        }

        [ViewTest]
        public void Write__Blank_Line(OutputLog control)
        {
            writer.Write();
        }

        [ViewTest]
        public void Write__Null_Value(OutputLog control)
        {
            writer.Write(null);
        }

        [ViewTest]
        public void Write__From_Background_Thread(OutputLog control)
        {
            var thread = new Thread(() => writer.Write(RandomData.LoremIpsum(5)));
            thread.Start();
        }

        [ViewTest]
        public void Write__Green(OutputLog control)
        {
            writer.Write(new OutputLine { Value = RandomData.LoremIpsum(5), Color = Colors.Green });
        }

        [ViewTest]
        public void Write__Red(OutputLog control)
        {
            writer.Write(new OutputLine { Value = RandomData.LoremIpsum(5), Color = Colors.Red });
        }

        [ViewTest]
        public void Write__Orange(OutputLog control)
        {
            writer.Write(new OutputLine { Value = RandomData.LoremIpsum(5), Color = Colors.Orange });
        }

        [ViewTest]
        public void Write__20(OutputLog control)
        {
            for (int i = 0; i < 20; i++)
            {
                writer.Write(RandomData.LoremIpsum(5));
            }
        }

        [ViewTest]
        public void Break(OutputLog control)
        {
            writer.Break();
        }

        [ViewTest]
        public void Clear(OutputLog control)
        {
            writer.Clear();
        }

        [ViewTest]
        public void ResetLineCount(OutputLog control)
        {
            control.ResetLineCount();
        }

        [ViewTest]
        public void Toggle__IsToolbarVisible(OutputLog control)
        {
            control.IsToolbarVisible = !control.IsToolbarVisible;
        }

        [ViewTest]
        public void Toggle__IsActive(OutputLog control)
        {
            control.IsActive = !control.IsActive;
        }

        [ViewTest]
        public void Global_Output__Write(OutputLog control)
        {
            Output.Write(RandomData.LoremIpsum(5));
        }

        [ViewTest]
        public void Global_Output__Write_Blank_Line(OutputLog control)
        {
            Output.Write();
        }

        [ViewTest]
        public void Global_Output__Write_Null_Value(OutputLog control)
        {
            Output.Write(null);
        }

        [ViewTest]
        public void Global_Output__Write_With_Color(OutputLog control)
        {
            Output.Write(Colors.Green, RandomData.LoremIpsum(5));
        }

        [ViewTest]
        public void Global_Output__Write_Values(OutputLog control)
        {
            Output.Write("One", "Two", "Three");
        }

        [ViewTest]
        public void Global_Output__WriteTitle(OutputLog control)
        {
            Output.WriteTitle(RandomData.LoremIpsum(3));
        }

        [ViewTest]
        public void Global_Output__WriteProperties(OutputLog control)
        {
            var stub = new Stub { Text = RandomData.LoremIpsum(5) };
            Output.WriteProperties(stub);
        }

        [ViewTest]
        public void Global_Output__WriteProperties_IncludeHierarchy(OutputLog control)
        {
            var stub = new Stub { Text = RandomData.LoremIpsum(5) };
            Output.WriteProperties(stub, true);
        }

        [ViewTest]
        public void Global_Output__WriteProperties_Specific(OutputLog control)
        {
            var stub = new Stub { Text = RandomData.LoremIpsum(5) };
            Output.WriteProperties(stub, m => m.ParentProperty, m => m.Text);
        }

        [ViewTest]
        public void Global_Output__WriteCollection(OutputLog control)
        {
            var items = CreateStubs(5);
            Output.WriteCollection(items);
        }

        [ViewTest]
        public void Global_Output__WriteCollection_Custom(OutputLog control)
        {
            var items = CreateStubs(5);
            Output.WriteCollection(items, stub => "Custom Output: " + stub.Number);
        }

        [ViewTest]
        public void Global_Output__Break(OutputLog control)
        {
            Output.Break();
        }
        #endregion

        #region Stubs
        private IEnumerable<Stub> CreateStubs(int total)
        {
            var list = new List<Stub>();
            for (var i = 0; i < total; i++)
            {
                list.Add(CreateStub());
            }
            return list;
        }

        private static Stub CreateStub()
        {
            return new Stub
            {
                Text = RandomData.LoremIpsum(5),
                Number = RandomData.Random.Next(),
                ParentProperty = RandomData.GetDateBeforeNow(50)
            };
        }

        public class Stub : ParentStub
        {
            public string Text
            {
                get { return GetPropertyValue<Stub, string>(m => m.Text); }
                set { SetPropertyValue<Stub, string>(m => m.Text, value); }
            }

            public int? Number
            {
                get { return GetPropertyValue<Stub, int?>(m => m.Number); }
                set { SetPropertyValue<Stub, int?>(m => m.Number, value); }
            }

            public override string ToString()
            {
                var number = Number;
                if (number == null) number = 0;
                return string.Format("{0} - {1}", number, Text);
            }
        }

        public class ParentStub : ModelBase
        {
            public DateTime ParentProperty
            {
                get { return GetPropertyValue<ParentStub, DateTime>(m => m.ParentProperty); }
                set { SetPropertyValue<ParentStub, DateTime>(m => m.ParentProperty, value); }
            }
        }
        #endregion
    }
}
