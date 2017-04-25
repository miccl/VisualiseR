using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualiseR.Common
{


    public class SelectFile : MonoBehaviour
    {
        //TODO read readme of this guy
        private GUISkin[] skins;

        private FileBrowser fb;
        private string output;

        private bool wasSelected = false;

        void Start()
        {
            fb = new FileBrowser();
            fb.guiSkin = skins[0]; //set the starting skin
//        FileBrowser fb = new FileBrowser(string startingDirectory); //starting directory
//        FileBrowser.searchDirectory(DirectoryInfo di,string sp,bool recursive); //This is a public function if you want to do your own search on a given directory, using a search pattern, and the
        }

        void OnGUI()
        {
            if (!wasSelected)
            {
                if (fb.draw())
                {
                    //true is returned when a file has been selected
                    //the output file is a member if the FileInfo class, if cancel was selected the value is null
                    if (fb.outputFile == null)
                    {
                        Debug.Log("Cancel hit");
                    }
                    else
                    {
                        Debug.Log("Ouput File = \"" + fb.outputFile + "\"");
                    }
                    wasSelected = true;
                }
            }
        }
    }
}