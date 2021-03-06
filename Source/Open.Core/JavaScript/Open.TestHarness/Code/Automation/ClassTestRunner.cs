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
        public void WriteResults(ILog log)
        {
            // Setup initial conditions.
            int successes = Successes;
            int failures = Failures;
            bool hasFailures = failures > 0;

            // Log the summary details.
            string summary = string.Format("Test run for class <b>{0}</b>", classInfo.DisplayName);
            IHtmlList list = log.WriteListSeverity(summary, hasFailures ? LogSeverity.Error : LogSeverity.Success);
            list.Add(string.Format("Successes: {0} ({1}%)", successes, ToPercent(successes)));
            list.Add(string.Format("Failures: {0} ({1}%)", failures, ToPercent(failures)));

            // Write out exceptions.
            if (hasFailures) log.LineBreak();
            foreach (ExecutedTest item in results)
            {
                if (item.Error == null) continue;
                item.Method.LogError(item.Error);
            }
            log.NewSection();
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
