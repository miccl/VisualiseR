using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using VisualiseR.CodeReview;
using VisualiseR.Main;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for working with directories.
    /// </summary>
    public static class DirectoryUtil
    {
        /// <summary>
        /// Returns <c>true</c> if the directory of the given path is valid and not empty.
        /// </summary>
        /// <param name="directoryPath">full path of the directory.</param>
        /// <returns><c>true</c> if the directory of the given path is valid and not empty.</returns>
        public static bool IsValidNotEmptyDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath) && !IsDirectoryEmpty(directoryPath);
        }

        /// <summary>
        /// Returns <c>true</c> if the directory is empty.
        /// </summary>
        /// <param name="directoryPath">path of the directory</param>
        /// <returns></returns>
        public static bool IsDirectoryEmpty(string directoryPath)
        {
            return !Directory.GetFileSystemEntries(directoryPath).Any();
        }

        /// <summary>
        /// Creates a directory if it not already exists.
        /// </summary>
        /// <param name="directoryPath"><full path of the directory</param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Directory.CreateDirectory(directoryPath);
            }
            return new DirectoryInfo(directoryPath);
        }

        /// <summary>
        /// Creates all directories needed for 
        /// Returns directory of unrated.
        /// </summary>
        /// <param name="mediumName"></param>
        /// <param name="mainDir"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectorysForCodeReview(string mainDir)
        {
            var mainDirInfo = CreateDirectoryIfNotExists(mainDir);
            foreach(var rate in EnumUtil.GetValues<Rate>())
            {
                CreateDirectoryIfNotExists(mainDirInfo.FullName + Path.DirectorySeparatorChar + rate);
            }
            return mainDirInfo;
        }

        /// <summary>
        /// Get the directory of a specific rating.
        /// </summary>
        /// <param name="mainDir"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static DirectoryInfo GetRatingDirectory(string mainDir, Rate rate)
        {
            var dirPath = mainDir + Path.DirectorySeparatorChar + rate;
            return new DirectoryInfo(dirPath);
        }

        /// <summary>
        /// Returns the parent directory path.
        /// Returns <c>null</c>, if the directory doesn't exist.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        [CanBeNull]
        public static string GetParentDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return null;
            }
            return Directory.GetParent(dirPath).FullName;
        }

        /// <summary>
        /// Deletes directory.
        /// </summary>
        /// <param name="dirPath">path of the directory</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void DeleteDirectory(string dirPath)
        {
            Directory.Delete(dirPath);
        }
    }
}