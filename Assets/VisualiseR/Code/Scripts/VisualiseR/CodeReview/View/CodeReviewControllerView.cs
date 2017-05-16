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

        internal GameObject _contextView;


        /// <summary>
        /// Distance between the player and the screens.
        /// </summary>
        private const float SPAWN_DISTANCE = 20;

        /// <summary>
        /// Number of screens shown concurrently in the sceen.
        /// </summary>
        private const int NUMBER_OF_SCREENS_SHOWN = 3;

        /// <summary>
        /// Degree of the radius, in which the screens are placed
        /// </summary>
        private const int RADIUS = 90;

        /// <summary>
        /// Y value of the placed screens
        /// </summary>
        private const float Y_VALUE = 5;

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
            }
        }

        private void IntialiseScreens()
        {
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

        private static List<Vector3> GetScreenPositions()
        {
            return MathUtil.ComputeSpawnPositionsWithElements(SPAWN_DISTANCE, NUMBER_OF_SCREENS_SHOWN, RADIUS, Y_VALUE);
        }


        private void InstantiateScreen(ICode code, Vector3 pos, bool isFirst)
        {
            //fix rotation
            Vector3 relativePos = Vector3.zero - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen = Instantiate(Resources.Load("CodeReview_Screen"), pos, rotation) as GameObject;
            screen.transform.Rotate(-270, 180, 180);

            screen.name = "Screen";
            screen.transform.SetParent(_contextView.transform);

            //TODO direkte verbindung verhindern
            CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
            screenView.ChangeCode(code);
            screenView.IsFirst = isFirst;

            screens.Add(screen);

        }

        public void NextCode(Code code)
        {
            int currPos = _medium.GetCodeFragmentPos(code);
            foreach (var screen in screens)
            {
                var currCode = _medium.GetCodeFragment(currPos);
                CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
                screenView.ChangeCode(currCode);
                currPos = getNextPosition(currPos);
            }

        }

        private int getNextPosition(int pos)
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