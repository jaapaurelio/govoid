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

		int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);

		// First time we enter here
		if(availableLevels == 0) {
			availableLevels = 20;
			PlayerPrefs.SetInt(Constants.PS_AVAIABLE_LEVELS, 20);
		}
	
		Debug.Log("date" + DateTime.Now.ToString());

		var d = DateTime.Now.ToString();
		Debug.Log(DateTime.Parse(d) );
		// Some people can have more levels done in updates
		if(levelsDone.Count > availableLevels) {
			availableLevels = levelsDone.Count;
		}

		Debug.Log("LEVEL DONE" + levelsDone.Count);
		Debug.Log("availableLevels" + availableLevels);

		// All levels are done, must generate news
		if(levelsDone.Count == availableLevels) {

			toShowAdButton = true;
			string dateToGenerate = PlayerPrefs.GetString(Constants.PS_DATE_TO_GENERATE_LEVELS);

			Debug.Log("dateToGenerate" + dateToGenerate);
			// First time in this stage.
			if(dateToGenerate == "") {
				// Generate levels in a few minutes
				PlayerPrefs.SetString(Constants.PS_DATE_TO_GENERATE_LEVELS, DateTime.Now.AddMinutes(Constants.MINUTES_TO_GENERATE_LEVELS).ToString());
			}

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

		// scroll to the right place
		float per = 1.0f - (levelsDone.Count * 1.0f) / (availableLevels * 1.0f);
		GameObject.Find ("LevelList").GetComponent<ScrollRect>().verticalNormalizedPosition = per;

	}

	public void Update() {

		if(toShowAdButton) {
			if (Advertisement.IsReady("rewardedVideo")) {
				generatingLevelObject.SetActive(true);
			} else {
				generatingLevelObject.SetActive(false);
			}
				
			string dateToGenerate = PlayerPrefs.GetString(Constants.PS_DATE_TO_GENERATE_LEVELS);

			DateTime date = DateTime.Parse(dateToGenerate);

			System.TimeSpan timeLeft = date.Subtract(DateTime.Now);

			if(timeLeft.Minutes <= 0) {
				AddMoreLevels();
			}

		} else {
			generatingLevelObject.SetActive(false);
		}
	}

	private void AddMoreLevels() {
		int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);

		availableLevels += 20;

		PlayerPrefs.SetInt(Constants.PS_AVAIABLE_LEVELS, availableLevels);
		PlayerPrefs.SetString(Constants.PS_DATE_TO_GENERATE_LEVELS, "");

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
