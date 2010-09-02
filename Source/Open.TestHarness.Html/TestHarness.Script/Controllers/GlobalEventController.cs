using System;
using System.Collections;
using Open.Core;
using TH = Open.Core.TestHarness;


namespace Open.TestHarness
{
    /// <summary>Listens for global events within the environment.</summary>
    internal class GlobalEventController
    {
        #region Head

        /// <summary>Constructor.</summary>
        public GlobalEventController()
        {
            // Wire up events.
            TH.TestClassRegistered += OnTestClassRegistered;
        }
        #endregion


        #region Event Handlers
        private void OnTestClassRegistered(object sender, TestClassEventArgs e)
        {
            if (e.TestPackage == null) return;

            TestPackageDef def = TestPackageDef.GetSingleton(e.TestPackage);
            def.AddClass(e.TestClass);

            Log.Info("!! From Event");
            //Log.Info("Package: " + e.TestPackage.FullName);
            Log.Info("Class: " + e.TestClass.FullName);
            Log.Info("def.Count: " + def.Count);
            //Log.Info("Same: " + (e.TestPackage == prev));

            foreach (TestClassDef item in def)
            {
                Log.Info("> " + item.Type.Name);
            }

//            prev = e.TestPackage;
            Log.LineBreak();

        }
        #endregion
    }
}
