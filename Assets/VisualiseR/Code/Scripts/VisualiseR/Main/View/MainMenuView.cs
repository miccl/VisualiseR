using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

public class MainMenuView : View
{
    private GameObject _settingsPanel;

    [SerializeField] private Sprite audioOffSprite;
    [SerializeField] private Sprite audioOnSprite;

    private bool isAudioOn = true;
    private Image audioImage;
    private GameObject _helpPanel;
    private GameObject _createRoomPanel;
    private GameObject _joinRoomPanel;
    private AudioSource _backgroundAudioSource;
    private bool _isMuted;

    protected override void Awake()
    {
        base.Awake();
        GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
        _settingsPanel = menuCanvas.transform.FindChild("SettingsPanel").gameObject;
        _helpPanel = menuCanvas.transform.FindChild("HelpPanel").gameObject;
        _createRoomPanel = menuCanvas.transform.FindChild("CreateRoomPanel").gameObject;
        _joinRoomPanel = menuCanvas.transform.FindChild("JoinRoomPanel").gameObject;

        _backgroundAudioSource = menuCanvas.GetComponent<AudioSource>();


        audioImage = transform.FindChild("AudioButton").GetComponent<Image>();

//        _settingsPanel = GameObject.Find("SettingsPanel");
    }

    protected override void Start()
    {
        base.Start();
        _isMuted = (PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.AUDIO_MUTED, 0) != 0);
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
        _backgroundAudioSource.mute = _isMuted;
        if (!_isMuted)
        {
            audioImage.sprite = audioOnSprite;
            _backgroundAudioSource.PlayDelayed(0);
        }
        else
        {
            audioImage.sprite = audioOffSprite;
        }

    }

    public void OnHelpButtonClick()
    {
        _helpPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();

//#if UNITY_EDITOR
//        EditorApplication.isPlaying = false;
//#else
//        Application.Quit();
//#endif
    }
}