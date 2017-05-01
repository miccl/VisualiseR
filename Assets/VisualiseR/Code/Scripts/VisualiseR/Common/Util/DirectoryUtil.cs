
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


        public static bool IsDirectoryEmpty(string directoryPath)
        {
            return !Directory.GetFileSystemEntries(directoryPath).Any();
        }
    }
}