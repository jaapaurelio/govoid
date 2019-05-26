using UnityEngine;
using System.Collections;

public class AdGenerateLevelBtn : MonoBehaviour
{

    public GameObject selectLevelManagerObject;

    public void OnTouch_TM()
    {

        selectLevelManagerObject.GetComponent<SelectLevelManager>().ShowAd();
    }
}
