using System;
using System.IO;
using JetBrains.Annotations;

namespace VisualiseR.Common
{
    public class Picture : IPicture
    {
        public string Title { get; set; }
        public string Path { get; set; }

        public Picture([NotNull] string title,[NotNull] string path)
        {
            Title = title;
            Path = path;
        }
    }
}