using UnityEngine;
using UnityEngine.UI;

namespace VisualiseR.Util
{
    public static class ButtonUtil
    {
        public static readonly string SUBMIT = "Submit";
        public static readonly string CANCEL = "Cancel";

        public static bool SubmitPressed()
        {
            return Input.GetButtonDown(SUBMIT) || Input.GetButtonDown("Fire1");
        }
        
        public static bool CancelPressed()
        {
            return Input.GetButtonDown(CANCEL);
        }

    }
}