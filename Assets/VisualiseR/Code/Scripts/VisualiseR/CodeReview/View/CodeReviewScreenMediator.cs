using System.IO;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenMediator : Mediator
    {
        [Inject]
        public CodeReviewScreenView _view { get; set; }

        [Inject]
        public MediumChangedSignal mediumChangedSignal { get; set; }

        [Inject]
        public CodePositionChangedSignal _CodePositionChangedSignal { get; set; }

        [Inject]
        public NextCodeSignal nextCodeSignal { get; set; }

        [Inject]
        public PrevCodeSignal prevCodeSignal { get; set; }

        [Inject]
        public IPictureMedium Medium { get; set; }


        public override void OnRegister()
        {
            mediumChangedSignal.AddListener(OnMediumChanged);
            _CodePositionChangedSignal.AddListener(OnCodePositionChanged);
            _view.NextCodeSignal.AddListener(OnNextCodeSignal);
            _view.PrevCodeSignal.AddListener(OnPrevCodeSignal);

            InitView();
        }

        private void InitView()
        {
            object o = PlayerPrefsUtil.RetrieveObject(PlayerPrefsUtil.ROOM_KEY);
            if (o != null)
            {
                Room room = (Room) o;
                Medium = room.Medium;
            }
            else
            {
                Medium = CreateMockMedium();
            }

            OnMediumChanged((PictureMedium) Medium);
        }

        public override void OnRemove()
        {
            mediumChangedSignal.RemoveListener(OnMediumChanged);
            _CodePositionChangedSignal.RemoveListener(OnCodePositionChanged);
            _view.NextCodeSignal.RemoveListener(OnNextCodeSignal);
            _view.NextCodeSignal.RemoveListener(OnPrevCodeSignal);
        }

        public void OnMediumChanged(PictureMedium pictureMedium)
        {
            _view._medium = pictureMedium;
            _view.SetupMedium();
        }

        private void OnNextCodeSignal(IPlayer player, IPictureMedium medium, int pos)
        {
            nextCodeSignal.Dispatch((Player) player, (PictureMedium) medium, pos);
        }

        private void OnPrevCodeSignal(IPlayer player, IPictureMedium medium, int pos)
        {
            prevCodeSignal.Dispatch((Player) player, (PictureMedium) medium, pos);
        }

        private void OnCodePositionChanged(int pos)
        {
            Debug.Log("AHUUU");
            _view._currPicturePos = pos;
            _view.LoadPictureIntoTexture(pos);
        }

        private IPictureMedium CreateMockMedium()
        {
            IPictureMedium medium = new PictureMedium
            {
                Name = "test"
            };

            for (int i = 0; i < 3; i++)
            {
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                medium.AddPicture(new Picture
                {
                    Title = pic,
                    Path = filePath
                });
            }
            return medium;
        }
    }
}