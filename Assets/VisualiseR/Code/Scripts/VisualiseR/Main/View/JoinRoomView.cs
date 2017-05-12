using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class JoinRoomView : View
    {
        private static readonly string CHOOSE_MEDIUM_TEXT = " Choose medium...";
        private static readonly string SELECT_DISK_FILE = "Choose disk file";
        private static readonly string SELECT_WEB_FILE = "Choose web file";

        internal InputField RoomNameInputField;

        private GameObject _mainMenuPanelView;

        protected override void Awake()
        {
            base.Awake();
            RoomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        public void OnJoinRoomButtonClick()
        {
            if (IsValidInput())
            {
                //TODO Join Room Signal...
            }
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }


        /// <summary>
        /// Test, if all needed information are choosen.
        /// If not, a error message is displayed with
        /// </summary>
        private bool IsValidInput()
        {
            //TODO Die Validerung sollte wohl woanders gemacht werden, wahrscheinlich im Model?! zumindestens die formale Validierung?
            //TODO Fehlernachricht anzeigen
            //TODO vielleicht andere Validierungen hinzufügen (keine Sonderzeichen...)
            //TODO Photon ist der Raumname schon vergeben???
            if (RoomNameInputField.text == null)
            {
                Debug.LogFormat("Room name wasn't choosen yet ({0})", RoomNameInputField.text);
                return false;
            }

            return true;
        }
    }
}