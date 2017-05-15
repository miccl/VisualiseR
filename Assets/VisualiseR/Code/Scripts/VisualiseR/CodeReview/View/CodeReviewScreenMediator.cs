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
        public CodePositionChangedSignal _CodePositionChangedSignal { get; set; }

        [Inject]
        public NextCodeSignal nextCodeSignal { get; set; }

        [Inject]
        public PrevCodeSignal prevCodeSignal { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }

        [Inject]
        public IPictureMedium PictureMedium { get; set; }

        [Inject]
        public ICodeMedium CodeMedium { get; set; }


        public override void OnRegister()
        {
            _CodePositionChangedSignal.AddListener(OnCodePositionChanged);
            ContextMenuCanceledSignal.AddListener(OnContextMenuCanceled);
            _view.NextCodeSignal.AddListener(OnNextCodeSignal);
            _view.PrevCodeSignal.AddListener(OnPrevCodeSignal);

            InitView();
        }

        public override void OnRemove()
        {
            _CodePositionChangedSignal.RemoveListener(OnCodePositionChanged);
            ContextMenuCanceledSignal.RemoveListener(OnContextMenuCanceled);
            _view.NextCodeSignal.RemoveListener(OnNextCodeSignal);
            _view.NextCodeSignal.RemoveListener(OnPrevCodeSignal);
        }

        private void InitView()
        {
            object o = PlayerPrefsUtil.RetrieveObject(PlayerPrefsUtil.ROOM_KEY);
            if (o != null)
            {
                Room room = (Room) o;
                PictureMedium = room.Medium;
                ConstructCodeMedium(PictureMedium);
            }
            else
            {
                CodeMedium = CreateMockMedium();
            }

            OnMediumChanged((CodeMedium) CodeMedium);
        }

        private void ConstructCodeMedium(IPictureMedium medium)
        {
            CodeMedium.Name = medium.Name;
            foreach (var pic in medium.Pictures)
            {
                ICode code = new Code();
                code.Name = pic.Title;
                code.Pic = pic;
                CodeMedium.AddCodeFragment(code);
            }
        }

        public void OnMediumChanged(CodeMedium medium)
        {
            _view._medium = medium;
            _view.SetupMedium();
        }

        private void OnNextCodeSignal(IPlayer player, ICodeMedium medium, int pos)
        {
            nextCodeSignal.Dispatch((Player) player, (CodeMedium) medium, pos);
        }

        private void OnPrevCodeSignal(IPlayer player, ICodeMedium medium, int pos)
        {
            prevCodeSignal.Dispatch((Player) player, (CodeMedium) medium, pos);
        }

        private void OnCodePositionChanged(int pos)
        {
            _view._currPicturePos = pos;
            _view.LoadPictureIntoTexture(pos);
        }

        private void OnContextMenuCanceled()
        {
            _view.IsContextMenuShown = false;
        }

        private ICodeMedium CreateMockMedium()
        {
            ICodeMedium medium = new CodeMedium
            {
                Name = "test"
            };

            for (int i = 0; i < 3; i++)
            {
                ICode code = new Code();
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                code.Pic = new Picture
                {
                    Title = pic,
                    Path = filePath
                };
                medium.AddCodeFragment(code);
            }
            return medium;
        }
    }
}