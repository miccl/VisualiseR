using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using UnityEngine;
using VisualiseR.CodeReview;

namespace VisualiseR.Common
{
    /// <summary>
    /// Loads the files from Disk and creates _medium model.
    /// </summary>
    public class LoadDiskDataCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(LoadDiskDataCommand));

        [Inject]
        public MediumChangedSignal _MediumChangedSignal { get; set; }

        [Inject]
        public string _directoryPath { get; set; }

        private ImageConversionStrategy _conversion;

        public override void Execute()
        {
//            if (!DirectoryUtil.IsValidDirectory(_directoryPath))
//            {
//                //TODO implement error message
//                throw new FileNotFoundException(_directoryPath);
//            }

            List<string> convertedFilePaths = new List<string>();

            if (WebUtil.CheckUrlValid(_directoryPath))
            {
                convertedFilePaths.Add(DownloadWeb(_directoryPath));
            }
            else
            {
                foreach (var filePath in Directory.GetFiles(_directoryPath))
                {
                    var convertedFilePath = ConvertFile(filePath);
                    if (convertedFilePath != null)
                    {
                        convertedFilePaths.Add(convertedFilePath);
                    }
                }
            }



            var medium = ConstructMedium(Path.GetFileNameWithoutExtension(_directoryPath), convertedFilePaths);

//            var _medium = ConstructMedium(_directoryPath);

            _MediumChangedSignal.Dispatch(medium);
        }

        /// <summary>
        /// Converts the file with the given file path.
        /// Valid file types are image (<see cref="FileUtil.ImageExtensions"/>), code (<see cref="FileUtil.CodeExtensions"/>)
        /// and pdf (<see cref="FileUtil.PdfExtensions"/>).
        /// Otherwise <code>null</code> is returned.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [CanBeNull]
        private string ConvertFile([NotNull] string filePath)
        {
            if (FileUtil.IsImageFile(filePath))
            {
                Logger.InfoFormat("ImageFile added: {0}", filePath);
                return filePath;
            }

            if (FileUtil.IsCodeFile(filePath))
            {
                //TODO
//                return Convert(filePath, new ConvertCodeToJpeg());
            }

            if (FileUtil.IsPdfFile(filePath))
            {
                //TODO
//                return Convert(filePath, new ConvertPdfToJpeg());
            }

            Logger.WarnFormat("File ({0}) has no valid type and therefore could not be converted.", filePath);
            return null;
        }

        private string Convert([NotNull] string filePath, [NotNull] ImageConversionStrategy strategy)
        {
            return strategy.Convert(filePath);
        }

        private Medium ConstructMedium(string name, List<string> filePaths)
        {
            Medium medium = new Medium(Path.GetFileNameWithoutExtension(name));
            foreach (var filePath in filePaths)
            {
                Picture picture = new Picture(Path.GetFileNameWithoutExtension(filePath), filePath);
                medium.AddPicture(picture);
            }
            return medium;
        }


        private IEnumerator TestDownload(string webPath)
        {
            WWW www = new WWW(webPath);
            //yield return www;
//            string progress;
//            while (!www.isDone) {
//                progress = "downloaded " + (www.progress*100).ToString() + "%...";
//                yield return null;
//            }
//
            string fullPath = Application.persistentDataPath + "/test.jpg";
            File.WriteAllBytes (fullPath, www.bytes);

//            progress = "downloaded, unzipping...";
            yield return fullPath;
        }

        private string DownloadWeb(string webPath)
        {
            WebClient client = new WebClient();
            string fullPath = Application.persistentDataPath + "/test.png";
//            string fullPath = "C:/test.png";
            //TODO das dateiformat auslesen...
            client.DownloadFile(webPath, fullPath);
            return fullPath;
        }

    }
}