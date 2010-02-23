using System.Linq;
using System.Collections.Generic;
using System.Windows;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Interprets the query string.</summary>
    public class QueryString : ModelBase
    {
        #region Head
        private const string keyXap = "xap";

        /// <summary>Constructor.</summary>
        public QueryString() : this(Application.Current.GetQueryString()){}

        /// <summary>Constructor.</summary>
        /// <param name="queryStringItems">The query-string items.</param>
        public QueryString(IEnumerable<KeyValuePair<string, string>> queryStringItems)
        {
            // Setup initial conditions.
            if (queryStringItems == null) queryStringItems = new List<KeyValuePair<string, string>>();
            Items = queryStringItems;

            // Extract values from query string.
            ExtractXapFiles();

            // Initialize.
            LoadXapModules();
        }
        #endregion

        #region Properties
        /// <summary>Gets the query-string as a set of key:value pairs. </summary>
        public IEnumerable<KeyValuePair<string, string>> Items { get; private set; }

        /// <summary>Gets the collection of XAP files that are specified within the query-string.</summary>
        public IEnumerable<string> XapFiles { get; private set; }
        #endregion

        #region Internal
        private void ExtractXapFiles()
        {
            var list = new List<string>();
            foreach (var item in Items.Where(m => m.Key.ToLower() == keyXap))
            {
                var value = item.Value.AsNullWhenEmpty();
                if (value == null) continue;
                value = value.RemoveEnd(".xap").TrimEnd(".".ToCharArray());
                list.Add(value + ".xap");
            }
            XapFiles = list;
        }

        private void LoadXapModules()
        {
            if (XapFiles.Count() == 0) return;

            var testHarness = TestHarnessModel.Instance;
            var modules = testHarness.Modules
                                    .Where(m => m.GetType() == typeof(ViewTestClassesAssemblyModule))
                                    .Cast<ViewTestClassesAssemblyModule>();

            foreach (var fileName in XapFiles)
            {
                var name = fileName.RemoveEnd(".xap").ToLower();
                if (modules.Count(m => m.XapFileName.ToLower() == name) == 0)
                {
                    testHarness.AddModule(fileName);
                }
            }
        }
        #endregion
    }
}
