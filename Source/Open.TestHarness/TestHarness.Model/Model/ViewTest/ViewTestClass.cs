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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.Collection;


namespace Open.TestHarness.Model
{
    /// <summary>Represents a method decorated with the [ViewTestClass] attribute.</summary>
    public class ViewTestClass : ModelBase
    {
        #region Head
        /// <summary>Fires when a view test needs to be executed (see ExecuteTest method).</summary>
        public event EventHandler<TestExecuteEventArgs> ExecuteRequest;
        protected void OnExecuteRequest(ViewTest test) { if (ExecuteRequest != null) ExecuteRequest(this, new TestExecuteEventArgs{ViewTest = test}); }

        public const string PropCurrentControls = "CurrentControls";
        public const string PropCurrentViewTest = "CurrentViewTest";
        public const string PropDisplayName = "DisplayName";
        public const string PropIsCurrent = "IsCurrent";

        private ObservableCollection<ViewTest> viewTests;
        private Type type;
        private ViewTestClassAttribute attribute;
        private ObservableCollectionMonitor<ViewTest> viewTestsMonitor;
        private ViewTest currentViewTest;
        private readonly CurrentControlsCollection currentControls = new CurrentControlsCollection();
        private object instance;
        private static readonly List<ViewTestClass> singletonInstances = new List<ViewTestClass>();
        private string customName;

        public ViewTestClass(Type classType, string xapFileName)
        {
            Initialize(classType, xapFileName);
        }

        public ViewTestClass(string typeName, string customName, string assemblyName, string xapFileName)
        {
            this.customName = customName.AsNullWhenEmpty();
            InitializeNames(typeName, assemblyName, xapFileName);
        }

        private void Initialize(Type classType, string xapFileName)
        {
            // Store values.
            type = classType;
            InitializeNames(
                                type.FullName, 
                                ReflectionUtil.GetAssemblyName(type.Assembly.FullName), 
                                xapFileName);

            // Retrieve the attribute.
            attribute = type.GetCustomAttributes(typeof(ViewTestClassAttribute), false).FirstOrDefault() as ViewTestClassAttribute;
            if (attribute == null) throw new ArgumentException(string.Format("Class not decorated with the [{0}] attribute.", typeof(ViewTestClassAttribute).Name));

            // Finish up.
            IsActivated = true;
        }

        private void InitializeNames(string typeName, string assemblyName, string xapFileName)
        {
            TypeName = typeName;
            AssemblyName = assemblyName;
            XapFileName = xapFileName;
        }
        #endregion

        #region Event Handlers
        void Handle_Test_ExecuteRequest(object sender, EventArgs e)
        {
            // Store property state.
            currentViewTest = sender as ViewTest;

            // Update the set of controls instances.
            currentControls.Populate(currentViewTest);

            // Alert listeners.
            OnExecuteRequest(currentViewTest);
            OnPropertyChanged(PropCurrentViewTest);
            OnPropertyChanged(PropCurrentControls);

            // Pass execution back to the ViewTest with the set of controls.
            CurrentViewTest.Execute(Instance, CurrentControls);
        }
        #endregion

        #region Properties - Names
        /// <summary>Gets the full name of the type.</summary>
        public string TypeName { get; private set; }

        /// <summary>Gets the full name of the Assembly.</summary>
        public string AssemblyName { get; private set; }

        /// <summary>Gets the name XAP file containing the Assembly.</summary>
        public string XapFileName { get; private set; }

        /// <summary>Gets the display name of the class.</summary>
        public string DisplayName
        {
            get
            {
                // Check for custom name set in constructor.
                if (customName != null) return customName;
                
                // Return the custom name specified on the [Attribute], if there was one.
                if (attribute!=null)
                {
                    var custom = Attribute.DisplayName.AsNullWhenEmpty();
                    if (custom != null) return custom;
                }

                // Format the name from the class name.
                const string viewTest = "ViewTest";

                var pieces = TypeName.Split(".".ToCharArray());
                var name = pieces[pieces.Length - 1];
                name = name.RemoveEnd(viewTest).FormatUnderscores();
                return name;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///    Gets whether the Type has been activated.  
        ///    True if a 'Type' was passed into the constructor, or the any member has been called that has caused the 'Activate' method to be called.
        ///    False if a string type-name was passed to the constructor and the 'Active' method has not yet been called.</summary>
        /// </summary>
        public bool IsActivated{ get; private set; }


        /// <summary>Gets the type of the class.</summary>
        public Type Type
        {
            get
            {
                Activate();
                return type;
            }
        }

        /// <summary>Gets the [ViewTestClass] attribute.</summary>
        public ViewTestClassAttribute Attribute
        {
            get
            {
                Activate();
                return attribute;
            }
        }

        /// <summary>Gets the module that this class is contained within.</summary>
        public ViewTestClassesAssemblyModule ParentModule { get { return TestHarnessModel.Instance.GetModule(XapFileName); } }

        /// <summary>Gets the collection of methods within the class decorated with the [ViewTest] attribute.</summary>
        public ObservableCollection<ViewTest> ViewTests
        {
            get
            {
                Activate();
                if (viewTests == null)
                {
                    viewTests = new ObservableCollection<ViewTest>();
                    PopulateViewTests();
                    viewTestsMonitor = new ObservableCollectionMonitor<ViewTest>(
                                                        viewTests,
                                                        (c, item) => { item.ExecuteRequest += Handle_Test_ExecuteRequest; },
                                                        (c, item) => { item.ExecuteRequest -= Handle_Test_ExecuteRequest; }
                                                        );
                }
                return viewTests;
            }
        }

        /// <summary>
        ///    Gets the default [ViewTest].  This is the first test marked with the 'Default' property, 
        ///    or if no tests are marked as default, the first test.
        /// </summary>
        public ViewTest DefaultViewTest
        {
            get
            {
                if (ViewTests.Count == 0) return null;
                var defaultTest = ViewTests.FirstOrDefault(item => item.Attribute.Default);
                return defaultTest ?? ViewTests[0];
            }
        }

        /// <summary>Gets the currently executed view-test.</summary>
        public ViewTest CurrentViewTest
        {
            get { return currentViewTest ?? DefaultViewTest; }
        }

        /// <summary>Gets whether the assembly is currently loaded for this class.</summary>
        public bool IsAssemblyLoaded
        {
            get { return TestHarnessModel.Instance.IsLoaded(XapFileName); }
        }

        /// <summary>Gets or sets whether the class is the currently selected item.</summary>
        public bool IsCurrent
        {
            get { return TestHarnessModel.Instance.CurrentClass == this; }
            set
            {
                // Setup initial conditions.
                if (value == IsCurrent) return;
                if (value) Activate();

                // Store value at root level of the model.
                var testHarness = TestHarnessModel.Instance;
                if (testHarness.CurrentClass == this && value == false)
                {
                    testHarness.CurrentClass = null;
                }
                else
                {
                    testHarness.CurrentClass = this;
                }

                // Finish up.
                OnPropertyChanged(PropIsCurrent);
            }
        }

        /// <summary>Gets the current set of controls pertaining the the current [ViewTest].</summary>
        public ObservableCollection<UIElement> CurrentControls
        {
            get
            {
                if (currentControls.Count == 0) currentControls.Populate(CurrentViewTest);
                return currentControls;
            }
        }
        #endregion

        #region Properties - Internal
        public object Instance
        {
            get
            {
                if (instance == null) instance = Activator.CreateInstance(Type);
                return instance;
            }
        }
        #endregion

        #region Methods - Create Singleton (Static)
        /// <summary>Create a new singleton instance of the model.</summary>
        /// <param name="typeName">The full name of the type.</param>
        /// <param name="customName">The custom name of the attribute (Null if not required).</param>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="xapFileName">The name of the XAP file containing the class.</param>
        /// <returns>A new model class instance.</returns>
        public static ViewTestClass GetSingleton(string typeName, string customName, string assemblyName, string xapFileName)
        {
            var model = singletonInstances.FirstOrDefault(item => item.IsEquivalent(typeName, assemblyName));
            if (model == null)
            {
                model = new ViewTestClass(typeName, customName, assemblyName, xapFileName);
                singletonInstances.Add(model);
            }
            return model;
        }

        /// <summary>Create a new singleton instance of the model.</summary>
        /// <param name="type">The type to model.</param>
        /// <param name="xapFileName">The name of the XAP file containing the class.</param>
        /// <returns>A new model class instance.</returns>
        public static ViewTestClass GetSingleton(Type type, string xapFileName)
        {
            var model = singletonInstances.FirstOrDefault(item => item.IsEquivalent(type));
            if (model == null)
            {
                model = new ViewTestClass(type, xapFileName);
                singletonInstances.Add(model);
            }
            return model;
        }
        #endregion

        #region Methods
        /// <summary>Loads the Type from the TypeName (if not already activated).</summary>
        /// <returns>True success, otherwise False (failed to load).</returns>
        public bool Activate()
        {
            // Setup initial conditions.
            if (IsActivated) return true;

            // Attempt to load the assembly.
            try
            {
                Initialize(GetAssembly().GetType(TypeName, true), XapFileName);
            }
            catch (TypeLoadException) { return false; }

            // Finish up.
            OnPropertyChanged(PropDisplayName);
            return true;
        }

        /// <summary>Resets the view tests.</summary>
        /// <param name="runDefaultTest">Flag indicating if the default test should be executed.</param>
        public void ResetTests(bool runDefaultTest)
        {
            instance = null;
            ViewTests.RemoveAll();
            currentControls.Reset();
            PopulateViewTests();
            Output.Clear();
            if (runDefaultTest && DefaultViewTest != null) DefaultViewTest.Execute();
        }

        /// <summary>Causes the a new test instance to be loaded.</summary>
        public void Reload()
        {
            var testHarness = TestHarnessModel.Instance;
            testHarness.CurrentClass = null;
            testHarness.CurrentClass = this;
        }

        /// <summary>Determines whether the specified type is represented by this model.</summary>
        /// <param name="compareType"></param>
        /// <returns>True if the value matches, otherwise False.</returns>
        public bool IsEquivalent(Type compareType)
        {
            return IsEquivalent(compareType.FullName, compareType.Assembly.GetAssemblyName());
        }

        /// <summary>Determines whether the specified values match this model.</summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="assemblyName">The assembly name.</param>
        /// <returns>True if the values match, otherwise False.</returns>
        public bool IsEquivalent(string typeName, string assemblyName)
        {
            return typeName == TypeName && assemblyName == AssemblyName; 
        }

        internal void RaiseIsCurrentChanged()
        {
            OnPropertyChanged(PropIsCurrent);
        }
        #endregion

        #region Internal
        private Assembly GetAssembly()
        {
            // See if the assembly has already been loaded.
            var assembly = 
                        TestHarnessModel.Instance.LoadedAssemblies.FirstOrDefault(
                        item => ReflectionUtil.GetAssemblyName(item.FullName) == AssemblyName);
            if (assembly != null) return assembly;

            // Load and store the assembly.
            assembly = Assembly.Load(AssemblyName);
            TestHarnessModel.Instance.LoadedAssemblies.Add(assembly);

            // Finish up.
            return assembly;
        }

        private void PopulateViewTests()
        {
            viewTests.AddRange(ViewTest.GetMethods(Type));
        }
        #endregion
    }
}
