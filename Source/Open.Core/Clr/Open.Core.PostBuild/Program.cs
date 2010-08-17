using System;
using System.Linq;

namespace Open.Core.PostBuild
{
    /// <summary>Executes classes decorated with the [OnPostBuild].</summary>
    /// <remarks>
    ///     To use copy the 'PostBuild.exe' to your output BIN directory.<br/>
    ///     Then in the post-build event insert:<br/>
    ///         $(TargetDir)PostBuild.exe<br/>
    /// 
    ///     Or alternatively if specifying a filter tag:
    ///         $(TargetDir)PostBuild.exe "MyTag"<br/>
    /// 
    ///     And finally, if you do not wish to have PostBuild.exe in your BIN directory:
    ///         $(TargetDir)PostBuild.exe "MyTag" "c:\MyPath\"<br/>
    /// 
    /// </remarks>
    class Program
    {
        /// <summary>Entry point to the application.</summary>
        /// <param name="args">
        /// Command Line Arguments:
        /// 0 - (string) Tag of the [OnPostBuild] attribute.  If not specified all classes with [OnPostBuild] are executed.
        /// 1 - Path to the folder containing the DLL's to examine (looking for classes decorated with [OnPostBuild] attribute).
        /// </param>
        static void Main(string[] args)
        {
            // Setup initial conditions.
            var tag = args.ElementAtOrDefault(0);
            var path = args.ElementAtOrDefault(1);
            var collection = new PostBuildCollection(tag, path);

            // Invoke each decorated class.
            foreach (var type in collection.Classes)
            {
                try
                {
                    Activator.CreateInstance(type);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }
    }
}
