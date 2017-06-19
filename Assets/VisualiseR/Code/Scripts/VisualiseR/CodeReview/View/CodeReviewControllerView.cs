﻿using System;
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
    /// Controls the entire code review scene.
    /// </summary>
    public class CodeReviewControllerView : View
    {
        public Signal<ICodeMedium> ShowSceneMenuSignal = new Signal<ICodeMedium>();

        internal ICodeMedium _medium;
        internal List<ICode> _codeFragmentsWithRate;
        public Rate _rate;
        internal IPlayer _player;


        internal List<GameObject> screens = new List<GameObject>();

        internal GameObject _contextView;
        private GameObject _screenParent;
        private GameObject _pileParent;
        internal bool _isSceneMenuShown = false;
        internal bool _isContextMenuShown = false;
        public bool _isShowAll = false;
        private GameObject _screen1;
        private GameObject _screen2;
        private GameObject _screen3;


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

        internal void InitialiseScreens()
        {
            if (_codeFragmentsWithRate.Count > 0)
            {
                var screenCount = 3;
                bool isFirst = true;
                var screensGO = _contextView.transform.Find("Screens");

                for (int i = 1; i <= screenCount; i++)
                {
                    var screenName = "Screen" + i;
                    var screen = screensGO.Find(screenName).gameObject;

                    CodeReviewScreenView screenView = screen.GetComponent<CodeReviewScreenView>();
                    screenView.Init(isFirst);

                    screens.Add(screen);
                    isFirst = false;
                }
            }
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
            return ScreenPositionUtil.ComputeSpawnPositionsWithElements(PILE_DISTANCE, pileCount, PILE_RADIUS,
                PILE_START_ANGLE,
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
        }

        private void InstantiateInfoScreen()
        {
            var infoScreen = _contextView.transform.Find("InfoCanvas");
            //TODO direkte verbindung verhindern
            InfoView infoView = infoScreen.GetComponent<InfoView>();
            infoView.UpdateView(_codeFragmentsWithRate[0]);
        }

        /// <summary>
        /// Shows the given code in the center of the scene.
        /// Rotates the others accordingly around.
        /// </summary>
        /// <param name="code"></param>
        public void NextCode(ICode code)
        {
            if (code == null)
            {
                return;
            }
            int currPos = _codeFragmentsWithRate.IndexOf(code);
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
            ActivateOrDeactivateScreens();
        }

        void Update()
        {
            if (Input.GetButtonDown(ButtonUtil.CANCEL))
            {
                if (_isSceneMenuShown || _isContextMenuShown || _isShowAll)
                {
                    return;
                }
                ShowSceneMenuSignal.Dispatch(_medium);
            }
        }

        public void ActivateOrDeactivateScreens()
        {
            var count = _codeFragmentsWithRate.Count;
            screens[0].SetActive(count > 0);
            screens[1].SetActive(count > 1);
            screens[2].SetActive(count > 2);
        }
    }
}