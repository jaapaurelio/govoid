using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    int level = 0;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { BtnClicked(); });
    }

    public void SetLevel(int num, bool levelDone)
    {
        level = num;
        gameObject.GetComponentInChildren<Text>().text = num.ToString();

        if (levelDone)
        {
            gameObject.GetComponent<Image>().color = new Color32(1, 225, 137, 255);
            gameObject.GetComponentInChildren<Text>().color = new Color32(34, 122, 80, 255);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(93, 93, 93, 255);
            gameObject.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        }

    }

    public void BtnClicked()
    {
        GameManager.instance.currentLevelFromPackage = level;
        SceneManager.LoadScene("InfinityScene");
    }
}
