using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class BoardManagerInfinity : BoardManager
{

	public override void Start(){
		// To be possible open game in any scene
		if(GameManager.instance == null) {
			SceneManager.LoadScene("MainMenuScene");
			return;
		}

		base.Start();


		googleAnalytics.LogScreen("InfinityMode");

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

		GameObject.Find("CurrentLevel").GetComponent<TextMesh>().text = GameManager.instance.currentLevelFromPackage.ToString();


		NewLevel();
	}
		
	protected override void LostLevel() {
		base.LostLevel();
		googleAnalytics.LogEvent("InfinityMode", "NoExitHouse", "", 0);
	}
		
	protected override void WonLevel() {
		base.WonLevel();

		GameManager.instance.playerStatistics.SetLevelDone( GameManager.instance.currentPackageNum, GameManager.instance.currentLevelFromPackage);

		GameManager.instance.currentLevelFromPackage++;

		googleAnalytics.LogEvent("InfinityMode", "NextLevel", "Score", GameManager.instance.currentLevelFromPackage);

		GameObject.Find("CurrentLevel").GetComponent<TextMesh>().text = GameManager.instance.currentLevelFromPackage.ToString();

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

		GameObject.Find("Messages").GetComponent<TextMesh>().text = currentLevelGrid.message;

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