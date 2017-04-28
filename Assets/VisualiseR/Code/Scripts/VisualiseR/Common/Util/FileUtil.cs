
using System.IO;
using System.Linq;

namespace VisualiseR.Common
{
    public class FileUtil
    {
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.GetFileSystemEntries(path).Any();
        }
    }
}