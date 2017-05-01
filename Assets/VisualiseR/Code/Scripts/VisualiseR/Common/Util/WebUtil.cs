using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace VisualiseR.Common
{
    public static class WebUtil
    {
        public static bool IsValidUrl([NotNull] string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static string DownloadFileFromWeb(string url)
        {
            WWW www = new WWW(url);
            while (!www.isDone)
            {
            }

            string fileName = GetFileName(url);
            string fullPath = Application.persistentDataPath + fileName;
            File.WriteAllBytes(fullPath, www.bytes);

            return fullPath;
        }

        [CanBeNull]
        public static string GetFileName(string url)
        {
            Uri uri;
            Uri.TryCreate(url, UriKind.Absolute, out uri);
            return Path.GetFileName(uri.LocalPath);
        }
    }
}