using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManagerTimeAttack : BoardManager
{

	public GameObject backgroundTimerGameObject;
	public GameObject gameOverPopupObject;
	public GameObject boostPopupObject;

	private int levelsCompleted = 0;
	private float currentTime = 0.0f;

	private int timeWon;
	private int timeWonRestart;
	private int currentLevelDifilculty;
	public override void Start(){
		base.Start();

		googleAnalytics.LogScreen("TimeAttackMode");

		int bestScoreInTimeAttack = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = bestScoreInTimeAttack.ToString();

		NewGame();
	}

	void Update() {

		if (playing) {

			currentTime -= Time.deltaTime;

			GameObject.Find("Timer").GetComponent<TextMesh>().text = Mathf.Round(currentTime).ToString();

			backgroundTimerGameObject.GetComponent<BackgroundTimer>().UpdateTime(currentTime);

			if(currentTime < 0) {
				GameOver();
				return;
			}

			base.BoardInteraction();

		}
	}

	protected override void LostLevel() {
		base.LostLevel();
		googleAnalytics.LogEvent("TimeAttackMode", "NoExitHouse", "", 0);
	}

	public override void RestartGame() {
		base.RestartGame();

		if(!playing) {
			return;
		}
			
		googleAnalytics.LogEvent("TimeAttackMode", "RestartGame", "", 0);

	}

	public override void NewGame() {

		base.ResetForNewGame();

		boostPopupObject.GetComponent<BoostPopup>().ShowPopup();

		gameOverPopupObject.SendMessage("Hide");

		levelsCompleted = 0;
		timeWon = 3;
		timeWonRestart = 1;
		currentLevelDifilculty = 1;

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInTimeAttack").ToString();
		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		currentTime = Constants.TIME_ATTACK_TIME;

	}

	public void StartNewGame() {
		base.NewGame();

		boostPopupObject.GetComponent<BoostPopup>().HidePopup();

		googleAnalytics.LogEvent("TimeAttackMode", "StartNewGame", "", 0);


		NewLevel();
	}

	protected override void WonLevel() {

		base.WonLevel();

		levelsCompleted++;

		// From 10 to 10 levels player win more time betweent levels
		if(levelsCompleted >= 10 && levelsCompleted % 10 == 0) {
			timeWon = timeWon + 2;
			timeWonRestart = timeWonRestart + 2;

			Debug.Log("timeWon "+ timeWon);
			Debug.Log("timeWonRestart "+ timeWonRestart);
		}

		if(hasRestarted) {
			googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "HasRestarted", 0);
			currentTime = currentTime + timeWonRestart;
			StartCoroutine(ShowBonusTime(timeWonRestart));
		} else {
			googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "NoRestarted", 0);
			currentTime = currentTime + timeWon;
			StartCoroutine(ShowBonusTime(timeWon));
		}

		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		NewLevel();

	}

	public void ExtraTime(int time) {
		currentTime += time;
	}

	private IEnumerator ShowBonusEasyLevel() {

		GameObject.Find("BonusMessages").GetComponent<TextMesh>().text = "Power\none easy level";

		yield return new WaitForSeconds(1);

		GameObject.Find("BonusMessages").GetComponent<TextMesh>().text = "";

		NewLevel();

	}

	private IEnumerator ShowBonusTime(int time) {

		GameObject.Find("BonusTime").GetComponent<TextMesh>().text = "+" + time.ToString();

		yield return new WaitForSeconds(3);

		GameObject.Find("BonusTime").GetComponent<TextMesh>().text = "";

	}

	private void GameOver() {

		playing = false;
		tapToRestartGameObject.SetActive(false);


		int bestScoreIn60s = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		googleAnalytics.LogEvent("TimeAttackMode", "GameOver", "Score", levelsCompleted);

		Social.ReportScore(levelsCompleted, "CgkI2ab42cEaEAIQAQ", (bool success) => {
			// handle success or failure
		});

		// new best score
		if( levelsCompleted > bestScoreIn60s) {
			PlayerPrefs.SetInt("BestScoreInTimeAttack", levelsCompleted);
			googleAnalytics.LogEvent("TimeAttackMode", "GameOver", "NewHighScore", levelsCompleted);

			gameOverPopupObject.GetComponent<GameOverPopup>().Show(levelsCompleted, true);
		} else {
			gameOverPopupObject.GetComponent<GameOverPopup>().Show(levelsCompleted, false);
		}

	}

	protected override void NewLevel() {

		base.DestroyCurrentLevel();
			
		currentLevelGrid = GenerateNewLevelGrid();

		base.NewLevel();
	}


	private LevelGrid GenerateNewLevelGrid() {
		int columns;                                         //Number of columns in our game board.
		int rows;                                            //Number of rows in our game board.
		int numberOfSteps;

		LevelGenerator levelGenerator =  new LevelGenerator();

		// Easy in the beginner, and it gets harder.

		if(levelsCompleted <= 9 ) {
			columns = 4;
			rows = 4;
			numberOfSteps = levelsCompleted + 2;
			currentLevelDifilculty = 5;

		} else {
			
			columns = Random.Range (3, 5 +1);
			rows = Random.Range (3, 5 +1);

			if ( levelsCompleted % 5 == 0 ) {
				currentLevelDifilculty += 5;
			}

			numberOfSteps = Random.Range (currentLevelDifilculty -2, currentLevelDifilculty +1);

			Debug.Log("levelsCompleted " +levelsCompleted );
			Debug.Log("currentLevelDifilculty "+ currentLevelDifilculty);
			Debug.Log("numberOfSteps " + numberOfSteps);
		}

		Debug.Log("logs: Level:" + levelsCompleted+ " \n cols: " + columns + "; rows: " + rows + "; stepts: " +  numberOfSteps);

		return levelGenerator.CreateLevel(columns, rows, numberOfSteps);

	}


	public override void PauseGame() {
		base.PauseGame();

		googleAnalytics.LogEvent("TimeAttackMode", "Pause", "", 0);
	}
		
	public override void ClosePausePopup() {
		base.ClosePausePopup();
		googleAnalytics.LogEvent("TimeAttackMode", "UnPause", "", 0);
	}

}