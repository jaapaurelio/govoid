using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayInfinity : ButtonText
{

    public override void OnTouch()
    {
        SceneManager.LoadScene(Constants.SELECT_LEVEL_SCENE);
    }
}
