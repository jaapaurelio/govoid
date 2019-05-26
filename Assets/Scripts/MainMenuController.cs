using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public virtual void Awake()
    {
        // To be possible open game in any scene
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene("IntroScene");
            return;
        }
    }

    void Start()
    {

        GameManager.instance.googleAnalytics.LogScreen("MainMenu");
    }
}
