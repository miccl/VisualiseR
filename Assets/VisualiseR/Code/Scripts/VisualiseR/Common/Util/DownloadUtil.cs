using System;

namespace VisualiseR.Common
{
    public static class DownloadUtil
    {
        public static bool CheckUrlValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }
    }
}