using System;
using System.IO;

namespace VisualiseR.Common
{
    public class Picture : IPicture
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }

        public Picture(string title, string path)
        {
            Title = title;
            if (String.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            Path = path;
            if (!File.Exists(Path)) throw new FileNotFoundException(Path);
            CreationDate = File.GetCreationTime(Path);
        }
    }
}