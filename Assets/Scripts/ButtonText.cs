using UnityEngine;
using System.Collections;

abstract public class ButtonText : MonoBehaviour
{
    private const float SCALE = 0.01f;

    public abstract void OnTouch();

    public void OnTouch_TM()
    {
        transform.localScale -= new Vector3(SCALE, SCALE, 0);
        OnTouch();
    }

    public void OnTouchBegan_TM()
    {
        transform.localScale += new Vector3(SCALE, SCALE, 0);
    }

    public void OnTouchLeave_TM()
    {
        transform.localScale -= new Vector3(SCALE, SCALE, 0);
    }
}
