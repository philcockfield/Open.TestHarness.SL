using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Open.TestHarness.Model
{
    /// <summary>Loads an assembly.</summary>
    public class AssemblyLoader
    {
        #region Head
        public AssemblyLoader(string xapFileName)
        {
            XapFileName = xapFileName;
        }
        #endregion

        #region Properties
        /// <summary>Gets the name of the XAP file being loaded.</summary>
        public string XapFileName { get; private set; }

        #endregion


    }
}
