using UnityEngine;
using System.Collections;

public class PrevLevelBtn : MonoBehaviour
{

    public GameObject boardManagerInfinity;

    public void OnTouch_TM()
    {
        boardManagerInfinity.GetComponent<BoardManagerInfinity>().GoPrevLevel();
    }
}
