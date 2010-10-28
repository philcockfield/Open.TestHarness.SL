using System;
using System.IO;
using System.Reflection;

namespace PostBuildCopier
{
    class Program
    {
        #region Head
        private const string FilterJs = "*.js";
        private static readonly string ProgramPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).CodeBase).Remove(0, 8);

        static void Main(string[] args)
        {
            try
            {
                CopyFiles(@"\JavaScript\Test\Open.Core.Test\bin\Debug", @"\Clr\Test\TestHarness.Web\Content\Scripts", FilterJs);
                CopyFiles(@"\JavaScript\Open.TestHarness\bin\Debug", @"\Clr\Open.Core.Web\Content\Scripts", FilterJs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to copy scripts");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        #endregion

        #region Properties
        public static DirectoryInfo OpenCoreFolder
        {
            get
            {
                var folder = new DirectoryInfo(ProgramPath);
                for (int i = 0; i < 5; i++) // Walk up 5 levels.
                {
                    if (folder.Parent == null) return null;
                    folder = folder.Parent;
                }
                return folder;
            }
        }
        #endregion

        #region Internal
        private static void CopyFiles(string fromPath, string toPath, string filter)
        {
            var to = GetFolder(toPath);
            foreach (FileInfo fileInfo in GetFolder(fromPath).GetFiles(filter))
            {
                new CopyItem(fileInfo.FullName, to.FullName).Copy();
            }
        }

        private static DirectoryInfo GetFolder(string path)
        {
            return new DirectoryInfo(OpenCoreFolder.FullName + path);
        }
        #endregion
    }
}
