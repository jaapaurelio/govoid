using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayTimeAttack : ButtonText
{

    override public void OnTouch()
    {
        SceneManager.LoadScene("TimeAttackScene");
    }

}
