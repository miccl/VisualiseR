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
        public string _path { get; set; }

        public override void Execute()
        {
            if (!Directory.Exists(_path) && FileUtil.IsDirectoryEmpty(_path))
            {
                //TODO implement error message
                throw new FileNotFoundException(_path);
            }

            Medium medium = new Medium(Path.GetFileNameWithoutExtension(_path));
            foreach (var filePath in Directory.GetFiles(_path))
            {
                Picture picture = new Picture(Path.GetFileNameWithoutExtension(filePath), filePath);
                medium.AddPicture(picture);
            }

            _MediumChangedSignal.Dispatch(medium);
        }
    }
}