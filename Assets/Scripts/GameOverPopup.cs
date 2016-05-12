using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class GameOverPopup : MonoBehaviour {

	private bool isOpen = false;
	private bool toShowAdButton = true;
	public GameObject adButtonGroup;
	public GoogleAnalyticsV3 googleAnalytics;
	public GameObject boardManagerTimeAttack;

	void Awake(){
		transform.position = new Vector3(0, 0, -2);
	}

	public void Hide() {
		transform.position = new Vector3(90, 0, -2);
		isOpen = false;
	}

	public void Show(int score, bool isHighscore, bool showAd) {
		isOpen = true;
		transform.Find("GameOverScore").GetComponent<TextMesh>().text = score + " levels done";

		if(isHighscore) {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(true);
		} else {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(false);
		} 

		toShowAdButton = showAd;

		transform.position = new Vector3(0, 0, -2);
	}

	public void Update() {
		if(!isOpen) {
			return;
		}
			
		if(toShowAdButton) {
			if (Advertisement.IsReady("rewardedVideo")) {
				adButtonGroup.SetActive(true);
			} else {
				adButtonGroup.SetActive(false);
			}
		}
	}


	public void ShowAd() {

		if (Advertisement.IsReady("rewardedVideo"))
		{
			googleAnalytics.LogEvent("ShowAdExtraTime", "IsReady", "", 0);
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		} else {
			googleAnalytics.LogEvent("ShowAdExtraTime", "NotReady", "", 0);
		}
	}

	private void HandleShowResult(ShowResult result){

		toShowAdButton = false;
		switch (result) {
		case ShowResult.Finished:

			googleAnalytics.LogEvent("ShowAdExtraTimeFinished", "", "", 0);
			adButtonGroup.SetActive(false);

			boardManagerTimeAttack.GetComponent<BoardManagerTimeAttack>().KeepPlaying();

			Debug.Log("The ad was successfully shown.");

			break;
		case ShowResult.Skipped:

			googleAnalytics.LogEvent("ShowAdExtraTimeSkipped", "", "", 0);
			Debug.Log("The ad was skipped before reaching the end.");
			adButtonGroup.SetActive(false);

			break;
		case ShowResult.Failed:

			googleAnalytics.LogEvent("ShowAdExtraTimeFailed", "", "", 0);
			Debug.LogError("The ad failed to be shown.");
			adButtonGroup.SetActive(false);

			break;
		}
	}


}
