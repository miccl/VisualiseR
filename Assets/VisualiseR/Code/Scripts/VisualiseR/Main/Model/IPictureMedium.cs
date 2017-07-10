using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    /// <summary>
    /// Represents a <see cref="IMedium"/> for pictures.
    /// </summary>
    public interface IPictureMedium : IMedium
    {
        /// <summary>
        /// List of pictures.
        /// </summary>
        List<IPicture> Pictures { get; set; }

        /// <summary>
        /// Adds a single picture to the <see cref="Pictures"/>.
        /// </summary>
        /// <param name="picture"></param>
        void AddPicture(IPicture picture);

        /// <summary>
        /// Removes a single picture to the <see cref="Pictures"/>.
        /// </summary>
        /// <param name="picture"></param>
        void RemovePicture(IPicture picture);

        /// <summary>
        /// Returns the picture on the given position.
        /// Returns <c>null</c>, if the position is invalid.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        IPicture GetPicture(int pos);

        /// <summary>
        /// Returns <c>true</c>, if the picture is Empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}