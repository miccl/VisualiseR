using System.Collections.Generic;

namespace VisualiseR.Common
{
    public abstract class ImageConversionStrategy
    {
        public abstract string Convert(string filePath);
    }
}