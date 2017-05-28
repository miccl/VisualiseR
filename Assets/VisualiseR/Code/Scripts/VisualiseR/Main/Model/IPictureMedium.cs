using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public interface IPictureMedium : IMedium
    {
        List<IPicture> Pictures { get; set; }

        void AddPicture(IPicture picture);

        void RemovePicture(IPicture picture);

        IPicture GetPicture(int pos);

        bool IsEmpty();
    }
}