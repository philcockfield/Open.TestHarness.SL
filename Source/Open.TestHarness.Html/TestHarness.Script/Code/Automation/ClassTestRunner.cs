using System;
using System.Collections;
using Open.Core;
using Open.Core.Controls.HtmlPrimitive;
using Open.Testing.Models;

namespace Open.Testing.Automation
{
    /// <summary>Runs all tests in a single class.</summary>
    public class ClassTestRunner
    {
        #region Head
        private readonly ClassInfo classInfo;
        private readonly ArrayList results = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="classInfo">The class to run.</param>
        public ClassTestRunner(ClassInfo classInfo)
        {
            this.classInfo = classInfo;
        }
        #endregion

        #region Properties
        /// <summary>Gets the total number of tests that have been run.</summary>
        public int Total { get { return results.Count; } }

        /// <summary>Gets the number of successfully executed methods..</summary>
        public int Successes
        {
            get { return Helper.Collection.Total(results, delegate(object o) { return ((ExecutedTest) o).Error == null; }); }
        }

        /// <summary>Gets the number of failed tests.</summary>
        public int Failures { get { return Total - Successes; } }
        #endregion

        #region Methods
        /// <summary>Runs all the tests in the class.</summary>
        public void Run()
        {
            foreach (MethodInfo method in classInfo)
            {
                ExecutedTest item = new ExecutedTest();
                item.Method = method;
                item.Error = method.Invoke();
                results.Add(item);
            }
        }

        /// <summary>Writes the results of a test run to the output log.</summary>
        /// <param name="log">The log to write to.</param>
        public void WriteResults(LogWriter log)
        {
            // Setup initial conditions.
            int successes = Successes;
            int failures = Failures;

            // Prepare summary details.
            HtmlList list = new HtmlList(HtmlListType.Unordered, CssSelectors.Classes.LogIndentedList);
            list.Add(string.Format("Successes: {0} ({1}%)", successes, ToPercent(successes)));
            list.Add(string.Format("Failures: {0} ({1}%)", failures, ToPercent(failures)));
            string summary = string.Format("Test run for class <b>{0}</b><br/>{1}", classInfo.DisplayName, list.OuterHtml);

            // Write to log.
            log.Write(summary, failures > 0 ? LogSeverity.Error : LogSeverity.Success);
            log.Divider(LogDivider.LineBreak);

            // Write out exceptions.
            foreach (ExecutedTest item in results)
            {
                if (item.Error == null) continue;
                log.Write(item.Method.FormatError(item.Error), LogSeverity.Error);
            }
            log.Divider(LogDivider.Section);
        }
        #endregion

        #region Internal
        private double ToPercent(int count)
        {
            if (Total == 0) return 0;
            return Math.Round((count / Total) * 100);
        }
        #endregion
    }

    internal class ExecutedTest
    {
        public MethodInfo Method;
        public Exception Error;
    }
}
