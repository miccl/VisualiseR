using System.Collections.Generic;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.Rendering;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class ShowAllSlidesCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowAllSlidesCommand));

        private static readonly float SPAWN_DISTANCE = 20;
        private static readonly float START_ANGLE = 180;
        private static readonly float END_ANGLE = 360;
        private static readonly float MIN_ANGLE_BETWEEN_ELEMENTS = 30;
        private static readonly float MAX_STAGES = 3;
        private static readonly float START_POS_Y = 5;
        private static readonly float POS_Y_DISTANCE = 10;
        private GameObject _simpleScreenParent;

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            var screen = DeactivateOtherObjects();
            InitialiseSimpleScreens(screen);
            Logger.InfoFormat("All slides are shown");
        }

        private GameObject DeactivateOtherObjects()
        {
            var screen = DeactivateScreen();
            DeactivateWalls();
            return screen;
        }

        [CanBeNull]
        private static GameObject DeactivateScreen()
        {
            GameObject screens = UnityUtil.FindGameObject("Screens");
            screens.SetActive(false);
            var screen = screens.transform.Find("Presentation_Screen").gameObject;
            return screen;
        }

        private void DeactivateWalls()
        {
            var walls = GameObject.Find("Environment");
            walls.SetActive(false);
        }

        private void InitialiseSimpleScreens(GameObject screen)
        {
            ISlideMedium medium = GetSlides(screen);
            if (medium == null) return;
            InstantiateSimpleScreenParent();
            List<Vector3> positions = GetPositions(medium);
            for (int i = 0; i < positions.Count; i++)
            {
                InstantiatePresentationScreen(positions[positions.Count - i - 1], medium.Slides[i], i);
            }
        }

        private ISlideMedium GetSlides(GameObject screen)
        {
            PresentationScreenView screenView = screen.GetComponent<PresentationScreenView>();
            return screenView._medium;
        }

        private List<Vector3> GetPositions(ISlideMedium slides)
        {
            return ScreenPositionUtil.ComputeSpawnPositionsWithAngle(SPAWN_DISTANCE, slides.Slides.Count, START_ANGLE, END_ANGLE,
                MIN_ANGLE_BETWEEN_ELEMENTS, MAX_STAGES, START_POS_Y, POS_Y_DISTANCE, Camera.main.transform.position.x, Camera.main.transform.position.z);
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


        private void InstantiatePresentationScreen(Vector3 pos, ISlide slide, int slidePos)
        {
            Vector3 relativePos = Camera.main.transform.position - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen =
                (GameObject) GameObject.Instantiate(Resources.Load("Simple_Presentation_Screen"), pos, rotation);
            screen.transform.Rotate(-270, 180, 180);
            screen.name = "SimpleScreen_" + slidePos;
            screen.transform.SetParent(_simpleScreenParent.transform);

            //TODO direkte verbindung verhindern
            SimplePresentationScreenView screenView = screen.GetComponent<SimplePresentationScreenView>();
            screenView.Init(slide);
        }
    }
}