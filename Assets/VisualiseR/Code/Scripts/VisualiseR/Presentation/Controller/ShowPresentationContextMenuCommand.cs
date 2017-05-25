using strange.extensions.command.impl;
using UnityEngine;

public class ShowPresentationContextMenuCommand : Command
{
    [Inject]
    public GameObject _gameObject { get; set; }

    public override void Execute()
    {
        InstantiateContextMenu();
    }

    private void InstantiateContextMenu()
    {
        var position = GetContextMenuPosition();
        var rotation = GetContextMenuRotation();
        GameObject contextMenu =
            GameObject.Instantiate(Resources.Load("PresentationContextMenuCanvas"), position, rotation) as GameObject;
        contextMenu.transform.Rotate(90, -180, 0);
        contextMenu.transform.SetParent(_gameObject.transform);
    }

    private Quaternion GetContextMenuRotation()
    {
        //TODO überarbeiten
        return _gameObject.transform.rotation;
    }

    private Vector3 GetContextMenuPosition()
    {
        //TODO irgendwann nochmal verbessern, derzeit schwankt das immer hin und her
        Vector3 cameraBack = -Camera.main.transform.forward * 12;
        Vector3 shift = new Vector3(0, 0, cameraBack.z);
        Vector3 pos = _gameObject.transform.position + shift;
        pos.y = 2;

        return pos;
    }
}