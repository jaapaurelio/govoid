using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class BoostPopup : MonoBehaviour {

	public GameObject boardManagerTimeAttack;
	public GameObject viewAdText;
	public GameObject adResultText;
	public GoogleAnalyticsV3 googleAnalytics;

	private bool toShowAdButton = true;
	private bool isOpen = false;

	public void HidePopup() {
		transform.position = new Vector3(90, 90, 0);
		isOpen = false;
	}

	public void ShowPopup() {
		isOpen = true;
		toShowAdButton = false;
		transform.position = new Vector3(0, 0, 0);

		viewAdText.SetActive(false);
		adResultText.SetActive(false);
	}

	public void Update() {
		if(!isOpen) {
			return;
		}

		if(toShowAdButton) {
			if (Advertisement.IsReady("rewardedVideo")) {
				viewAdText.SetActive(true);
			} else {
				viewAdText.SetActive(false);
			}
		}
	}

	public void ShowAd() {
		
		if (Advertisement.IsReady("rewardedVideo"))
		{
			googleAnalytics.LogEvent("ShowAd", "IsReady", "", 0);
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		} else {
			googleAnalytics.LogEvent("ShowAd", "NotReady", "", 0);
		}
	}

	private void HandleShowResult(ShowResult result){

		toShowAdButton = false;
		switch (result) {
		case ShowResult.Finished:

			googleAnalytics.LogEvent("ShowAdFinished", "", "", 0);
			viewAdText.SetActive(false);
			adResultText.SetActive(true);

			int extraTime = Random.Range(10, 30);
			
			adResultText.GetComponent<TextMesh>().text = "You will have\n" + extraTime + " seconds extra!";

			boardManagerTimeAttack.GetComponent<BoardManagerTimeAttack>().ExtraTime(extraTime);

			Debug.Log("The ad was successfully shown.");

			break;
		case ShowResult.Skipped:

			googleAnalytics.LogEvent("ShowAdSkipped", "", "", 0);
			Debug.Log("The ad was skipped before reaching the end.");
			viewAdText.SetActive(false);

			break;
		case ShowResult.Failed:

			googleAnalytics.LogEvent("ShowAdFailed", "", "", 0);
			Debug.LogError("The ad failed to be shown.");
			viewAdText.SetActive(false);

			break;
		}
	}

	public void DontShowAd() {
		boardManagerTimeAttack.GetComponent<BoardManagerTimeAttack>().StartNewGame();
	}
}
