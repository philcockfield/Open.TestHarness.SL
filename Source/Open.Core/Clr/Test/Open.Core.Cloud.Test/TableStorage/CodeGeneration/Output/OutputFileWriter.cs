using Open.Core.Common;

namespace Open.Core.Cloud.Test.TableStorage
{
    public static class OutputFileWriter
    {
        public static void Write(string fileName, string code)
        {
            code.WriteToProjectFile(@"\Open.Core.Cloud.Test\TableStorage\CodeGeneration\Output\g\", fileName);
        }
    }
}
