using UnityEngine;


public class FitSpriteToScreen : MonoBehaviour
{
    
    public int targetWidth=2040;
    public float pixelsToUnits = 98.5f;


    private void Update()
    {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
        Camera cam = Camera.main;
        cam.orthographicSize = height / pixelsToUnits / 2;
    }
}
