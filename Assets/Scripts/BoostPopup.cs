using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class BoostPopup : MonoBehaviour
{

    public GameObject boardManagerTimeAttack;

    public void HidePopup()
    {
        transform.position = new Vector3(90, 90, 0);
    }

    public void ShowPopup()
    {
        transform.position = new Vector3(0, 0, 0);

    }

    public void DontShowAd()
    {
        boardManagerTimeAttack.GetComponent<BoardManagerTimeAttack>().StartNewGame();
    }
}
