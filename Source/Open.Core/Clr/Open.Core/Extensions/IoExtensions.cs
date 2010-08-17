using System;
using System.IO;

namespace Open.Core.Common
{
    public static class IoExtensions
    {
        /// <summary>Copies files that match the specified pattern to the target directory.</summary>
        /// <param name="sourceFolder">The source folder to copy from.</param>
        /// <param name="targetFolder">The target folder to copy to.</param>
        /// <param name="pattern">The file name pattern to select the files to copy with.</param>
        /// <param name="overwrite">Flag indicating if existing files should be overwritten.</param>
        public static void CopyFiles(this DirectoryInfo sourceFolder, string targetFolder, string pattern, bool overwrite = true)
        {
            sourceFolder.CopyFiles(new DirectoryInfo(targetFolder), pattern);
        }

        /// <summary>Copies files that match the specified pattern to the target directory.</summary>
        /// <param name="sourceFolder">The source folder to copy from.</param>
        /// <param name="targetFolder">The target folder to copy to.</param>
        /// <param name="pattern">The file name pattern to select the files to copy with.</param>
        /// <param name="overwrite">Flag indicating if existing files should be overwritten.</param>
        public static void CopyFiles(this DirectoryInfo sourceFolder, DirectoryInfo targetFolder, string pattern, bool overwrite = true)
        {
            // Setup initial conditions.
            if (sourceFolder == null) throw new ArgumentNullException("sourceFolder");
            if (targetFolder == null) throw new ArgumentNullException("targetFolder");
            if (!sourceFolder.Exists) return;
            if (!targetFolder.Exists) targetFolder.Create();

            // Copy files.
            foreach (var file in sourceFolder.GetFiles(pattern))
            {
                file.CopyTo(targetFolder.FullName + file.Name, overwrite);
            }
        }
    }
}
