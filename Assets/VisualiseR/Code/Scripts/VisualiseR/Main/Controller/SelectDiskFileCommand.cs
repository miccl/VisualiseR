﻿using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Main
{
    public class SelectDiskFileCommand : Command
    {

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        public override void Execute()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("FileBrowser") as GameObject);
            go.name = "FileBrowser";
            go.transform.parent = contextView.transform;
        }
    }
}