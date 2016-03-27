using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class BoardManagerInfinity : BoardManager
{

	private int levelsCompleted = 0;

	public override void Start(){
		base.Start();

		googleAnalytics.LogScreen("InfinityMode");

		int bestScore = PlayerPrefs.GetInt("BestScoreInInfinityMode");

		Debug.Log("Best score: " + bestScore);

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = bestScore.ToString();

		NewGame();
	}

	public void Update() {
		if (playing) {
			base.BoardInteraction();
		}
	}
		

	public override void RestartGame() {
		base.RestartGame();

		if(!playing) {
			return;
		}

		googleAnalytics.LogEvent("InfinityMode", "RestartGame", "", 0);

	}

	public override void NewGame() {
		base.NewGame();

		googleAnalytics.LogEvent("InfinityMode", "StartNewGame", "", 0);

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInInfinityMode").ToString();

		levelsCompleted = 0;
		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();


		NewLevel();
	}
		
	protected override void LostLevel() {
		base.LostLevel();
		googleAnalytics.LogEvent("InfinityMode", "NoExitHouse", "", 0);
	}
		
	protected override void WonLevel() {
		base.WonLevel();

		GameManager.instance.playerStatistics.SetLevelDone( GameManager.instance.currentPackageNum, GameManager.instance.currentLevelFromPackage);

		levelsCompleted++;

		GameManager.instance.currentLevelFromPackage++;

		googleAnalytics.LogEvent("InfinityMode", "NextLevel", "Score", levelsCompleted);

		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		if(levelsCompleted > PlayerPrefs.GetInt("BestScoreInInfinityMode")){
			PlayerPrefs.SetInt("BestScoreInInfinityMode", levelsCompleted);
			GameObject.Find("BestScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();
			googleAnalytics.LogEvent("InfinityMode", "NextLevel", "NewBestScore", levelsCompleted);

		}
			
		if(GameManager.instance.currentLevelFromPackage <= GameManager.instance.currentPackage.levels.Length){
			NewLevel();
		} else {
			Debug.Log("NO MORE LEVELS IN THS PACK");
			SceneManager.LoadScene("SelectPackageScene");
		}

	}

	protected override void NewLevel() {

		base.DestroyCurrentLevel();

		int levelNumber = GameManager.instance.currentLevelFromPackage;
		Pack package = GameManager.instance.currentPackage;

		Debug.Log("levelNumber"+ levelNumber);

		currentLevelGrid = LevelJsonGenerator.CreateLevel(package, levelNumber);

		base.NewLevel();

	}

	public override void PauseGame() {
		base.PauseGame();

		googleAnalytics.LogEvent("InfinityMode", "Pause", "", 0);
	}
		
	public override void ClosePausePopup() {
		base.ClosePausePopup();
		googleAnalytics.LogEvent("InfinityMode", "UnPause", "", 0);
	}

}