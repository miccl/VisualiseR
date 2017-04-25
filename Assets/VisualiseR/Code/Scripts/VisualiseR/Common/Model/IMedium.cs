using System.Collections.Generic;

namespace VisualiseR.Common
{
    public interface IMedium
    {
        string Name { get; set; }
        List<IPicture> Pictures { get; set; }

        void AddPicture(IPicture picture);

        void RemovePicture(IPicture picture);
    }
}