using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class ShowAllCodeCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowAllCodeCommand));

        private static readonly float SPAWN_DISTANCE = 20;
        private static readonly float START_ANGLE = 0;
        private static readonly float END_ANGLE = 180;
        private static readonly float MIN_AGNLE_BETWEEN_ELEMENTS = 25;
        private static readonly float MAX_STAGES = 3;
        private static readonly float START_POS_Y = 5;
        private static readonly float POS_Y_DISTANCE = 10;
        private GameObject _simpleScreenParent;

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            DeactivateOtherObjects();
            InitialiseSimpleScreens();
            Logger.InfoFormat("All code are shown");
        }

        private void DeactivateOtherObjects()
        {
            DeactivateScreen();
            DeactivatateMenus();
            DeactivateInfoScreen();
            DeactivatePiles();
        }

        private static void DeactivateScreen()
        {
            GameObject screens = UnityUtil.FindGameObject("Screens");
            screens.SetActive(false);
        }

        private void DeactivatateMenus()
        {
            GameObject sceneMenu = UnityUtil.FindGameObject("CodeReviewSceneMenuCanvas");
            CodeReviewSceneMenuView sceneMenuView = sceneMenu.GetComponent<CodeReviewSceneMenuView>();
            sceneMenuView.Hide();
        }

        private void DeactivateInfoScreen()
        {
            GameObject infoCanvas = UnityUtil.FindGameObject("InfoCanvas");
            infoCanvas.SetActive(false);
        }

        private void DeactivatePiles()
        {
            GameObject piles = UnityUtil.FindGameObject("Piles");
            piles.SetActive(false);
        }

        private void InitialiseSimpleScreens()
        {
            CodeReviewControllerView screenView = _contextView.GetComponent<CodeReviewControllerView>();
            ICodeMedium medium = screenView._medium;
            if (medium == null) return;
            IPlayer player = screenView._player;

            InstantiateSimpleScreenParent();
            List<Vector3> positions = GetPositions(medium);
            for (int i = 0; i < positions.Count; i++)
            {
                InstantiatePresentationScreen(positions[positions.Count - i - 1], medium.CodeFragments[i], i, player);
            }
        }

        private List<Vector3> GetPositions(ICodeMedium codeMedium)
        {
            return ScreenPositionUtil.ComputeSpawnPositionsWithAngle(SPAWN_DISTANCE, codeMedium.CodeFragments.Count,
                START_ANGLE, END_ANGLE,
                MIN_AGNLE_BETWEEN_ELEMENTS, MAX_STAGES, START_POS_Y, POS_Y_DISTANCE, Camera.main.transform.position.x,
                Camera.main.transform.position.z);
        }

        private void InstantiateSimpleScreenParent()
        {
            if (_simpleScreenParent == null)
            {
                _simpleScreenParent = new GameObject();
                _simpleScreenParent.name = "SimpleScreens";
                _simpleScreenParent.transform.SetParent(_contextView.transform);
            }
        }


        private void InstantiatePresentationScreen(Vector3 pos, ICode code, int codePos, IPlayer player)
        {
            Vector3 relativePos = Camera.main.transform.position - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen =
                (GameObject) GameObject.Instantiate(Resources.Load("SimpleCodeReviewScreen"), pos, rotation);
            screen.transform.Rotate(-270, 180, 180);
            screen.name = "SimpleScreen_" + codePos;
            screen.transform.SetParent(_simpleScreenParent.transform);

            SimpleCodeReviewScreenView screenView = screen.GetComponent<SimpleCodeReviewScreenView>();
            screenView.Init(code, player);
        }
    }
}