using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public void OnTouch_TM()
    {

        if (OnClicked != null)
        {
            OnClicked();
        }
    }

}
