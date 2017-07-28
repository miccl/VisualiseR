using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Drag and dropable object.
    /// </summary>
    public class DDObject : MonoBehaviour, DragDropHandler
    {
        private bool IsHeld;
        private GameObject Reticle;

        void Start()
        {
            IsHeld = false;
            GetComponent<Renderer>().material.color = Color.yellow;
            Reticle = GameObject.Find("GvrReticlePointer");
        }

        public void HandleGazeTriggerStart()
        {
            IsHeld = true;
            GetComponent<Renderer>().material.color = Color.blue;
        }

        public void HandleGazeTriggerEnd()
        {
            IsHeld = false;
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        void Update()
        {
            if (IsHeld)
            {
                Ray ray = new Ray(Reticle.transform.position, Reticle.transform.forward);
                float distance = Vector3.Distance(Reticle.transform.position, transform.position);
                transform.position = ray.GetPoint(distance);
            }
        }
    }
}