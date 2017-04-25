using System;

namespace VisualiseR.Common
{
    public interface IPicture
    {
        string Title { get; set; }
        DateTime CreationDate { get; set; }
        string UriString { get; set; }

    }
}