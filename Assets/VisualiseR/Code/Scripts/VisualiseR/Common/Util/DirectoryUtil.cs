
using System.IO;
using System.Linq;

namespace VisualiseR.Common
{
    public class DirectoryUtil
    {
        public static bool IsValidDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath) && !IsDirectoryEmpty(directoryPath);
        }


        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.GetFileSystemEntries(path).Any();
        }
    }
}