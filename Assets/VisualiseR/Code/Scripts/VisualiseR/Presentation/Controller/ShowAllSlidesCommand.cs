using System.Collections.Generic;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Command to show all slides in the scene.
    /// </summary>
    public class ShowAllSlidesCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowAllSlidesCommand));

        /// <summary>
        /// Distance between player and screen.
        /// </summary>
        private static readonly float SPAWN_DISTANCE = 20;
        /// <summary>
        /// Start angle for the screens.
        /// </summary>
        private static readonly float START_ANGLE = 210;
        /// <summary>
        /// End angle for the screens.
        /// </summary>
        private static readonly float END_ANGLE = 330;
        /// <summary>
        /// Minimum angle betweeen two screens.
        /// </summary>
        private static readonly float MIN_ANGLE_BETWEEN_ELEMENTS = 30;
        /// <summary>
        /// Maximum amount stages of screens.
        /// </summary>
        private static readonly float MAX_STAGES = 3;
        /// <summary>
        /// Starting y-position of the screens.
        /// </summary>
        private static readonly float START_POS_Y = 5;
        /// <summary>
        /// Distance between two stages.
        /// </summary>
        private static readonly float POS_Y_DISTANCE = 10;
        /// <summary>
        /// Parent of
        /// </summary>
        private GameObject _simpleScreenParent;

        [Inject]
        public ShowLaserSignal ShowLaserSignal { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            var screen = DeactivateOtherObjects();
            InitialiseSimpleScreens(screen);
            Logger.InfoFormat("All slides are shown");
        }

        /// <summary>
        /// Deactivate all unecessary objects.
        /// </summary>
        /// <returns></returns>
        private GameObject DeactivateOtherObjects()
        {
            var screen = DeactivateScreen();
            DeactivateWalls();
            ShowLaserSignal.Dispatch(false);
            return screen;
        }

        /// <summary>
        /// Deactivates the screen.
        /// </summary>
        /// <returns></returns>
        [CanBeNull]
        private static GameObject DeactivateScreen()
        {
            GameObject screens = UnityUtil.FindGameObject("Screens");
            screens.SetActive(false);
            var screen = screens.transform.Find("Presentation_Screen").gameObject;
            return screen;
        }

        /// <summary>
        /// Deactivate the walls.
        /// </summary>
        private void DeactivateWalls()
        {
            var walls = GameObject.Find("Environment");
            walls.SetActive(false);
        }

        /// <summary>
        /// Initialises the <see cref="SimplePresentationScreenView"/>s.
        /// </summary>
        /// <param name="screen"></param>
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

        /// <summary>
        /// Retrieve slides from the mains screen.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        private ISlideMedium GetSlides(GameObject screen)
        {
            PresentationScreenView screenView = screen.GetComponent<PresentationScreenView>();
            return screenView._medium;
        }

        /// <summary>
        /// Returns the positions of the screen.
        /// </summary>
        /// <param name="slides"></param>
        /// <returns></returns>
        private List<Vector3> GetPositions(ISlideMedium slides)
        {
            return ScreenPositionUtil.ComputeSpawnPositionsWithAngle(SPAWN_DISTANCE, slides.Slides.Count, START_ANGLE, END_ANGLE,
                MIN_ANGLE_BETWEEN_ELEMENTS, MAX_STAGES, START_POS_Y, POS_Y_DISTANCE, Camera.main.transform.position.x, Camera.main.transform.position.z);
        }

        /// <summary>
        /// Initialises simple screen parent.
        /// </summary>
        private void InstantiateSimpleScreenParent()
        {
            if (_simpleScreenParent == null)
            {
                _simpleScreenParent = new GameObject();
                _simpleScreenParent.name = "SimpleScreens";
                _simpleScreenParent.transform.SetParent(_contextView.transform);
            }
        }


        /// <summary>
        /// Initialises presentation screen at given position.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="slide"></param>
        /// <param name="slidePos"></param>
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