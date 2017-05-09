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

    protected override void Start()
    {
        GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
        _settingsPanel = menuCanvas.transform.FindChild("SettingsPanel").gameObject;

        audioImage = transform.FindChild("AudioButton").GetComponent<Image>();

//        _settingsPanel = GameObject.Find("SettingsPanel");
    }

    public void OnCreateRoomButtonClick()
    {
        //TODO
        Debug.Log("OnCreateRoomButtonClick");
    }

    public void OnJoinRoomButtonClick()
    {
        //TODO
        Debug.Log("OnJoinRoomButtonClick");
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
        //TODO
        Debug.Log("OnHelpButtonClick");
    }

    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
//        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}