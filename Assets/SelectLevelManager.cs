using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class SelectLevelManager : MonoBehaviour {

	public GameObject levelButtonGameObject;
	public GameObject levelGridContainerObject;
	public GameObject generatingLevelObject;

	private bool toShowAdButton = false;

	// Use this for initialization
	void Start () {

		// To be possible open game in any scene
		if(GameManager.instance == null) {
			SceneManager.LoadScene("MainMenuScene");
			return;
		}

		TextAsset bindata= Resources.Load<TextAsset>("Levels/Pack" + GameManager.instance.currentPackageNum);

		Pack p = JsonUtility.FromJson<Pack>(bindata.text);
		GameManager.instance.currentPackage = p;

		List<int> levelsDone =  GameManager.instance.playerStatistics.GetLevelsDoneFromPackage(GameManager.instance.currentPackageNum);

		int availableLevels = PlayerPrefs.GetInt(Constants.AVAIABLE_LEVELS);

		if(availableLevels == 0) {
			availableLevels = 20;
			PlayerPrefs.SetInt(Constants.AVAIABLE_LEVELS, 20);
		}
	
		// Some people can have more levels done in updates
		if(levelsDone.Count > availableLevels) {
			availableLevels = levelsDone.Count;
		}

		Debug.Log("LEVEL DONE" + levelsDone.Count);
		Debug.Log("availableLevels" + availableLevels);

		if(levelsDone.Count == availableLevels) {
			Debug.Log("show ad");
			toShowAdButton = true;
		} else {
			Debug.Log("hide ad");
			toShowAdButton = false;
		}

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

	}

	public void Update() {

		if(toShowAdButton) {
			if (Advertisement.IsReady("rewardedVideo")) {
				generatingLevelObject.SetActive(true);
			} else {
				generatingLevelObject.SetActive(false);
			}
		} else {
			generatingLevelObject.SetActive(false);
		}

	}

	private void AddMoreLevels() {
		int availableLevels = PlayerPrefs.GetInt(Constants.AVAIABLE_LEVELS);

		availableLevels += 20;

		PlayerPrefs.SetInt(Constants.AVAIABLE_LEVELS, availableLevels);

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

		//toShowAdButton = false;
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
