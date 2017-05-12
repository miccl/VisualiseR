using System;

namespace VisualiseR.Common
{
    public interface IPicture
    {
        string Title { get; set; }
        string Path { get; set; }

        bool IsEmpty();

        string ToString();

    }
}