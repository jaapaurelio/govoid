using UnityEngine;
using System.Collections;

public class TapToStart : MonoBehaviour
{

    public void OnTouch_TM()
    {
        GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().StartFromTap();
    }
}
