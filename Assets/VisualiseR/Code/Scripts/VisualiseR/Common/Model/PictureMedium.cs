using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualiseR.Common
{
    [Serializable]
    public class PictureMedium : IPictureMedium
    {
        public string Name { get; set; }

        public List<IPicture> Pictures { get; set; }

        public PictureMedium()
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("PictureMedium[Name: {0}, Pictures: [", Name);
            foreach (IPicture pic in Pictures)
            {
                sb.Append(pic.ToString() + ", ");
            }
            sb.Append("]");
            return string.Format("Name: {0}, Pictures: {1}", Name, Pictures);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || Pictures.Count == 0;
        }
    }
}