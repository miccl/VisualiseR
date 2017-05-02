using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace VisualiseR.Main

{
    public class CreateRoomView : View
    {
        public Signal _selectDiskFileButtonClickedSignal = new Signal();


        /// <summary>
        /// Initialise the view.
        /// </summary>
        internal void Init()
        {
        }


        public void ChooseDiskFileButtonClicked()
        {
            _selectDiskFileButtonClickedSignal.Dispatch();
        }
    }
}