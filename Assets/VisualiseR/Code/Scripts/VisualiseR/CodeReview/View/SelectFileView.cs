using System.Runtime.InteropServices;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{


    public class SelectFileView : View
    {
        public Signal<string> _selectedFileSignal = new Signal<string>();

        //TODO read readme of this guy
        [SerializeField] private GUISkin _skin;
        [SerializeField] private Texture2D file,folder,back,drive;

        public FileBrowser fb;
        private string output;

        private bool wasSelected = false;

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
            if (fb != null)
            {
                if (fb.draw())
                {
                    //true is returned when a file has been selected
                    //the output file is a member if the FileInfo class, if cancel was selected the value is null
                    var selectedFile = fb.outputFile;
                    if (selectedFile == null)
                    {
                        _selectedFileSignal.Dispatch(null);
                        Debug.Log("Cancel hit");
                        return;
                    }

                    var output = selectedFile.Directory;
                    if (output != null)
                    {
                        Debug.Log("Ouput File = \"" + output.FullName + "\"");
                        _selectedFileSignal.Dispatch(output.FullName);
                    }
                }
            }
        }
    }
}