using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Drag and dropable object.
    /// </summary>
    public class DdObject : View, DragDropHandler
    {
        private bool IsHeld;
        private GameObject Reticle;
        internal bool ddIsActive = true;

        void Start()
        {
            base.Start();
            Init();
        }

        public void Init()
        {
            IsHeld = false;
            Reticle = GameObject.Find("GvrReticlePointer");
        }

        public void HandleGazeTriggerStart()
        {
            if (!ddIsActive) return;
            IsHeld = true;
            ChangeColorValue(1.25f);
        }

        public void HandleGazeTriggerEnd()
        {
            if (!ddIsActive) return;

            IsHeld = false;
            ChangeColorValue(0.8f);
        }

        public void Update()
        {
            if (!ddIsActive) return;

            if (IsHeld)
            {
                Ray ray = new Ray(Reticle.transform.position, Reticle.transform.forward);
                float distance = Vector3.Distance(Reticle.transform.position, transform.position);
                transform.position = ray.GetPoint(distance);
            }
        }

        private void ChangeColorValue(float rate)
        {
            var color = GetComponent<Renderer>().material.color;
            color.a*=rate;
            color.b*=rate;
            color.g*=rate;
            GetComponent<Renderer>().material.color = color;
        }
    }
}