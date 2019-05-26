using UnityEngine;
using System.Collections;

public class GoBackButton : MonoBehaviour
{

    public void OnTouch_TM()
    {
        GameManager.instance.GoBack();
    }
}
