using UnityEngine;

public class LoadingEffect : MonoBehaviour
{
    public bool loading = false;

    public Texture loadingTexture;

    public float size = 70;

    private float rotAngle;

    public float rotSpeed = 300;


    // Update is called once per frame
    void Update()
    {
        if (loading)
        {
            rotAngle += rotSpeed * Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (loading)
        {
            var pivot = new Vector2(Screen.width / 2, Screen.height / 2);
            GUIUtility.RotateAroundPivot(rotAngle % 360, pivot);
            GUI.DrawTexture(new Rect((Screen.width - size) / 2, (Screen.height - size) / 2, size, size),
                loadingTexture);
        }
    }
}