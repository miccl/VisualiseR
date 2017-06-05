using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class ShowAllCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowAllCommand));

        private static readonly float SPAWN_DISTANCE = 20;
        private static readonly float START_ANGLE = 0;
        private static readonly float END_ANGLE = 180;
        private static readonly float MIN_AGNLE_BETWEEN_ELEMENTS = 20;
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

        private static GameObject DeactivateScreen()
        {
            GameObject screen = GameObject.Find("Presentation_Screen");
            if (screen == null)
            {
                Logger.ErrorFormat("Couldn't find game object '{0}'", "Presentation_Screen");
                return null;
            }
            screen.SetActive(false);
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
            return MathUtil.ComputeSomething(SPAWN_DISTANCE, slides.Slides.Count, START_ANGLE, END_ANGLE,
                MIN_AGNLE_BETWEEN_ELEMENTS, MAX_STAGES, START_POS_Y, POS_Y_DISTANCE);
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
            Vector3 relativePos = Vector3.zero - pos;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            var screen =
                (GameObject) GameObject.Instantiate(Resources.Load("Simple_Presentation_Screen"), pos, rotation);
            screen.transform.Rotate(-270, 180, 180);
            screen.name = "SimpleScreen_" + slidePos;
            screen.transform.SetParent(_simpleScreenParent.transform);

            //TODO direkte verbindung verhindern
            SimplePresentationScreenView screenView = screen.GetComponent<SimplePresentationScreenView>();
            screenView.Init(slide, slidePos);
        }
    }
}