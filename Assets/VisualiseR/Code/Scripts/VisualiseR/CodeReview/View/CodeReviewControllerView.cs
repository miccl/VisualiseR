using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Controls the entire
    /// </summary>
    public class CodeReviewControllerView : View
    {
        public Signal<ICodeMedium> ShowSceneMenuSignal = new Signal<ICodeMedium>();

        internal ICodeMedium _medium;
        internal List<ICode> _codeFragmentsWithRate;
        public Rate _rate;
        internal IPlayer _player;


        internal List<GameObject> screens = new List<GameObject>();
        private List<GameObject> _piles = new List<GameObject>();

        internal GameObject _contextView;
        private GameObject _screenParent;
        private GameObject _pileParent;
        internal bool _isSceneMenuShown = false;
        internal bool _isContextMenuShown = false;


        /// <summary>
        /// Distance between the player and the screens.
        /// </summary>
        private const float SCREEN_DISTANCE = 20;

        /// <summary>
        /// Number of screens shown concurrently in the sceen.
        /// </summary>
        private const int MAX_NUMBER_OF_SCREENS_SHOWN = 3;

        /// <summary>
        /// Degree of the radius, in which the screens are placed
        /// </summary>
        private const int SCREEN_RADIUS = 90;


        private const float SCREEN_START_ANGLE = 90;

        /// <summary>
        /// Y value of the placed screens
        /// </summary>
        private const float SCREEN_VALUE_Y = 3;

        /// <summary>
        /// Distance between the player and the piles.
        /// </summary>
        private const float PILE_DISTANCE = 3;

        /// <summary>
        /// Degree of the radius, in which the screens are placed
        /// </summary>
        private const int PILE_RADIUS = 150;

        private const float PILE_START_ANGLE = 15;

        /// <summary>
        /// y value of the placed piles
        /// </summary>
        private const float PILE_VALUE_Y = 0.1f;


        private const float INFO_DISTANCE = 5;

        private const int INFO_RADIUS = -75;


        protected override void Awake()
        {
            base.Awake();

            _player = new Player
            {
                Name = "Test",
                Type = PlayerType.Host
            };
        }

        internal void SetupMedium()
        {
            if (_medium != null && !_medium.IsEmpty())
            {
                _codeFragmentsWithRate = _medium.GetCodeFragmentsWithRate(Rate.Unrated);
                InitialiseScreens();
                InitialisePiles();
                InstantiateInfoScreen();
                NextCode(_codeFragmentsWithRate[0]);
            }
        }

        internal void ClearScreens()
        {
            foreach (var screen in screens)
            {
                Destroy(screen);
            }
            screens.Clear();
        }

        internal void InitialiseScreens()
        {
            if (_codeFragmentsWithRate.Count > 0)
            {
                InstantiateScreenParent();

                bool isFirst = true;
                List<Vector3> screenPositions = GetScreenPositions();
                for (int i = 0; i < screenPositions.Count; i++)
                {
                    InstantiateScreen(screenPositions[i], isFirst);
                    isFirst = false;
                }
            }
        }

        private void InstantiateScreenParent()
        {
            if (_screenParent == null)
            {
                _screenParent = new GameObject();
                _screenParent.name = "Screens";
                _screenParent.transform.SetParent(_contextView.transform);
            }
        }

        private List<Vector3> GetScreenPositions()
        {
            return MathUtil.ComputeSpawnPositionsWithElements(SCREEN_DISTANCE, MAX_NUMBER_OF_SCREENS_SHOWN,
                SCREEN_RADIUS,
                SCREEN_START_ANGLE, SCREEN_VALUE_Y);
        }

        private void InstantiateScreen(Vector3 pos, bool isFirst)
        {
            //fix rotation
            Vector3 relativePos = Vector3.zero - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen = (GameObject) Instantiate(Resources.Load("CodeReview_Screen"), pos, rotation);
            screen.transform.Rotate(-270, 180, 180);
            screen.name = "Screen";
            screen.transform.SetParent(_screenParent.transform);

            //TODO direkte verbindung verhindern
            CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
            screenView.Init(isFirst);

            screens.Add(screen);
        }


        private void InitialisePiles()
        {
            InstantiatePileParent();

            List<Vector3> pilePositions = GetPilePositions();

            Array rates = EnumUtil.GetValues<Rate>();
            for (var i = 0; i < pilePositions.Count; i++)
            {
                Rate currRate = (Rate) rates.GetValue(i);
                List<ICode> currCode = new List<ICode>();
                if (currRate.Equals(Rate.Unrated))
                {
                    currCode = new List<ICode>(_medium.CodeFragments);
                }

                var pilePos = pilePositions[pilePositions.Count - i - 1];
                InstantiatePile(currRate, pilePos, currCode);
            }
        }

        private void InstantiatePileParent()
        {
            if (_pileParent == null)
            {
                _pileParent = new GameObject();
                _pileParent.name = "Piles";
                _pileParent.transform.SetParent(_contextView.transform);
            }
        }

        private static List<Vector3> GetPilePositions()
        {
            int pileCount = EnumUtil.Length<Rate>();
            return MathUtil.ComputeSpawnPositionsWithElements(PILE_DISTANCE, pileCount, PILE_RADIUS, PILE_START_ANGLE,
                PILE_VALUE_Y);
        }


        private void InstantiatePile(Rate rate, Vector3 pos, List<ICode> codes)
        {
            Vector3 relativePos = Vector3.zero - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var pile = (GameObject) Instantiate(Resources.Load("PileCanvas"), pos, rotation);
            pile.name = "Pile_" + rate;
            pile.transform.Rotate(67.5f, -180, 0);
            pile.transform.SetParent(_pileParent.transform);

            PileView pileView = pile.GetComponent<PileView>();
            pileView.Init(rate, codes);

            _piles.Add(pile);
        }

        private void InstantiateInfoScreen()
        {
            var computeSpawnPositionFromStartPosition =
                MathUtil.ComputeSpawnPositionFromStartPosition(INFO_DISTANCE, INFO_RADIUS, 90, 2);
            if (computeSpawnPositionFromStartPosition != null)
            {
                Vector3 pos = (Vector3) computeSpawnPositionFromStartPosition;
                Vector3 relativePos = Vector3.zero - pos;
                Quaternion rotation = Quaternion.LookRotation(relativePos);

                var info = (GameObject) Instantiate(Resources.Load("InfoCanvas"), pos, rotation);
                info.transform.Rotate(0, -180, 0);
                info.name = "InfoScreen";
                info.transform.SetParent(_contextView.transform);

                //TODO direkte verbindung verhindern
                InfoView infoView = info.GetComponent<InfoView>();
                infoView.UpdateView(_codeFragmentsWithRate[0]);
            }
        }

        /// <summary>
        /// Shows the given code in the center of the scene.
        /// Rotates the others accordingly around.
        /// </summary>
        /// <param name="code"></param>
        public void NextCode(ICode code)
        {
            int currPos = _codeFragmentsWithRate.IndexOf(code);
            Debug.Log("Curr Pos:" + currPos);
            foreach (var screen in screens)
            {
                var currCode = _codeFragmentsWithRate.ElementAt(currPos);
                CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
                screenView.ChangeCode(currCode);
                currPos = GetNextCodePosition(currPos);
            }
        }

        private int GetNextCodePosition(int pos)
        {
            return (pos + 1) % _codeFragmentsWithRate.Count;
        }

        public void RemoveCodeFragment(Code code)
        {
            _medium.RemoveCodeFragment(code);
            _codeFragmentsWithRate.Remove(code);
            RemoveScreensIfNeeded();
        }

        internal void RemoveScreensIfNeeded()
        {
            while (_codeFragmentsWithRate.Count < screens.Count)
            {
                var lastScreen = screens.Last();
                Destroy(lastScreen);
                screens.Remove(lastScreen);
            }
        }

        void Update()
        {
            if (Input.GetButtonDown(ButtonUtil.CANCEL))
            {
                if (_isSceneMenuShown || _isContextMenuShown)
                {
                    return;
                }
                ShowSceneMenuSignal.Dispatch(_medium);

            }
        }
    }
}