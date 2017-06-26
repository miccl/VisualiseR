using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for the buttons.
    /// </summary>
    public static class ButtonUtil
    {
        /// <summary>
        /// Label for the submit button.
        /// </summary>
        public static readonly string SUBMIT = "Submit";
        /// <summary>
        /// Label for the cancel button.
        /// </summary>
        public static readonly string CANCEL = "Cancel";

        /// <summary>
        /// Returns <c>true</c> if submit button is pressed.
        /// More formally, returns <c>true</c> if the <see cref="SUBMIT"/> button or fire1 button is pressed.
        /// </summary>
        /// <returns><c>true</c> if submit button pressed.</returns>
        public static bool IsSubmitButtonPressed()
        {
            return Input.GetButtonDown(SUBMIT) || Input.GetButtonDown("Fire1");
        }
        
        /// <summary>
        /// Returns <c>true</c> if canel button is pressed.
        /// More formally, returns <c>true</c> if the <see cref="CANCEL"/> button is pressed.
        /// </summary>
        /// <returns><c>true</c> if cancel button pressed.</returns>
        public static bool IsCancelButtonPressed()
        {
            return Input.GetButtonDown(CANCEL);
        }

    }
}