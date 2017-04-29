using System.Collections.Generic;
using System.IO;

namespace VisualiseR.Common
{
    public static class FileUtil
    {
        public static readonly List<string> ImageExtensions =
            new List<string> {".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG"};

        public static readonly List<string> CodeExtensions = new List<string> {".JAVA", ".CS", ".PY", ".C", ".PNG"};
        public static readonly List<string> PdfExtensions = new List<string> {".PDF"};

        public static bool IsImageFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extension != null && ImageExtensions.Contains(extension.ToUpperInvariant());
        }

        public static bool IsCodeFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extension != null && CodeExtensions.Contains(extension.ToUpperInvariant());
        }


        public static bool IsPdfFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extension != null && PdfExtensions.Contains(extension.ToUpperInvariant());
        }
    }
}