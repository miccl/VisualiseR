using System;
using JetBrains.Annotations;

namespace VisualiseR.Common
{
    public static class WebUtil
    {
        public static bool IsValidUrl([NotNull] string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}