using System.IO;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class PresentationStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(PresentationStartCommand));

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        [Inject]
        public ISlideMedium _slideMedium { get; set; }

        [Inject]
        public IPlayer _player { get; set; }


        public override void Execute()
        {
            InitPlayer();
            InitView();
        }


        private void InitView()
        {
            InitScreen();
        }

        private void InitPlayer()
        {
            //TODO ordentlich player erzeugen
            _player.Name = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            _player.Type = GetPlayerType();
        }

        private PlayerType GetPlayerType()
        {
            if (PhotonNetwork.isMasterClient)
            {
                return PlayerType.Host;
            }
            return PlayerType.Client;
        }

        private void InitScreen()
        {
            var screen = InstantiateScreen();

            PresentationScreenView screenView = screen.GetComponent<PresentationScreenView>();
            InitScreenView(screenView);
        }

        private GameObject InstantiateScreen()
        {
            GameObject stand = UnityUtil.FindGameObject("Presentation_Screen");
            return stand;
        }

        private void InitScreenView(PresentationScreenView view)
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
                _slideMedium = CreateMockMedium();
            }
            view.Init(_slideMedium, _player);
        }

        private void ConstructSlideMedium(IPictureMedium medium)
        {
            _slideMedium.Name = medium.Name;
            foreach (var pic in medium.Pictures)
            {
                ISlide slide = new Slide();
                slide.Name = pic.Title;
                slide.Pic = pic;
                _slideMedium.AddSlide(slide);
            }
        }

        private ISlideMedium CreateMockMedium()
        {
            ISlideMedium medium = new SlideMedium()
            {
                Name = "Test"
            };

            for (int i = 0; i < 3; i++)
            {
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                medium.AddSlide(new Slide
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

            return medium;
        }
    }
}