using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewControllerView : View
    {
        internal ICodeMedium _medium;
        internal IPlayer _player;
        internal int _currCodePos;

        internal List<GameObject> screens = new List<GameObject>();
        private List<GameObject> _piles = new List<GameObject>();

        internal GameObject _contextView;
        private GameObject screenParent;
        private GameObject pileParent;


        /// <summary>
        /// Distance between the player and the screens.
        /// </summary>
        private const float SCREEN_DISTANCE = 20;

        /// <summary>
        /// Number of screens shown concurrently in the sceen.
        /// </summary>
        private const int NUMBER_OF_SCREENS_SHOWN = 3;

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
        private const float PILE_DISTANCE = 5;

        /// <summary>
        /// Degree of the radius, in which the screens are placed
        /// </summary>
        private const int PILE_RADIUS = 150;

        private const float PILE_START_ANGLE = 15;

        /// <summary>
        /// y value of the placed piles
        /// </summary>
        private const float PILE_VALUE_Y = 0.1f;


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
                _currCodePos = 0;
                IntialiseScreens();
                InitialisePiles();
            }
        }

        private void IntialiseScreens()
        {
            InstantiateScreenParent();

            bool isFirst = true;
            List<Vector3> screenPositions = GetScreenPositions();
            for (int i = 0; i < screenPositions.Count; i++)
            {
                //TODO zu überprüfen, ob diese Abfrage auch richtig greift
                if (i <= _medium.CodeFragments.Count - 1)
                {
                    InstantiateScreen(_medium.GetCodeFragment(_currCodePos + i), screenPositions[i], isFirst);
                    isFirst = false;
                }
            }
        }

        private void InstantiateScreenParent()
        {
            screenParent = new GameObject();
            screenParent.name = "Screens";
            screenParent.transform.SetParent(_contextView.transform);
        }

        private static List<Vector3> GetScreenPositions()
        {
            return MathUtil.ComputeSpawnPositionsWithElements(SCREEN_DISTANCE, NUMBER_OF_SCREENS_SHOWN, SCREEN_RADIUS,
                SCREEN_START_ANGLE, SCREEN_VALUE_Y);
        }

        private void InstantiateScreen(ICode code, Vector3 pos, bool isFirst)
        {
            //fix rotation
            Vector3 relativePos = Vector3.zero - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen = (GameObject) Instantiate(Resources.Load("CodeReview_Screen"), pos, rotation);
            screen.transform.Rotate(-270, 180, 180);
            screen.name = "Screen";
            screen.transform.SetParent(screenParent.transform);

            //TODO direkte verbindung verhindern
            CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
            screenView.Init(code, isFirst);

            screens.Add(screen);
        }


        private void InitialisePiles()
        {
            InstiatePileParent();

//            List<Vector3> pilePositions = MathUtil.ComputePilePositions(rates, PILE_VALUE_X_START,
            List<Vector3> pilePositions = GetPilePositions();
            foreach (var position in pilePositions)
            {
                Debug.Log(position);
            }

            Array rates = EnumUtil.GetValues<Rate>();
            for (var i = 0; i < pilePositions.Count; i++)
            {
                Rate currRate = (Rate) rates.GetValue(i);
                List<ICode> currCode = new List<ICode>();
                if (currRate.Equals(Rate.Unrated))
                {
                    currCode = _medium.CodeFragments;
                }

                InstantiatePile(currRate, pilePositions[i], currCode);
            }
        }

        private void InstiatePileParent()
        {
            pileParent = new GameObject();
            pileParent.name = "Piles";
            pileParent.transform.SetParent(_contextView.transform);
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
            pile.transform.Rotate(-270, 180, 180);
            pile.transform.SetParent(pileParent.transform);

            PileView pileView = pile.GetComponent<PileView>();
            pileView.Init(rate, codes);

            _piles.Add(pile);
        }

        public void NextCode(Code code)
        {
            int currPos = _medium.GetCodeFragmentPos(code);
            foreach (var screen in screens)
            {
                var currCode = _medium.GetCodeFragment(currPos);
                CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
                screenView.ChangeCode(currCode);
                currPos = getNextCodePosition(currPos);
            }
        }

        private int getNextCodePosition(int pos)
        {
            return (pos + 1) % _medium.CodeFragments.Count;
        }

        public void RemoveCodeFragment(Code code)
        {
            _medium.RemoveCodeFragment(code);
            if (_medium.CodeFragments.Count < screens.Count)
            {
                var lastScreen = screens.Last();
                Destroy(lastScreen);
            }
        }
    }
}