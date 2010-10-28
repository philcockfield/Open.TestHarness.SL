using System;
using System.Collections.Generic;
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
                // Script files.
                CopyFiles(@"\JavaScript\Test\Open.Core.Test\bin\Debug", @"\Clr\Test\TestHarness.Web\Content\Scripts", FilterJs);
                CopyFiles(@"\JavaScript\Open.TestHarness\bin\Debug", @"\Clr\Open.Core.Web\Content\Scripts", FilterJs);

                // DLL's to Bin.
                CopyToBin("Open.Core");
                CopyToBin("Open.Core.Controls");
                CopyToBin("Open.Core.Lists");
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
        private static void CopyToBin(string projectFolder)
        {
            const string binPath = @"\Bin\Bin.JavaScript";
            CopyFiles(string.Format(@"\JavaScript\{0}\bin\Debug", projectFolder), binPath, "*.dll", "*.xml");
        }

        private static void CopyFiles(string fromPath, string toPath, params string[] filter)
        {
            var to = GetFolder(toPath);

            var files = new List<FileInfo>();
            foreach (string item in filter)
            {
                files.AddRange(GetFolder(fromPath).GetFiles(item));
            }

            foreach (FileInfo fileInfo in files)
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
