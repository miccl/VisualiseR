using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Drag and dropable object.
    /// </summary>
    public class DDObject : View, DragDropHandler
    {
        private bool IsHeld;
        private GameObject Reticle;
        internal bool ddIsActive = true;

        void Start()
        {
            IsHeld = false;
            GetComponent<Renderer>().material.color = Color.yellow;
            Reticle = GameObject.Find("GvrReticlePointer");
        }

        public void HandleGazeTriggerStart()
        {
            if (!ddIsActive) return;
            IsHeld = true;
            GetComponent<Renderer>().material.color = Color.blue;
        }

        public void HandleGazeTriggerEnd()
        {
            if (!ddIsActive) return;

            IsHeld = false;
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        void Update()
        {
            if (!ddIsActive) return;

            if (IsHeld)
            {
                Ray ray = new Ray(Reticle.transform.position, Reticle.transform.forward);
                float distance = Vector3.Distance(Reticle.transform.position, transform.position);
                transform.position = ray.GetPoint(distance);
            }
        }
    }
}