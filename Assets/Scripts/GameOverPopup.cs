using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class GameOverPopup : MonoBehaviour
{

    private bool isOpen = false;
    private bool toShowAdButton = true;
    public GameObject adButtonGroup;
    public GameObject boardManagerTimeAttack;
    public GameObject clickBlockerObject;
    public GameObject gameOverScoreObject;
    public GameObject gameOverNewHighscore;
    public GameObject bestScoreGameOverObject;


    void Awake()
    {
        transform.position = new Vector3(90, 0, -2);
    }

    public void Hide()
    {
        transform.position = new Vector3(90, 0, -2);
        isOpen = false;
    }

    public void Show(int score, bool isHighscore, bool showAd)
    {
        isOpen = true;
        clickBlockerObject.SetActive(true);
        gameOverScoreObject.GetComponent<TextMesh>().text = score.ToString();
        bestScoreGameOverObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInTimeAttack").ToString();


        if (isHighscore)
        {
            gameOverNewHighscore.SetActive(true);
        }
        else
        {
            gameOverNewHighscore.SetActive(false);
        }

        toShowAdButton = showAd;

        transform.position = new Vector3(0, 0, -2);

        gameObject.GetComponent<Animator>().Play("GameOverShow");

        StartCoroutine(EnableClick());
    }

    protected IEnumerator EnableClick()
    {

        yield return new WaitForSeconds(1f);
        clickBlockerObject.SetActive(false);

    }

    public void Update()
    {
        if (!isOpen)
        {
            return;
        }

        if (toShowAdButton)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                adButtonGroup.SetActive(true);
            }
            else
            {
                adButtonGroup.SetActive(false);
            }
        }
    }


    public void ShowAd()
    {

        if (Advertisement.IsReady("rewardedVideo"))
        {
            GameManager.instance.googleAnalytics.LogEvent("ShowAdExtraTime", "IsReady", "", 0);
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            GameManager.instance.googleAnalytics.LogEvent("ShowAdExtraTime", "NotReady", "", 0);
        }
    }

    private void HandleShowResult(ShowResult result)
    {

        toShowAdButton = false;
        switch (result)
        {
            case ShowResult.Finished:

                GameManager.instance.googleAnalytics.LogEvent("ShowAdExtraTimeFinished", "", "", 0);
                adButtonGroup.SetActive(false);

                boardManagerTimeAttack.GetComponent<BoardManagerTimeAttack>().KeepPlaying();

                Debug.Log("The ad was successfully shown.");

                break;
            case ShowResult.Skipped:

                GameManager.instance.googleAnalytics.LogEvent("ShowAdExtraTimeSkipped", "", "", 0);
                Debug.Log("The ad was skipped before reaching the end.");
                adButtonGroup.SetActive(false);

                break;
            case ShowResult.Failed:

                GameManager.instance.googleAnalytics.LogEvent("ShowAdExtraTimeFailed", "", "", 0);
                Debug.LogError("The ad failed to be shown.");
                adButtonGroup.SetActive(false);

                break;
        }
    }


}
