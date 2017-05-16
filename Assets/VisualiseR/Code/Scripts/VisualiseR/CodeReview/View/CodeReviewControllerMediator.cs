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
        public RemoveCodeSignal RemoveCodeSignal { get; set; }

        [Inject]
        public IPictureMedium PictureMedium { get; set; }

        [Inject]
        public ICodeMedium CodeMedium { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }


        public override void OnRegister()
        {
            NextCodeSignal.AddListener(OnNextCode);
            RemoveCodeSignal.AddListener(OnRemoveCode);
            CodePositionChangedSignal.AddListener(OnCodePositionChanged);

            InitView();
        }

        private void OnNextCode(Code code)
        {
            View.NextCode(code);

        }

        public override void OnRemove()
        {
            RemoveCodeSignal.RemoveListener(OnRemoveCode);
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

        private void OnRemoveCode(Code code)
        {
            var currPos = View._medium.GetCodeFragmentPos(code);
            Code nextCode = (Code) View._medium.GetCodeFragment(currPos +1);
            View.RemoveCodeFragment(code);
            OnNextCode(nextCode);
        }

        private void OnCodePositionChanged(int pos)
        {
            View._currCodePos = pos;
            //TODO
//            _view.LoadPictureIntoTexture(pos);
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