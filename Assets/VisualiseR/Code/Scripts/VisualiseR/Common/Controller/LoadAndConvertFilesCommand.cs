using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using strange.extensions.command.impl;

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
        public string uri { get; set; }

        private ImageConversionStrategy _conversion;

        public override void Execute()
        {
            if (!DirectoryUtil.IsValidDirectory(uri) && !WebUtil.IsValidUrl(uri))
            {
                //TODO implement error message
                throw new FileNotFoundException(uri);
            }

            List<string> filePaths = new List<string>();

            if (WebUtil.IsValidUrl(uri))
            {
                filePaths.Add(WebUtil.DownloadFileFromWeb(uri));
            }
            else
            {
                TraverseFilesAndConvert(filePaths);
            }


            var medium = ConstructMedium(Path.GetFileNameWithoutExtension(uri), filePaths);

            _MediumChangedSignal.Dispatch(medium);
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
    }
}