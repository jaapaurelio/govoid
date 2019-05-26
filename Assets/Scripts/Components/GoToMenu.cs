using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{

    public void OnTouch_TM()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
