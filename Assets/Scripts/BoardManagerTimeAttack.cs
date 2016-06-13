using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManagerTimeAttack : BoardManager
{

	public GameObject backgroundTimerGameObject;
	public GameObject gameOverPopupObject;
	public GameObject tapToStartGameObject;
	public GameObject tapToRestartGameObject;
	public GameObject timeRed;
	public GameObject gamePausedPopupObject;

	private int levelsCompleted = 0;
	private float currentTime = 0.0f;
	private bool extraTime = false;
	private bool hasStarted = false;

	private int timeWon;
	private int timeWonRestart;
	private int currentLevelDifilculty;
	public override void Start(){
		base.Start();

		GameManager.instance.googleAnalytics.LogScreen("TimeAttackMode");
		tapToStartGameObject.SetActive(false);
		timeRed.SetActive(false);
		tapToRestartGameObject.SetActive(false);

		int bestScoreInTimeAttack = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = bestScoreInTimeAttack.ToString();

		PauseButton.OnClicked += PauseGame;
		ClosePausePopupButton.OnClicked += ClosePausePopup;

		NewGame();
	}

	public override void OnDisable(){
		base.OnDisable();
		PauseButton.OnClicked -= PauseGame;
		ClosePausePopupButton.OnClicked -= ClosePausePopup;
	}

	void Update() {
		
		GameObject.Find("Timer").GetComponent<TextMesh>().text = Mathf.Round(currentTime).ToString();

		backgroundTimerGameObject.GetComponent<BackgroundTimer>().UpdateTime(currentTime);

		if (playing) {

			currentTime -= Time.deltaTime;

			if( currentTime < Constants.TIME_TO_SHOW_RED) {
				timeRed.SetActive(true);
			} else {
				timeRed.SetActive(false);
			}

			if(currentTime < 0) {
				GameOver();
				return;
			}

			base.BoardInteraction();

		}
	}

	protected override void LostLevel() {
		base.LostLevel();
		StartCoroutine(ShowTapToRestart());
		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "NoExitHouse", "", 0);
	}

	public override void RestartGame() {
		base.RestartGame();

		tapToRestartGameObject.SetActive(false);
			
		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "RestartGame", "", 0);

	}

	protected IEnumerator ShowTapToRestart() {

		yield return new WaitForSeconds(0.1f);
		tapToRestartGameObject.SetActive(true);

	}

	protected override void ResetForNewGame(){
		base.ResetForNewGame();
		gamePausedPopupObject.SendMessage("Hide");
		tapToRestartGameObject.SetActive(false);

	}


	public override void NewGame() {

		ResetForNewGame();

		gameOverPopupObject.SendMessage("Hide");

		levelsCompleted = 0;
		timeWon = 3;
		timeWonRestart = 1;
		currentLevelDifilculty = 1;
		extraTime = false;
		hasStarted = false;

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInTimeAttack").ToString();
		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		currentTime = Constants.TIME_ATTACK_TIME;

		tapToStartGameObject.SetActive(true);

		boardHolder.gameObject.SetActive(false);

	}

	public void StartNewGame() {
		base.NewGame();

		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "StartNewGame", "", 0);

		boardHolder.gameObject.SetActive(true);
		hasStarted = true;
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
			GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "HasRestarted", 0);
			currentTime = currentTime + timeWonRestart;
			StartCoroutine(ShowBonusTime(timeWonRestart));
		} else {
			GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "NoRestarted", 0);
			currentTime = currentTime + timeWon;
			StartCoroutine(ShowBonusTime(timeWon));
		}

		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		NewLevel();

	}

	public void ExtraTime(int time) {
		currentTime += time;
	}

	public void KeepPlaying(){
		currentTime += Constants.TIME_ATTACK_TIME_EXTRA_TIME;
		gameOverPopupObject.GetComponent<GameOverPopup>().Hide();
		playing = false;
		extraTime = true;
		tapToStartGameObject.SetActive(true);
	}

	public void StartFromTap() {

		tapToStartGameObject.SetActive(false);

		if(extraTime) {
			StartCounting();
		} else {
			StartNewGame();
		}

	}

	private void StartCounting() {
		StartCoroutine(CanPlay());
		StartCoroutine(CanInteractWithBoardAgain());
		boardHolder.gameObject.SetActive(true);
		RestartGame();
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
		boardHolder.gameObject.SetActive(false);
		timeRed.SetActive(false);

		int bestScoreIn60s = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "GameOver", "Score", levelsCompleted);

		Social.ReportScore(levelsCompleted, "CgkI2ab42cEaEAIQAQ", (bool success) => {
			// handle success or failure
		});

		Debug.Log("extraTimeextraTime" + extraTime);

		// new best score
		if( levelsCompleted > bestScoreIn60s) {
			PlayerPrefs.SetInt("BestScoreInTimeAttack", levelsCompleted);
			GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "GameOver", "NewHighScore", levelsCompleted);

			gameOverPopupObject.GetComponent<GameOverPopup>().Show(levelsCompleted, true, !extraTime);
		} else {
			gameOverPopupObject.GetComponent<GameOverPopup>().Show(levelsCompleted, false, !extraTime);
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


	public virtual void PauseGame() {
		gamePausedPopupObject.SendMessage("Show", SendMessageOptions.RequireReceiver);
		playing = false;
		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "Pause", "", 0);
	}

	public virtual void ClosePausePopup() {
		gamePausedPopupObject.SendMessage("Hide");

		if(hasStarted) {
			StartCoroutine(CanPlay());
		}

		GameManager.instance.googleAnalytics.LogEvent("TimeAttackMode", "UnPause", "", 0);
	}


}