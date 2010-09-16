using System;
using System.Collections;
using Open.Core;
using Open.Core.Controls.HtmlPrimitive;

namespace Open.Testing.Models
{
    /// <summary>Represents a single test method.</summary>
    public class MethodInfo : ModelBase
    {
        #region Head
        public const string KeyGetter = "get_";
        public const string KeySetter = "set_";
        public const string KeyField = "_";
        public const string KeyFunction = "function";


        private readonly ClassInfo classInfo;
        private readonly string name;
        private readonly string displayName;
        private readonly bool isSpecial;

        /// <summary>Constructor.</summary>
        /// <param name="classInfo">The test-class that this method is a member of.</param>
        /// <param name="name">The name of the method.</param>
        public MethodInfo(ClassInfo classInfo, string name)
        {
            this.classInfo = classInfo;
            this.name = name;
            displayName = FormatName(name);
            isSpecial = MethodHelper.IsSpecial(Name);
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-class that this method is a member of.</summary>
        public ClassInfo ClassInfo { get { return classInfo; } }

        /// <summary>Gets the name of the method.</summary>
        public string Name { get { return name; } }

        /// <summary>Gets the display version of the name.</summary>
        public string DisplayName { get { return displayName; } }

        /// <summary>Gets or sets whether this method is one of the special Setup/Teardown methods.</summary>
        public bool IsSpecial { get { return isSpecial; } }
        #endregion

        #region Methods
        /// <summary>Invokes the method.</summary>
        /// <returns>The exception that occured (if any).</returns>
        public Exception Invoke()
        {
            // Setup initial conditions.
            object instance = ClassInfo.Instance;

            // Invoke the pre "setup" method (if this a standard test-method and is not itself one of the special methods).
            if (!IsSpecial && ClassInfo.TestInitialize != null) ClassInfo.TestInitialize.Invoke();

            // Invoke the method.
            Exception error = null;
            try
            {
                Function func = Helper.Reflection.GetFunction(instance, Name);
                if (func != null) func.Call(instance);
            }
            catch (Exception e)
            {
                error = e;
                Log.Error(FormatError(error));
            }

            // Invoke the post "teardown" method (if this a standard test-method and is not itself one of the special methods).
            if (!IsSpecial && ClassInfo.TestCleanup != null) ClassInfo.TestCleanup.Invoke();

            // Finish up.
            return error;
        }

        /// <summary>Formats an error message.</summary>
        /// <param name="error">The invoke error.</param>
        public string FormatError(Exception error)
        {
            HtmlList htmlList = new HtmlList(HtmlListType.Unordered, CssSelectors.Classes.LogIndentedList);
            htmlList.Add(string.Format("Message: '{0}'", error.Message));
            htmlList.Add("Method: " + Helper.String.ToCamelCase(Name));
            htmlList.Add("Class: " + ClassInfo.ClassType.FullName);
            htmlList.Add("Package: " + Html.ToHyperlink(ClassInfo.PackageInfo.Loader.ScriptUrl, null, LinkTarget.Blank));
            return string.Format(
                                "<b>Exception</b> Failed while executing '<b>{0}</b>'.<br/>{1}",
                                DisplayName,
                                htmlList.OuterHtml);
        }
        #endregion

        #region Methods : Static
        /// <summary>Determines whether the specified DictionaryEntry represents a valid test-method.</summary>
        /// <param name="item">The Dictionaty item to examine.</param>
        public static bool IsTestMethod(DictionaryEntry item)
        {
            // Setup initial conditions.
            string key = item.Key;
            if (Type.GetScriptType(item.Value) != KeyFunction) return false;

            // Check for special methods.
            if (MethodHelper.IsConstructor(key)) return false;
            if (MethodHelper.IsSpecial(key)) return false;

            // Check for non-method signatures.
            if (key.StartsWith(KeyField)) return false;
            if (key.StartsWith(KeyGetter)) return false;
            if (key.StartsWith(KeySetter)) return false;

            // Finish up.
            return true;
        }

        /// <summary>Formats a name into a display name (replace underscores with spaces etc.).</summary>
        /// <param name="name">The name to format.</param>
        public static string FormatName(string name)
        {
            name = name.Replace("__", ": ");
            name = name.Replace("_", " ");
            name = Helper.String.ToSentenceCase(name);
            return name;
        }
        #endregion
    }
}
