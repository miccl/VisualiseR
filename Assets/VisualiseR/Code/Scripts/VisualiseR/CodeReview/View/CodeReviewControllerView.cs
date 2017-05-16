using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
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

        public Signal<IPlayer, ICodeMedium, int> NextCodeSignal = new Signal<IPlayer, ICodeMedium, int>();
        public Signal<IPlayer, ICodeMedium, int> PrevCodeSignal = new Signal<IPlayer, ICodeMedium, int>();
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
            List<Vector3> screenPositions = GetScreenPositions();
            for (int i = 0; i < screenPositions.Count; i++)
            {
                //TODO zu überprüfen, ob diese Abfrage auch richtig greift
                if (i <= _medium.CodeFragments.Count - 1)
                {
                    InstantiateScreen(_medium.GetCodeFragment(_currCodePos + i), screenPositions[i]);
                }
            }
        }

        private static List<Vector3> GetScreenPositions()
        {
            return MathUtil.ComputeSpawnPositionsWithElements(SPAWN_DISTANCE, NUMBER_OF_SCREENS_SHOWN, RADIUS, Y_VALUE);
        }


        private void InstantiateScreen(ICode code, Vector3 pos)
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

            screens.Add(screen);
        }


//        private ShowCodeScreen(ICode code)
//        {
//        }

        private void NextCode()
        {
            NextCodeSignal.Dispatch(_player, _medium, _currCodePos);
        }

        private void PrevCode()
        {
            PrevCodeSignal.Dispatch(_player, _medium, _currCodePos);
        }
    }
}