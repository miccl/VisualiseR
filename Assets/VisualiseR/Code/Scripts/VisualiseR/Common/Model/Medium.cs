using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    public class Medium : IMedium
    {
        public string Name { get; set; }
        public List<IPicture> Pictures { get; set; }

        public Medium(string name) : this(name, new List<IPicture>())
        {
        }

        public Medium(string name, List<IPicture> pictures)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (pictures == null) throw new ArgumentNullException("pictures");
            Name = name;
            Pictures = pictures;
        }


        public void AddPicture(IPicture picture)
        {
            Pictures.Add(picture);
        }

        public void RemovePicture(IPicture picture)
        {
            Pictures.Remove(picture);
        }
    }
}