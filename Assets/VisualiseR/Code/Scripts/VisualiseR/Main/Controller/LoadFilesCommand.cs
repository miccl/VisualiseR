using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// Loads the files from Disk and creates _medium model.
    /// </summary>
    public class LoadFilesCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(LoadFilesCommand));

        [Inject]
        public MediumChangedSignal _MediumChangedSignal { get; set; }

        [Inject]
        public ShowMessageSignal ShowMessageSignal { get; set; }

        [Inject]
        public string uri { get; set; }

        public override void Execute()
        {
            CheckIfValidFile();

            List<string> filePaths = new List<string>();
            LoadFiles(filePaths);
            var medium = ConstructMedium(Path.GetFileNameWithoutExtension(uri), filePaths);

            _MediumChangedSignal.Dispatch((PictureMedium) medium);
        }

        private void LoadFiles(List<string> filePaths)
        {
            if (WebUtil.IsValidUrl(uri))
            {
                filePaths.Add(WebUtil.DownloadFileFromWeb(uri));
            }
            else
            {
                TraverseFilesAndConvert(filePaths);
            }
        }

        private void CheckIfValidFile()
        {
            if (!DirectoryUtil.IsValidNotEmptyDirectory(uri) && !WebUtil.IsValidUrl(uri))
            {
                ShowMessageSignal.Dispatch(new Message(MessageType.Error, "Invalid Input",
                    string.Format("The chosen uri '{0}'file was invalid", uri)));
                throw new FileNotFoundException(uri);
            }
        }

        private void TraverseFilesAndConvert(List<string> convertedFilePaths)
        {
            foreach (var filePath in Directory.GetFiles(uri))
            {
                var convertedFilePath = ConvertFile(filePath);
                if (convertedFilePath != null)
                {
                    convertedFilePaths.Add(convertedFilePath);
                }
            }
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

        private IPictureMedium ConstructMedium(string name, List<string> filePaths)
        {
            IPictureMedium medium = new PictureMedium
            {
                Name = Path.GetFileNameWithoutExtension(name)
            };
            foreach (var filePath in filePaths)
            {
                IPicture picture = new Picture
                {
                    Title = Path.GetFileNameWithoutExtension(filePath),
                    Path = filePath
                };
                medium.AddPicture(picture);
            }
            return medium;
        }
    }
}