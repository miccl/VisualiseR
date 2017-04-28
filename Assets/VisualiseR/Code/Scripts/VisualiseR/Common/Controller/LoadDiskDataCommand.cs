using System.IO;
using strange.extensions.command.impl;
using VisualiseR.Code.Scripts.VisualiseR.CodeReview.Controller;

namespace VisualiseR.Common
{
    /// <summary>
    /// Loads the files from Disk and creates medium model.
    /// </summary>
    public class LoadDiskDataCommand : Command
    {
        [Inject]
        public MediumChangedSignal _MediumChangedSignal { get; set; }

        [Inject]
        public string directoryPath { get; set; }

        public override void Execute()
        {
            if (!DirectoryUtil.IsDirectoryEmpty(directoryPath))
            {
                //TODO implement error message
                throw new FileNotFoundException(directoryPath);
            }

            Medium medium = new Medium(Path.GetFileNameWithoutExtension(directoryPath));
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                Picture picture = new Picture(Path.GetFileNameWithoutExtension(filePath), filePath);
                medium.AddPicture(picture);
            }

            _MediumChangedSignal.Dispatch(medium);
        }

    }
}