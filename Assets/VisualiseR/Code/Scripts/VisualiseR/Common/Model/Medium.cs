using System.Collections.Generic;
using System.Linq;

namespace VisualiseR.Common
{
    public class Medium : IMedium
    {
        public string Name { get; set; }

        public List<IPicture> Pictures { get; set; }

        public Medium()
        {
            Pictures = new List<IPicture>();
        }

        public void AddPicture(IPicture picture)
        {
            Pictures.Add(picture);
        }

        public void RemovePicture(IPicture picture)
        {
            Pictures.Remove(picture);
        }

        public IPicture GetPicture(int pos)
        {
            return Pictures.ElementAt(pos);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Pictures: {1}", Name, Pictures);
        }
    }
}