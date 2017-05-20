﻿using System.IO;
using System.Linq;
using VisualiseR.CodeReview;

namespace VisualiseR.Common
{
    public static class DirectoryUtil
    {
        public static bool IsValidNotEmptyDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath) && !IsDirectoryEmpty(directoryPath);
        }


        public static bool IsDirectoryEmpty(string directoryPath)
        {
            return !Directory.GetFileSystemEntries(directoryPath).Any();
        }


        public static DirectoryInfo CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Directory.CreateDirectory(directoryPath);
            }
            return new DirectoryInfo(directoryPath);
        }

        /// <summary>
        /// Returns directory of unrated.
        /// </summary>
        /// <param name="mediumName"></param>
        /// <param name="mainDir"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectorysForCodeReview(string mainDir)
        {
            var mainDirInfo = CreateDirectoryIfNotExists(mainDir);
            var unratedDir = CreateDirectoryIfNotExists(mainDirInfo.FullName + Path.DirectorySeparatorChar +
                                                        Rate.Unrated);
            var uncriticalDir = CreateDirectoryIfNotExists(mainDirInfo.FullName + Path.DirectorySeparatorChar +
                                                           Rate.Uncritical);
            var minorDir = CreateDirectoryIfNotExists(mainDirInfo.FullName + Path.DirectorySeparatorChar + Rate.Minor);
            var CriticalDir = CreateDirectoryIfNotExists(mainDirInfo.FullName + Path.DirectorySeparatorChar +
                                                         Rate.Critical);
            return mainDirInfo;
        }

        public static DirectoryInfo GetRatingDirectory(string mainDir, Rate rate)
        {
            var dirPath = mainDir + Path.DirectorySeparatorChar + rate;
            return new DirectoryInfo(dirPath);
        }
    }
}