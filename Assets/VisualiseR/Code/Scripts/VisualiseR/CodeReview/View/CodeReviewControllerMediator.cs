using System.IO;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewControllerMediator : Mediator
    {
        [Inject]
        public CodeReviewControllerView View { get; set; }

        [Inject]
        public CodePositionChangedSignal CodePositionChangedSignal { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }

        [Inject]
        public PrevCodeSignal PrevCodeSignal { get; set; }

        [Inject]
        public IPictureMedium PictureMedium { get; set; }

        [Inject]
        public ICodeMedium CodeMedium { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }



        public override void OnRegister()
        {
            NextCodeSignal.AddListener(OnNextCodeSignal);
            PrevCodeSignal.AddListener(OnPrevCodeSignal);
            CodePositionChangedSignal.AddListener(OnCodePositionChanged);

            InitView();
        }

        public override void OnRemove()
        {
            NextCodeSignal.RemoveListener(OnNextCodeSignal);
            PrevCodeSignal.RemoveListener(OnPrevCodeSignal);
            CodePositionChangedSignal.RemoveListener(OnCodePositionChanged);
        }

        private void InitView()
        {
            View._contextView = contextView;

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

        public void OnMediumChanged(CodeMedium medium)
        {
            View._medium = medium;
            View.SetupMedium();
        }

        private void OnCodePositionChanged(int pos)
        {
            View._currCodePos = pos;
            //TODO
//            _view.LoadPictureIntoTexture(pos);
        }

        private void OnNextCodeSignal(IPlayer player, ICodeMedium medium, int pos)
        {
            NextCodeSignal.Dispatch((Player) player, (CodeMedium) medium, pos);
        }

        private void OnPrevCodeSignal(IPlayer player, ICodeMedium medium, int pos)
        {
            PrevCodeSignal.Dispatch((Player) player, (CodeMedium) medium, pos);
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