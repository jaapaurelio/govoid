using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using DateTime = System.DateTime;

public class SelectLevelManager : MonoBehaviour {

	public GameObject levelButtonGameObject;
	public GameObject levelGridContainerObject;
	public GameObject generatingLevelObject;

	// Use this for initialization
	void Start () {


		// To be possible open game in any scene
		if(GameManager.instance == null) {
			SceneManager.LoadScene("MainMenuScene");
			return;
		}

		Debug.Log("1 " + Time.realtimeSinceStartup);
		List<int> levelsDone =  GameManager.instance.playerStatistics.GetLevelsDoneFromPackage(GameManager.instance.currentPackageNum);

		int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);

		// Some people can have more levels done in updates
		if(levelsDone.Count > availableLevels) {
			availableLevels = levelsDone.Count;
		}

		Debug.Log("LEVEL DONE" + levelsDone.Count);
		Debug.Log("availableLevels" + availableLevels);

		// All levels are done, must generate news
		if(levelsDone.Count == availableLevels && availableLevels < Constants.MAX_NUMBER_OF_LEVELS) {
			AddMoreLevels();
		}

		Debug.Log("2 - " + Time.realtimeSinceStartup);

		Debug.Log("availableLevels AFTER" + availableLevels);

		for(int i = 1; i <= availableLevels; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelButton =
				Instantiate (levelButtonGameObject, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			bool done = false;

			if(levelsDone.Contains(i)) {
				done = true;
			}

			levelButton.GetComponent<LevelSelectButton>().SetLevel(i, done);

			// This will put the object in the right position inside parent
			levelButton.transform.SetParent(levelGridContainerObject.transform, false);
		}

		// scroll to the right place
		float per = 1.0f - (levelsDone.Count * 1.0f) / (availableLevels * 1.0f);
		GameObject.Find("LevelList").GetComponent<ScrollRect>().verticalNormalizedPosition = per;

		GameObject.Find("NumberOfLevels").GetComponent<TextMesh>().text =  levelsDone.Count + "/" + Constants.MAX_NUMBER_OF_LEVELS;

		// Show button to generate more levels
		if(availableLevels < Constants.MAX_NUMBER_OF_LEVELS){
			StartCoroutine(CheckAdButton());
		} else {
			generatingLevelObject.SetActive(false);
			GameObject.Find("BottomMessage").SetActive(false);
		}

		Debug.Log("3 - " + Time.realtimeSinceStartup);
	}

	public IEnumerator CheckAdButton() {

		while(true) {
			
			if (Advertisement.IsReady("rewardedVideo")) {
				generatingLevelObject.SetActive(true);
			} else {
				generatingLevelObject.SetActive(false);
			}

			yield return new WaitForSeconds(2f);
		}

	}

	private void AddMoreLevels() {
		int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);

		// At the begining we generate more because levels are shorter.
		if (availableLevels < 30) {
			availableLevels += Constants.NUMBER_OF_LEVELS_TO_GENERATE_FIRST_TIME;
		} else {
			availableLevels += Constants.NUMBER_OF_LEVELS_TO_GENERATE;
		}

		if(availableLevels > Constants.MAX_NUMBER_OF_LEVELS) {
			availableLevels = Constants.MAX_NUMBER_OF_LEVELS;
		}

		PlayerPrefs.SetInt(Constants.PS_AVAIABLE_LEVELS, availableLevels);

		// TODO: for now just restart to load all new levels
		SceneManager.LoadScene(Constants.SELECT_LEVEL_SCENE);
	}

	public void ShowAd() {

		if (Advertisement.IsReady("rewardedVideo"))
		{
			//googleAnalytics.LogEvent("ShowAd", "IsReady", "", 0);
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		} else {
			//googleAnalytics.LogEvent("ShowAd", "NotReady", "", 0);
		}
	}

	private void HandleShowResult(ShowResult result){

		switch (result) {
		case ShowResult.Finished:
			
			Debug.Log("The ad was successfully shown.");

			AddMoreLevels();
			break;

		case ShowResult.Skipped:
			
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

}
