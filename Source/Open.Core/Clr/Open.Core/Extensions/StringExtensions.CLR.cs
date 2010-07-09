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
using System.IO;
using System.Linq;
using System.Reflection;

namespace Open.Core.Common
{
    public static partial class StringExtensions
    {
        #region Methods
        /// <summary>Writes the given string to a file within the executing application's folder.</summary>
        /// <param name="content">The content to write.</param>
        /// <param name="folder">The path of the folder (starting at the name of the project folder).</param>
        /// <param name="fileName">The name of the file</param>
        public static void WriteToProjectFile(this string content, string folder, string fileName)
        {
            var folderInfo = GetFolder(folder);
            using (var writer = new StreamWriter(folderInfo.FullName + @"\" + fileName))
            {
                writer.Write(content);
            }
        }
        #endregion

        #region Internal
        public static DirectoryInfo GetFolder(string folderPath)
        {
            // Setup initial conditions.
            var backslash = @"\".ToCharArray();
            folderPath = folderPath.Replace("/", @"\");
            folderPath = folderPath.TrimStart(backslash);

            // Extract the project folder's name.
            var projectFolderName = folderPath.Split(backslash).FirstOrDefault();
            if (projectFolderName.IsNullOrEmpty(true)) throw new ArgumentOutOfRangeException("No root folder");
            projectFolderName = projectFolderName.ToLower();
            folderPath = folderPath.RemoveStart(projectFolderName);

            // Walk up from the BIN folder looking for the root project folder
            var appFolder = new DirectoryInfo(GetApplicationPath());
            do
            {
                var match = GetMatchingDirectory(appFolder, projectFolderName);
                if (match != null)
                {
                    appFolder = match;
                    break;
                }
                appFolder = appFolder.Parent;
            } while (appFolder != null);
            if (appFolder == null) throw new ArgumentOutOfRangeException(string.Format("A project folder named '{0}' could not be found.", projectFolderName));

            // Construct the folder-info and ensure the directory exists on disk.
            var path = appFolder.FullName + folderPath;
            var folderInfo = new DirectoryInfo(path);
            if (!folderInfo.Exists) folderInfo.Create();

            // Finish up.
            return folderInfo;
        }

        private static DirectoryInfo GetMatchingDirectory(DirectoryInfo start, string name)
        {
            if (start.Name.ToLower() == name) return start;
            foreach (var child in start.GetDirectories())
            {
                if (child.Name.ToLower() == name) return child;
                var next = GetMatchingDirectory(child, name);
                if (next != null) return next;
            }
            return null;
        }

        private static string GetApplicationPath()
        {
            const string prefix = @"file:\";
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Substring(prefix.Length, path.Length - prefix.Length);
            return path;
        }
        #endregion
    }
}
