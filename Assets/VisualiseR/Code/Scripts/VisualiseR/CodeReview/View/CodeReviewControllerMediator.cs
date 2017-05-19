using System.IO;
using System.Linq;
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
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        [Inject]
        public PileSelectedSignal PileSelectedSignal { get; set; }

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
            CodeRatingChangedSignal.AddListener(OnCodeRatingChanged);
            PileSelectedSignal.AddListener(OnPileSelected);

            InitView();
        }

        public override void OnRemove()
        {
            NextCodeSignal.RemoveListener(OnNextCode);
            RemoveCodeSignal.RemoveListener(OnRemoveCode);
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChanged);
            PileSelectedSignal.RemoveListener(OnPileSelected);
        }

        private void OnPileSelected(Rate rate)
        {
            if (!View._rate.Equals(rate))
            {
                View._rate = rate;
                View._codeFragmentsWithRate = View._medium.GetCodeFragmentsWithRate(rate);
                View.ClearScreens();
                View.InitialiseScreens();
                View.UpdateCode();
            }
        }

        private void OnCodeRatingChanged(Code code)
        {
            View._codeFragmentsWithRate.Remove(code);
            View.RemoveScreensIfNeeded();
            GetNextCodeFragment(code);
        }

        private void OnNextCode(Code code)
        {
            View.NextCode(code);
        }

        private void InitView()
        {
            View._contextView = contextView;
            View._rate = Rate.Unrated;

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

            View._medium = (CodeMedium) CodeMedium;
            View.SetupMedium();
        }

        private void OnRemoveCode(Code code)
        {
            GetNextCodeFragment(code);
            View.RemoveCodeFragment(code);
        }

        private void GetNextCodeFragment(Code code)
        {
            var currPos = View._codeFragmentsWithRate.IndexOf(code);

            if (View._codeFragmentsWithRate.Count > 0)
            {
                Code nextCode = (Code) View._codeFragmentsWithRate.ElementAt((currPos + 1) % View._codeFragmentsWithRate.Count);
                OnNextCode(nextCode);
            }
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