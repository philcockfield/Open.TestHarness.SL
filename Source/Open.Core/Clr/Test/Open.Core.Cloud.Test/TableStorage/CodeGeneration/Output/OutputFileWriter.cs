using System.IO;
using System.Reflection;
using Open.Core.Cloud.TableStorage.CodeGeneration;

namespace Open.Core.Cloud.Test.TableStorage
{
    public static class OutputFileWriter
    {
        public static void Write(string fileName, string code)
        {
            var folder = GetFolder();

            var writer = new StreamWriter(folder.FullName + @"\" + fileName);
            writer.Write(code);
            writer.Close();
        }

        public static DirectoryInfo GetFolder()
        {
            var appFolder = new DirectoryInfo(GetApplicationPath());
            appFolder = appFolder.Parent.Parent.Parent;
            var path = appFolder.FullName + @"\Clr\Test\Open.Core.Cloud.Test\TableStorage\CodeGeneration\Output\g\";
            var folder = new DirectoryInfo(path);
            if (!folder.Exists) folder.Create();
            return folder;
        }

        private static string GetApplicationPath()
        {
            const string prefix = @"file:\";
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Substring(prefix.Length, path.Length - prefix.Length);
            return path;
        }
    }
}
