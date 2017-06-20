using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for files.
    /// </summary>
    public static class FileUtil
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(FileUtil));
        
        public static readonly string FILE_PREFIX = "file:///";
        
        private static readonly List<string> ImageExtensions =
            new List<string> {".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG"};

        private static readonly List<string> CodeExtensions = new List<string> {".JAVA", ".CS", ".PY", ".C"};
        private static readonly List<string> PdfExtensions = new List<string> {".PDF"};


        /// <summary>
        /// Checks to see whether the file with the given path is a image file or not.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsImageFile([NotNull] string filePath)
        {
            return ImageExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        /// <summary>
        /// Checks to see whether the file with the given path is a code file or not.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsCodeFile([NotNull] string filePath)
        {
            return CodeExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        /// <summary>
        /// Checks to see whether the file with the given path is a pdf file or not.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsPdfFile([NotNull] string filePath)
        {
            return PdfExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        /// <summary>
        /// Returns the directory name of given file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetDirectoryPath([NotNull] string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// Returns the file name with extension of the given file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileNameWithExtension([NotNull] string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// Returns the file name without extension of the given file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// Returns path with the exentsion
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetPathWithExtension(string filePath, string fileExtension)
        {
            return GetDirectoryPath(filePath) + Path.DirectorySeparatorChar +
                   GetFileNameWithoutExtension(filePath) + "." + fileExtension;
        }

        /// <summary>
        /// Copies file to a directory.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destDir"></param>
        /// <returns></returns>
        [CanBeNull]
        public static string CopyFile(string sourceFilePath, string destDir)
        {
            CreateDirectoryIfNotExists(destDir);
            if (File.Exists(sourceFilePath))
            {
                var destFilePath = destDir + Path.DirectorySeparatorChar + GetFileNameWithExtension(sourceFilePath);
                if (!File.Exists(destFilePath))
                {
                    File.Copy(sourceFilePath, destFilePath);
                    Logger.InfoFormat("Copied file '{0}' to '{1}'", sourceFilePath, destFilePath);
                    return destFilePath;
                }
                else
                {
                    Logger.ErrorFormat("File '{0}' already exists", destFilePath);
                }
            }
            else
            {
                Logger.ErrorFormat("Source file '{0}' does not exist", sourceFilePath);
            }
            return null;
        }

        /// <summary>
        /// Copies a file in a directory.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destDir"></param>
        /// <returns></returns>
        public static string CopyFile(string sourceFilePath, DirectoryInfo destDir)
        {
            return CopyFile(sourceFilePath, destDir.FullName);
        }

        /// <summary>
        /// Creates directory.
        /// </summary>
        /// <param name="filePath"></param>
        private static void CreateDirectoryIfNotExists(string filePath)
        {
            string directoryPath = GetDirectoryPath(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Logger.InfoFormat("Created directory '{0}'", directoryPath);
            }
        }

        /// <summary>
        /// Creates file in specified path if it not exists.
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateFileIfNotExists(string filePath)
        {
            CreateDirectoryIfNotExists(filePath);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                Logger.InfoFormat("Created file '{0}'", filePath);
            }
        }

        /// <summary>
        /// Delete file with specified path if it exists.
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleleFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Logger.InfoFormat("Deleted file '{0}'", filePath);
            }
        }

        /// <summary>
        /// Move the specified file to the specified directory.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destDirectoryPath"></param>
        public static string MoveFile(string sourceFilePath, string destDirectoryPath)
        {
            DirectoryUtil.CreateDirectoryIfNotExists(destDirectoryPath);
            if (File.Exists(sourceFilePath))
            {
                var destFilePath = destDirectoryPath + Path.DirectorySeparatorChar +
                                   GetFileNameWithExtension(sourceFilePath);
                if (!File.Exists(destFilePath))
                {
                    Directory.Move(sourceFilePath, destFilePath);
                    Logger.InfoFormat("Moved file '{0}' to '{1}'", sourceFilePath, destFilePath);
                    return destFilePath;
                }
                else
                {
                    Logger.ErrorFormat("File '{0}' already exists", destFilePath);
                }
            }
            else
            {
                Logger.ErrorFormat("Source file '{0}' does not exist", sourceFilePath);
            }
            return null;
        }

        public static string GetSizeInBytes(string filePath)
        {
            var sizeInBytes = new FileInfo(filePath).Length;
            return String.Format("{0} Bytes", sizeInBytes);
        }

        public static string GetOwner(string filePath)
        {
            //TODO get last modifier
            return "Owner";
        }

        public static string GetLastModified(string filePath)
        {
            DateTime lastModified = File.GetLastWriteTime(filePath);

            return lastModified.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static void WriteFile(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }
    }
}