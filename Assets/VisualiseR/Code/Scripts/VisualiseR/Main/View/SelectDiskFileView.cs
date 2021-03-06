﻿using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace VisualiseR.Main
{
    /// <summary>
    /// View for the disk file dialog.
    /// Uses <see cref="FileBrowser"/>.
    /// </summary>
    public class SelectDiskFileView : View
    {
        private JCsLogger Logger;

        public Signal<string> SelectedFileSignal = new Signal<string>();

        [SerializeField] private GUISkin _skin;

        [SerializeField] private Texture2D file, folder, back, drive;

        private FileBrowser fb;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(SelectDiskFileView));
        }

        internal void Init()
        {
            fb = new FileBrowser(Application.persistentDataPath); //starting directory
            fb.guiSkin = _skin; //set the starting skin

            fb.fileTexture = file;
            fb.directoryTexture = folder;
            fb.backTexture = back;
            fb.driveTexture = drive;


//            fb = new FileBrowser(1); //starting directory
//
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                fb.guiSkin.button.fontSize = 100;
                fb.guiSkin.verticalScrollbar.fixedWidth = 75;
                fb.guiSkin.verticalScrollbarThumb.fixedWidth = 75;
            }
        }

        void OnGUI()
        {
            if (fb != null && fb.draw())
            {
                //true is returned when a file has been selected
                //the output file is a member if the FileInfo class, if cancel was selected the value is null
                var selectedFile = fb.outputFile;
                if (selectedFile == null)
                {
                    SelectedFileSignal.Dispatch(null);
                    Logger.Debug("Cancel hit");
                    return;
                }

                var output = selectedFile.Directory;
                if (output != null)
                {
                    Logger.DebugFormat("Ouput File ='{0}'", output.FullName);
                    SelectedFileSignal.Dispatch(output.FullName);
                }
            }
        }
    }
}