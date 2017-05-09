using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace VisualiseR.Common
{
    public static class FileUtil
    {
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
        public static string GetDirectoryName([NotNull] string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// Returns the file name of the given file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName([NotNull] string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// Returns
        /// </summary>
        /// <param name="outputFormat"></param>
        /// <returns></returns>
        public static string GetPathWithExtension(string filePath, string outputFormat)
        {
            return GetDirectoryName(filePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filePath) + "." + outputFormat;
        }


    }
}