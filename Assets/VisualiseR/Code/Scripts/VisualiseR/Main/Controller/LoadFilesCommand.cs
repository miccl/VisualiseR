using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using UnityEditor;
using VisualiseR.Common;
using VisualiseR.Util;
using FileUtil = VisualiseR.Util.FileUtil;

namespace VisualiseR.Main
{
    /// <summary>
    /// Command to load the files from disk.
    /// Creates a  <see cref="IPictureMedium"/> model.
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

        [Inject]
        public FileType fileType { get; set; }

        public override void Execute()
        {
            List<string> filePaths = new List<string>();
            var isValidInput = LoadFiles(filePaths);
            if (!isValidInput)
            {
                _MediumChangedSignal.Dispatch(null);
                return;
            }
            var medium = ConstructMedium(GetName(filePaths), filePaths);

            _MediumChangedSignal.Dispatch((PictureMedium) medium);
        }

        private string GetName(List<string> filePaths)
        {
            if (fileType.Equals(FileType.Disk))
            {
                return Path.GetFileNameWithoutExtension(uri);
            }

            return FileUtil.GetFileNameWithoutExtension(filePaths[0]);
        }

        private bool LoadFiles(List<string> filePaths)
        {
            switch (fileType)
            {
                case FileType.Web:
                    return LoadWebFile(filePaths);
                default:
                    return LoadDiskFile(filePaths);
            }
        }

        private bool LoadDiskFile(List<string> filePaths)
        {
            if (!DirectoryUtil.IsValidNotEmptyDirectory(uri))
            {
                return false;
            }
            TraverseFilesAndConvert(filePaths);
            return true;

        }

        private bool LoadWebFile(List<string> filePaths)
        {
            if (!IsValidWebFile())
            {
                return false;
            }
            filePaths.Add(WebUtil.DownloadFileFromWeb(uri));
            return true;
        }

        private bool IsValidWebFile()
        {
            if (String.IsNullOrEmpty(uri))
            {
                string errorMessage = "Url was empty";
                Logger.Info("Error:" + errorMessage);
                ShowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }
            
            if (!WebUtil.IsValidUrl(uri))
            {
                string errorMessage = "Url is invalid";
                Logger.Info("Error:" + errorMessage);
                ShowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }

            return true;
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
        /// Valid file types are image (<see cref="Util.FileUtil.ImageExtensions"/>), code (<see cref="Util.FileUtil.CodeExtensions"/>)
        /// and pdf (<see cref="Util.FileUtil.PdfExtensions"/>).
        /// Otherwise <c>null</c> is returned.
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
                //TODO convert
            }

            if (FileUtil.IsPdfFile(filePath))
            {
                //TODO convert
            }

            Logger.WarnFormat("File ({0}) has no valid type and therefore could not be converted.", filePath);
            return null;
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