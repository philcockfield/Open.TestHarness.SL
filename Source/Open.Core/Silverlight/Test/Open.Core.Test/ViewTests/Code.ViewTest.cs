using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Open.Core.Common;
using System.Diagnostics;
using Open.Core.Common.Converter;
using System;
using Open.Core.Composite;
using Open.Core.Test.ViewTests;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests
{
//    [ViewTestClass(IsPropertyExplorerVisible = false)]
    [ViewTestClass]
    public class CodeViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize()
        {
        }
        #endregion

        #region Tests

        [ViewTest]
        public void TestControl(TestControl control)
        {
            control.Width = 350;
            control.Height = 350;

        }


        [ViewTest]
        public void Path_Strings()
        {
            Debug.WriteLine("GetServerUrl: " + Application.Current.GetServerUrl());
            Debug.WriteLine("GetXapFileName: " + Application.Current.GetXapFileName());
            Debug.WriteLine("GetClientBinPath: " + Application.Current.GetClientBinPath());
            Debug.WriteLine("GetClientBinUrl: " + Application.Current.GetClientBinUrl());
            Debug.WriteLine("GetApplicationRootUrl: " + Application.Current.GetApplicationRootUrl());
        }

        [ViewTest]
        public void CommaConverter()
        {
            var value = 5.0;
            for (int i = 0; i < 20; i++)
            {
                value -= 0.5;
                WriteCommaConverter(value);
            }
            WriteCommaConverter(999);
            WriteCommaConverter(1000);
        }

        private static void WriteCommaConverter(double value)
        {
            var converter = new CommaConverter();
            Debug.WriteLine("> " + value + ": " + converter.Convert(value, null, null, null));
        }

        [ViewTest]
        public void ToComma()
        {
            WriteToComma(500);
            WriteToComma(500.5);
            WriteToComma(-1000);
            WriteToComma(1000);
            WriteToComma(1000.5);
            WriteToComma(1000.005);
            WriteToComma(123456789.005);
        }

        private static void WriteToComma(double value)
        {
            Debug.WriteLine("ToComma - " + value + ": " + value.ToComma());
        }

        [ViewTest]
        public void ToFileSizeUnit()
        {
            WriteFileSizeUnit(0.123456);
            WriteFileSizeUnit(1);
            WriteFileSizeUnit(500);
            WriteFileSizeUnit(845.267);
            WriteFileSizeUnit(1024);
            WriteFileSizeUnit(532412);
            WriteFileSizeUnit(870707.26657865);
            WriteFileSizeUnit(1047527424);
            WriteFileSizeUnit(1635308797.952);
        }

        private static void WriteFileSizeUnit(double value)
        {
            Debug.WriteLine(value.ToComma() + " => " + value.ToFileSize());
        }

        [ViewTest]
        public void Version()
        {
            var assembly = Assembly.GetExecutingAssembly();
            Debug.WriteLine("Assembly: " + assembly.FullName);
            Debug.WriteLine("Version: " + assembly.Version());
        }

        [ViewTest]
        public void IsApplicationStorageEnabled()
        {
            Debug.WriteLine("IsApplicationStorageEnabled: " + SettingsModelBase.IsApplicationStorageEnabled);
        }

        [ViewTest(AllowAutoRun = false)]
        public void Uri__NavigateTo()
        {
            var uri = new Uri("http://google.com");
            uri.NavigateTo();
        }

        [ViewTest]
        public void Uri__NavigateTo_window()
        {
            var uri = new Uri("http://google.com");
            uri.NewWindow();
        }

        private Settings model1;
        private Settings model2;
        private Settings model3;
        private Settings[] models;

        private void CreateSettings()
        {
            model1 = new Settings(new Uri("http://one"));
            model2 = new Settings(new Uri("http://one/2"));
            model3 = new Settings(new Uri("http://three/"));
            models = new [] { model1, model2, model3 };
        }

        [ViewTest]
        public void Cache_Write()
        {
            CreateSettings();

            foreach (var model in models)
            {
                Output.Write(model.Id + " | Text: " + model.Text);
            }
            Output.Break();
        }

        [ViewTest]
        public void Cache_Save()
        {
            CreateSettings();

            foreach (var model in models)
            {
                model.Text = "value";
                model.Save();
            }

            Cache_Write();
        }

        [ViewTest]
        public void Cache_Clear_Model1()
        {
            CreateSettings();
            model1.Clear();
        }

        [ViewTest]
        public void ControlAndENum(Placeholder control, Visibility visibility)
        {
            
        }

        [ViewTest]
        public void Control(Placeholder control1)
        {

        }

        [ViewTest]
        public void Controls(Placeholder control1, Visibility visibility1, Visibility visibility2, Placeholder control2,  Visibility visibility3)
        {
        }


        [ViewTest]
        public void Host()
        {
            Output.WriteProperties(Application.Current.Host);
            Output.WriteProperties(Application.Current.Host.Source);
            Output.WriteProperties(HtmlPage.Document.DocumentUri);
        }



        #endregion

        public class Settings : SettingsModelBase
        {
            public Settings(Uri uri) : base(SettingsStoreType.Application, "Model-" + uri){}
            public string Text
            {
                get { return GetPropertyValue<Settings, string>(m => m.Text); }
                set { SetPropertyValue<Settings, string>(m => m.Text, value); }
            }
        }
    }
}
