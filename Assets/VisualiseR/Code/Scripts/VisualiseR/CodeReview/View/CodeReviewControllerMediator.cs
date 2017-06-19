using System;
using System.IO;
using System.Linq;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewControllerMediator : Mediator
    {
        [Inject]
        public CodeReviewControllerView _view { get; set; }
        
        [Inject]
        public ShowSceneMenuSignal ShowSceneMenuSignal { get; set; }

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
        
        [Inject]
        public CodeReviewSceneMenuIsShownSignal CodeReviewSceneMenuIsShownSignal { get; set; }

        [Inject]
        public CodeReviewContextMenuIsShownSignal CodeReviewContextMenuIsShownSignal { get; set; }
        
        [Inject]
        public ShowAllCodeSignal ShowAllCodeSignal { get; set; }
        
        [Inject]
        public CodeSelectedSignal CodeSelectedSignal { get; set; }

        public override void OnRegister()
        {
            _view.ShowSceneMenuSignal.AddListener(OnShowSceneMenuSignal);
            NextCodeSignal.AddListener(OnNextCode);
            RemoveCodeSignal.AddListener(OnRemoveCode);
            CodeRatingChangedSignal.AddListener(OnCodeRatingChanged);
            PileSelectedSignal.AddListener(OnPileSelected);
            CodeReviewSceneMenuIsShownSignal.AddListener(OnSceneMenuIsShown);
            CodeReviewContextMenuIsShownSignal.AddListener(OnContextMenuIsShown);
            ShowAllCodeSignal.AddListener(OnShowAllCode);
            CodeSelectedSignal.AddListener(OnCodeSelected);

            InitView();
        }

        public override void OnRemove()
        {
            _view.ShowSceneMenuSignal.RemoveListener(OnShowSceneMenuSignal);
            NextCodeSignal.RemoveListener(OnNextCode);
            RemoveCodeSignal.RemoveListener(OnRemoveCode);
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChanged);
            PileSelectedSignal.RemoveListener(OnPileSelected);
            CodeReviewSceneMenuIsShownSignal.RemoveListener(OnSceneMenuIsShown);
            CodeReviewContextMenuIsShownSignal.RemoveListener(OnContextMenuIsShown);
            ShowAllCodeSignal.RemoveListener(OnShowAllCode);
            CodeSelectedSignal.RemoveListener(OnCodeSelected);

        }

        private void OnShowSceneMenuSignal(ICodeMedium medium)
        {
            ShowSceneMenuSignal.Dispatch((CodeMedium) medium);
        }

        private void OnPileSelected(Rate rate)
        {
            if (!_view._rate.Equals(rate))
            {
                _view._rate = rate;
                _view._codeFragmentsWithRate = _view._medium.GetCodeFragmentsWithRate(rate);
                _view.ActivateOrDeactivateScreens();
                if (_view._codeFragmentsWithRate.Count > 0)
                {
                    NextCodeSignal.Dispatch((Code) _view._codeFragmentsWithRate[0]);
                }
                else
                {
                    NextCodeSignal.Dispatch(null);
                }
            }
        }

        private void OnSceneMenuIsShown(bool isShown)
        {
            _view._isSceneMenuShown = isShown;
        }

        private void OnContextMenuIsShown(bool isShown)
        {
            _view._isContextMenuShown = isShown;
        }


        private void OnCodeRatingChanged(Code code)
        {
            _view._codeFragmentsWithRate.Remove(code);
            _view.ActivateOrDeactivateScreens();
            GetNextCodeFragment(code);
        }

        private void OnNextCode(Code code)
        {
            _view.NextCode(code);
        }

        private void InitView()
        {
            _view._contextView = contextView;
            _view._rate = Rate.Unrated;

            object o = PlayerPrefsUtil.RetrieveObject(PlayerPrefsUtil.ROOM_KEY);
            if (o != null)
            {
                Common.Room room = (Common.Room) o;
                PictureMedium = (IPictureMedium) room.Medium;
                ConstructCodeMedium(PictureMedium);
            }
            else
            {
                CodeMedium = CreateMockMedium();
            }

            _view._medium = (CodeMedium) CodeMedium;
            _view.SetupMedium();
        }

        private void OnRemoveCode(Code code)
        {
            GetNextCodeFragment(code);
            _view.RemoveCodeFragment(code);
        }

        private void GetNextCodeFragment(Code code)
        {
            var currPos = _view._codeFragmentsWithRate.IndexOf(code);

            if (_view._codeFragmentsWithRate.Count > 0)
            {
                Code nextCode =
                    (Code) _view._codeFragmentsWithRate.ElementAt((currPos + 1) % _view._codeFragmentsWithRate.Count);
                NextCodeSignal.Dispatch(nextCode);
            }
        }

        private void ConstructCodeMedium(IPictureMedium medium)
        {
            string mainDirName = String.Format("{0}_{1}", medium.Name, DateTime.Now.Ticks);
            string mainDir = Application.persistentDataPath + Path.DirectorySeparatorChar + "CodeReviews" + Path.DirectorySeparatorChar + mainDirName;
            DirectoryInfo mainDirInfo = DirectoryUtil.CreateDirectorysForCodeReview(mainDir);
            if (mainDirInfo != null && mainDirInfo.Exists)
            {
                PlayerPrefsUtil.SaveValue(PlayerPrefsUtil.MAIN_DIR, mainDirInfo.FullName);

                CodeMedium.Name = medium.Name;
                foreach (var pic in medium.Pictures)
                {
                    ICode code = new Code();
                    code.Name = pic.Title;
                    code.OldPath = pic.Path;
                    var filePath = FileUtil.CopyFile(pic.Path,
                        DirectoryUtil.GetRatingDirectory(mainDirInfo.FullName, Rate.Unrated));
                    if (filePath != null)
                    {
                        pic.Path = filePath;
                        code.Pic = pic;
                    }
                    else
                    {
                        Debug.LogErrorFormat("File '{0}' couldnt be copied", pic.Path);
                    }
                    CodeMedium.AddCodeFragment(code);
                }
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

        private void OnShowAllCode()
        {
            _view._isShowAll = true;
        }

        private void OnCodeSelected(Code arg1, Player arg2)
        {
            _view._isShowAll = false;
        }
    }
}