using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class SelectDiskFileMediator : Mediator
    {
        [Inject]
        public SelectDiskFileView _view{ get; set;}


        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }


        public override void OnRegister()
        {
            _view._selectedFileSignal.AddListener(OnFileSelected);
            _view.Init();
        }

        public override void OnRemove()
        {
            _view._selectedFileSignal.RemoveListener(OnFileSelected);

        }

        private void OnFileSelected(string filePath)
        {
            if (filePath != null)
            {
                LoadFilesSignal.Dispatch(filePath);
            }
            else
            {
                //TODO Hinweismeldung, dass nicht ausgewählt wurde?!
            }
            Destroy();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}