using UnityEngine;
using System.Collections;

public class AdButtonExtraTime : MonoBehaviour
{

    public void OnTouch_TM()
    {
        GameObject.Find("GameOverPopup").GetComponent<GameOverPopup>().ShowAd();
    }
}
