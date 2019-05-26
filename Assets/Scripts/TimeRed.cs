using UnityEngine;
using System.Collections;

public class TimeRed : MonoBehaviour
{

    float lastWorldScreenHeight = 0;


    // Use this for initialization
    void Start()
    {

        ResizeSpriteToScreen();
    }


    void Update()
    {

        // If a resize happen, we need to calculate the background size again
        if (lastWorldScreenHeight != GetWorldScreenHeight())
        {
            lastWorldScreenHeight = GetWorldScreenHeight();
            ResizeSpriteToScreen();
        }
    }

    private void ResizeSpriteToScreen()
    {
        float worldScreenHeight = GetWorldScreenHeight();
        transform.Find("TimeRedBottom").transform.localPosition = new Vector3(0, -worldScreenHeight, 0);
        transform.Find("TimeRedTop").transform.localPosition = new Vector3(0, worldScreenHeight, 0);
    }

    private float GetWorldScreenHeight()
    {
        // orthographicSize is half the screen height, so we multiply by 2
        return Camera.main.orthographicSize;
    }


}
