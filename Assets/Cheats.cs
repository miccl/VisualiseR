using UnityEngine;

public class Cvheats : MonoBehaviour
{
    private bool vrModeEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("VrMode"))
        {
            vrModeEnabled = !vrModeEnabled;
            GvrViewer.Instance.VRModeEnabled = vrModeEnabled;
        }
    }
}