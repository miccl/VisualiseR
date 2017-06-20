using System.Collections.Generic;
using System.IO;
using strange.extensions.command.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class LoadFilesCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(LoadFilesCommand));
        
        [Inject]
        public Player _player { get; set; }

        [Inject]
        public ISlideMedium SlideMedium { get; set; }

        [Inject]
        public FilesLoadedSignal FilesLoadedSignal { get; set; }

        public override void Execute()
        {
            if (_player.IsHost())
            {
                ISlideMedium medium = RetrieveFiles();
                List<byte[]> images = GetFileBytes(medium);
                FilesLoadedSignal.Dispatch((SlideMedium) medium, images);
                Logger.Info("Files loaded");
            }

        }

        private List<byte[]> GetFileBytes(ISlideMedium medium)
        {
            List<byte[]> images = new List<byte[]>();
            foreach (var slide in medium.Slides)
            {
                images.Add(loadFile(slide.Pic.Path));
            }
            return images;
        }

        private byte[] loadFile(string filePath)
        {
            WWW www = new WWW(FileUtil.FILE_PREFIX + filePath);
            var result = www.bytes;
            www.Dispose();
            return result;
        }

        private ISlideMedium RetrieveFiles()
        {
            object o = PlayerPrefsUtil.RetrieveObject(PlayerPrefsUtil.ROOM_KEY);
            if (o != null)
            {
                Common.Room room = (Common.Room) o;
                IPictureMedium PictureMedium = (IPictureMedium) room.Medium;
                ConstructSlideMedium(PictureMedium);
            }
            else
            {
                CreateMockMedium();
            }
            return SlideMedium;
        }

        private void ConstructSlideMedium(IPictureMedium medium)
        {
            SlideMedium.Name = medium.Name;
            foreach (var pic in medium.Pictures)
            {
                ISlide slide = new Slide();
                slide.Name = pic.Title;
                slide.Pic = pic;
                SlideMedium.AddSlide(slide);
            }
        }

        private void CreateMockMedium()
        {
            SlideMedium.Name = "Test"                ;

            for (int i = 0; i < 3; i++)
            {
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                SlideMedium.AddSlide(new Slide
                {
                    Name = pic,
                    Pic = new Picture
                    {
                        Title = pic,
                        Path = filePath
                    }
                });
                Debug.Log(filePath);
            }
        }
    }
}