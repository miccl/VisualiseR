using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for web.
    /// </summary>
    public static class WebUtil
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(WebUtil));

        /// <summary>
        /// Returns <c>true</c> if url is valid.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl([NotNull] string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Downloads the file from web.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DownloadFileFromWeb(string url)
        {
            WWW www = new WWW(url);
            while (!www.isDone)
            {
                //TODO show progess?!
            }

            string fileName = GetFileName(url);
            string fullPath = Application.persistentDataPath + fileName;
            File.WriteAllBytes(fullPath, www.bytes);

            Logger.InfoFormat("Created file from web: {0}", fileName);

            return fullPath;
        }

        /// <summary>
        /// Returns the file name of the file.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [CanBeNull]
        private static string GetFileName(string url)
        {
            Uri uri;
            Uri.TryCreate(url, UriKind.Absolute, out uri);
            return Path.GetFileName(uri.LocalPath);
        }
    }
}