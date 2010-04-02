using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.UI
{
    [Tag("current")]
    [TestClass]
    public class ViewModelBaseTest
    {
        #region Tests
        [TestMethod]
        public void ShouldReportIsActiveChanged()
        {
            var stub = new Mock();
            stub.IsActive.ShouldBe(true); // True by default.

            var eventCount = 0;
            stub.IsActiveChanged += delegate { eventCount++; };

            stub.ShouldFirePropertyChanged<Mock>(() => stub.IsActive = false, m => m.IsActive);
            stub.IsActive.ShouldBe(false);

            eventCount.ShouldBe(1);
            stub.IsActive = false;
            stub.IsActive = false;
            stub.IsActive = false;
            eventCount.ShouldBe(1);
        }
        #endregion
        
        #region Tests - Command
        [TestMethod]
        public void ShouldGetCommand()
        {
            var stub = new CmdMock();
            stub.Command1.ShouldBeInstanceOfType<ICommand>();
            stub.Command2.ShouldBeInstanceOfType<ICommand>();
            stub.Command3.ShouldBeInstanceOfType<ICommand>();
            stub.Command4.ShouldBeInstanceOfType<ICommand>();
        }

        [TestMethod]
        public void CommandShouldBeEnabled()
        {
            var stub = new CmdMock();
            stub.IsEnabled.ShouldBe(true);
            stub.Command1.CanExecute(null).ShouldBe(true);
            stub.Command2.CanExecute(null).ShouldBe(true);

            stub.IsEnabled = false;
            stub.Command1.CanExecute(null).ShouldBe(false);
            stub.Command2.CanExecute(null).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldRaiseCanExecuteChangedWhenEnabledPropertyChanges()
        {
            var stub = new CmdMock();
            stub.IsEnabled.ShouldBe(true);

            var fireCount = 0;
            stub.Command1.CanExecuteChanged += delegate { fireCount++; };

            stub.IsEnabled = false;
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldInvokeActionOnCommandExecute()
        {
            var stub = new CmdMock();

            stub.Command1.Execute(null);
            stub.CommandExecuteCount.ShouldBe(1);

            stub.Command2.Execute(null);
            stub.CommandExecuteCount.ShouldBe(2);

            stub.Command3.Execute(null);
            stub.CommandExecuteCount.ShouldBe(3);

            stub.Command4.Execute(null);
            stub.CommandExecuteCount.ShouldBe(4);
        }

        [TestMethod]
        public void ShouldReturnSingleInstanceOfCommands()
        {
            var stub = new CmdMock();
            var command = stub.Command1;
            stub.Command1.ShouldBe(command);
        }

        [TestMethod]
        public void ShouldThrowIfCommandEnabledPropertyIsNotBoolean()
        {
            var mock = new ErrorCmdMock();
            Should.Throw<ArgumentOutOfRangeException>(() => { var m = mock.Command; });
        }
        #endregion

        #region Mocks
        public class Mock : ViewModelBase
        {
        }

        public class ErrorCmdMock : ViewModelBase
        {
            public string Text { get; set; }
            public ICommand Command
            {
                get { return GetCommand<ErrorCmdMock, Button>(m => m.Command, m => m.Text, OnCommandExecute); }
            }
            private static void OnCommandExecute(){}
        }

        public class CmdMock : ViewModelBase
        {
            public bool IsEnabled
            {
                get { return GetPropertyValue<CmdMock, bool>(m => m.IsEnabled, true); }
                set { SetPropertyValue<CmdMock, bool>(m => m.IsEnabled, value, true); }
            }

            public ICommand Command1
            {
                get { return GetCommand<CmdMock, Button>(m => m.Command1, m => m.IsEnabled, OnCommandExecute); }
            }

            public ICommand Command2
            {
                get { return GetCommand<CmdMock, Button>(m => m.Command2, OnCommandExecute); }
            }

            public ICommand Command3
            {
                get { return GetCommand<CmdMock>(m => m.Command3, m => m.IsEnabled, OnCommandExecute); }
            }

            public ICommand Command4
            {
                get { return GetCommand<CmdMock>(m => m.Command4, OnCommandExecute); }
            }


            public int CommandExecuteCount;
            private void OnCommandExecute()
            {
                CommandExecuteCount++;
            }
        }
        #endregion
    }
}
