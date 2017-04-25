
using System;
using System.IO;

namespace VisualiseR.Common {

    public class Picture : IPicture {

        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string UriString { get; set; }

        public Picture(string title, string uriString)
        {
            Title = title;
            UriString = uriString;
            if (!File.Exists(uriString))
            {
                throw new FileNotFoundException(uriString);
            }
            CreationDate = File.GetCreationTime(UriString);
        }
    }

}

