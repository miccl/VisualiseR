using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PictureMedium : IPictureMedium
    {
        /// <summary>
        /// Name of pictures.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// List of pictures.
        /// </summary>
        public List<IPicture> Pictures { get; set; }

        /// <summary>
        /// Ctor for <see cref="PictureMedium"/>.
        /// </summary>
        public PictureMedium()
        {
            Pictures = new List<IPicture>();
        }

        /// <summary>
        /// Adds a single picture to the <see cref="Pictures"/>.
        /// </summary>
        /// <param name="picture"></param>
        public void AddPicture(IPicture picture)
        {
            Pictures.Add(picture);
        }

        /// <summary>
        /// Removes a single picture to the <see cref="Pictures"/>.
        /// </summary>
        /// <param name="picture"></param>
        public bool RemovePicture(IPicture picture)
        {
            return Pictures.Remove(picture);
        }

        public IPicture GetPicture(int pos)
        {
            return Pictures.ElementAt(pos);
        }

        /// <summary>
        /// Returns the picture on the given position.
        /// Returns <c>null</c>, if the position is invalid.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("PictureMedium[Name: {0}, Pictures: [", Name);
            foreach (IPicture pic in Pictures)
            {
                sb.Append(pic.ToString() + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }
        
        /// <summary>
        /// Returns <c>true</c>, if the picture is Empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || Pictures.Count == 0;
        }
    }
}