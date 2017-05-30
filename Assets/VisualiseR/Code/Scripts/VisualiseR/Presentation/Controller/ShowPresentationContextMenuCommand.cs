using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

public class ShowPresentationContextMenuCommand : Command
{
    private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowPresentationContextMenuCommand));

    [Inject]
    public Player _player { get; set; }
    
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject _contextView { get; set; }

    public override void Execute()
    {
        if (!_player.HasRight(AcessList.ContextMenu))
        {
            Logger.InfoFormat(AcessList.errorMessageFormat, _player, typeof(ShowPresentationContextMenuCommand));
            return;
        }

        ShowContextMenu();
    }

    private void ShowContextMenu()
    {
        var sceneMenu = _contextView.transform.Find("Menus").transform.Find("PresentationContextMenuCanvas").gameObject;
        sceneMenu.SetActive(true);
        Logger.InfoFormat("Context menu is shown");
    }
}