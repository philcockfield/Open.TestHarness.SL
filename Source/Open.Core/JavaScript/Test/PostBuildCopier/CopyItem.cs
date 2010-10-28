using System.IO;

namespace PostBuildCopier
{
    public class CopyItem
    {
        #region Head
        public CopyItem(string copyFile, string copyToFolder)
        {
            CopyFile = new FileInfo(copyFile);
            CopyToFolder = new DirectoryInfo(copyToFolder);
            DestinationFile = new FileInfo(string.Format("{0}\\{1}", CopyToFolder.FullName, CopyFile.Name));
        }
        #endregion

        #region Properties
        public FileInfo CopyFile { get; private set; }
        public DirectoryInfo CopyToFolder { get; set; }
        public FileInfo DestinationFile { get; private set; }
        #endregion

        #region Methods
        public void Copy()
        {
            if (!DestinationFile.Directory.Exists) DestinationFile.Directory.Create();
            Delete(DestinationFile);
            CopyFile.CopyTo(DestinationFile.FullName, true);
        }
        #endregion

        #region Internal
        private static void Delete(FileSystemInfo file)
        {
            // Setup initial conditions.
            if (!file.Exists) return;
            var path = file.FullName;

            // Remove the read-only attribute.
            if (File.GetAttributes(path) == FileAttributes.ReadOnly) File.SetAttributes(path, FileAttributes.Normal);

            // Finish up.
            file.Delete();
        }
        #endregion
    }
}
