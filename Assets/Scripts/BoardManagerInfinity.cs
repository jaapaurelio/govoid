﻿using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using UnityEngine.SceneManagement;

public class BoardManagerInfinity : BoardManager
{
	public GameObject endLevelPopup;

	public override void Start(){

		base.Start();

		GameManager.instance.googleAnalytics.LogScreen("InfinityMode");



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

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "RestartGame",  "level" + GameManager.instance.currentLevelFromPackage, 0);

	}

	public override void NewGame() {
		base.NewGame();

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "StartNewGame", "", 0);

		GameObject.Find("CurrentLevel").GetComponent<TextMesh>().text = GameManager.instance.currentLevelFromPackage.ToString();


		NewLevel();
	}
		
	protected override void LostLevel() {
		base.LostLevel();

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "NoExitHouse", "level" + GameManager.instance.currentLevelFromPackage, 0);
	}

		
	protected override void WonLevel() {
		base.WonLevel();

		endLevelPopup.GetComponent<EndLevelPopup>().Show(GameManager.instance.currentLevelFromPackage);

		GameManager.instance.playerStatistics.SetLevelDone( GameManager.instance.currentPackageNum, GameManager.instance.currentLevelFromPackage);

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "WonLevel", "level" + GameManager.instance.currentLevelFromPackage, 0);

		Social.ReportScore(GameManager.instance.playerStatistics.GetNumberOfDoneLevelsFromPackage(1), "CgkI2ab42cEaEAIQBw", (bool success) => {
			// handle success or failure
		});
			
	}

	// End level popup will call this
	public void GoNextLevel() {

		endLevelPopup.GetComponent<EndLevelPopup>().Hide();

		GameManager.instance.currentLevelFromPackage++;

		GameObject.Find("CurrentLevel").GetComponent<TextMesh>().text = GameManager.instance.currentLevelFromPackage.ToString();

		int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);
		if(GameManager.instance.currentLevelFromPackage <= availableLevels ) {//GameManager.instance.currentPackage.levels.Length){
			NewLevel();
		} else {
			Debug.Log("NO MORE LEVELS IN THS PACK");
			SceneManager.LoadScene(Constants.SELECT_LEVEL_SCENE);
		}
	}

	protected override void NewLevel() {

		base.DestroyCurrentLevel();
		boardHolder.localScale = new Vector3(1, 1, 1);

		int levelNumber = GameManager.instance.currentLevelFromPackage;
		Pack package = GameManager.instance.currentPackage;

		// First levels are static to show the tutorial
		if( levelNumber <= 15 ) {
			currentLevelGrid = LevelJsonGenerator.CreateLevel(package, levelNumber);
		
			// Next levels are automatically generated
		} else {
			System.Random newRandom = new System.Random(levelNumber);

			LevelGenerator levelGenerator = new LevelGenerator(newRandom);
			int rows = newRandom.Next(3,6 +1);
			int cols = newRandom.Next(3,6 +1);

			if(rows > 5 || cols> 5) {
				boardHolder.localScale = new Vector3(0.82f, 0.82f, 0.82f);
			}

			Debug.Log("level:" + levelNumber + " " + rows + " " + cols );
			currentLevelGrid = levelGenerator.CreateLevel(rows, cols, levelNumber);

		}

		GameObject.Find("Messages").GetComponent<TextMesh>().text = currentLevelGrid.message;

		base.NewLevel();

	}

	public override void PauseGame() {
		base.PauseGame();

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "Pause", "", 0);
	}
		
	public override void ClosePausePopup() {
		base.ClosePausePopup();
		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "UnPause", "", 0);
	}

}