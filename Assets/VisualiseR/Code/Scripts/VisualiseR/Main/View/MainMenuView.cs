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

    protected override void Start()
    {
        GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
        _settingsPanel = menuCanvas.transform.FindChild("SettingsPanel").gameObject;
        _helpPanel = menuCanvas.transform.FindChild("HelpPanel").gameObject;
        _createRoomPanel = menuCanvas.transform.FindChild("CreateRoomPanel").gameObject;
        _joinRoomPanel = menuCanvas.transform.FindChild("JoinRoomPanel").gameObject;


        audioImage = transform.FindChild("AudioButton").GetComponent<Image>();

//        _settingsPanel = GameObject.Find("SettingsPanel");
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
        isAudioOn = !isAudioOn;
        if (isAudioOn)
        {
            audioImage.sprite = audioOnSprite;
            //TODO how to toggle audio
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