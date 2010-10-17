using System;
using System.Runtime.CompilerServices;
using Open.Core.Controls.HtmlPrimitive;

namespace Open.Core.Test.ViewTests.Controls
{
    public class LogTest
    {
        #region Head
        private string myReadWrite;
        #endregion

        #region Properties
        public static string MyStatic { get { return "My Static Value"; } }

        public string MyReadOnly { get { return "Foo"; } }
        public string MyReadWrite
        {
            get { return myReadWrite; }
            set { myReadWrite = value; }
        }
        public string MyNullProperty { get { return null; } }
        public string MyErrorProperty { get { throw new Exception("Error from Property"); } }
        private string MyPrivateProperty { get { return "Private Value"; } }
        #endregion

        #region Tests
        public void Log__Info() { Log.Info("Info"); }
        public void Log__Debug() { Log.Debug("Debug"); }
        public void Log__Warning() { Log.Warning("Warning"); }
        public void Log__Error() { Log.Error(new Exception("My error.")); }
        public void Log__Null() { Log.Info(null); }
        public void Log__Empty_String() { Log.Info(""); }
        public void Log__White_Space() { Log.Info("   "); }
        public void Log__Event() { Log.Event("My Event"); }
        public void Log__Background_Color() { Log.Write("On custom Background Color", "#bee85a"); }
        public void Log__Write_Icon() { Log.WriteIcon("My value", Icons.SilkAccept); }

        public void LineBreak(){ Log.LineBreak();}
        public void NewSection() { Log.NewSection(); }

        public void Log__WriteProperties() { WriteProperties("My Title"); }
        public void Log__WriteProperties_null_title() { WriteProperties(null); }
        public void Log__WriteProperties_no_title_passed() { WriteProperties(); }
        public void Log__WriteProperties_string() { WriteProperties("Foo String"); }

        [AlternateSignature]
        private static extern void WriteProperties();
        private static void WriteProperties(string title)
        {
            LogTest instance = new LogTest();
            instance.MyReadWrite = "Bar";
            if (Script.IsUndefined(title))
            {
                Log.WriteProperties(instance);
            }
            else
            {
                Log.WriteProperties(instance, title);
            }
        }

        public void Log__WriteList() { WriteList("My Title"); }
        public void Log__WriteList_no_title() { WriteList(null); }
        private static void WriteList(string title)
        {
            IHtmlList list = Log.WriteList(title, "#ecffc0");
            for (int i = 1; i <= 3; i++)
            {
                list.Add("Item " + i);
            }
        }

        public void Log__ListSeverity_Warning()
        {
            Log.WriteListSeverity("Foo", LogSeverity.Warning).Add("Item");
        }


        public void Log_After_Delay()
        {
            DelayedAction.Invoke(0.5, delegate { Log.Info("Logged after delay"); });
        }

        public void Clear() { Log.Clear(); }

        public void Sample1()
        {
            Log.Info("This is a log entry");
            Log.Info("Another entry within the same test.");
        }

        public void Sample2()
        {
            Log.Info("This entry came from another test (Sample2).");
            Log.Info("Note the section divider visually showing that these entries were emitted from different tests.");
        }
        #endregion
    }
}
