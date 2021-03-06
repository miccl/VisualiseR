﻿using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// View for the main menu.
    /// </summary>
    public class MainMenuView : View
    {
        public Signal<Message> MessageSignal = new Signal<Message>();

        private GameObject _settingsPanel;

        [SerializeField] private Sprite audioOffSprite;
        [SerializeField] private Sprite audioOnSprite;

        private Image _audioImage;
        private GameObject _helpPanel;
        private GameObject _createRoomPanel;
        private GameObject _joinRoomPanel;
        private GameObject _aboutPanel;
        private AudioSource _backgroundAudioSource;
        private bool _isMuted;
        private bool _fire1Pressed;

        protected override void Awake()
        {
            base.Awake();
            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _settingsPanel = menuCanvas.transform.Find("SettingsPanel").gameObject;
            _helpPanel = menuCanvas.transform.Find("HelpPanel").gameObject;
            _createRoomPanel = menuCanvas.transform.Find("CreateRoomPanel").gameObject;
            _joinRoomPanel = menuCanvas.transform.Find("JoinRoomPanel").gameObject;
            _aboutPanel = menuCanvas.transform.Find("AboutPanel").gameObject;

            _backgroundAudioSource = menuCanvas.GetComponent<AudioSource>();

            _audioImage = transform.FindChild("AudioButton").GetComponent<Image>();
        }

        protected override void Start()
        {
            base.Start();
            _isMuted = (PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.AUDIO_MUTED, 0) != 0);
            PlayMusic(_isMuted);
        }

        public void OnCreateRoomButtonClick()
        {
            _createRoomPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnJoinRoomButtonClick()
        {
            _joinRoomPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSettingsButtonClick()
        {
            _settingsPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnAudioButtonClick()
        {
            _isMuted = !_isMuted;
            PlayerPrefsUtil.SaveValue(PlayerPrefsUtil.AUDIO_MUTED, _isMuted ? 1 : 0);
            PlayMusic(_isMuted);
        }

        private void PlayMusic(bool isMuted)
        {
            _backgroundAudioSource.mute = isMuted;
            if (!isMuted)
            {
                _audioImage.sprite = audioOnSprite;
                _backgroundAudioSource.PlayDelayed(0);
            }
            else
            {
                _audioImage.sprite = audioOffSprite;
            }
        }

        public void OnHelpButtonClick()
        {
            _helpPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnAboutButtonClick()
        {
            _aboutPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnQuitButtonClick()
        {
            Application.Quit();
        }
    }
}