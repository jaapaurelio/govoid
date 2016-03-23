using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManagerTimeAttack : MonoBehaviour
{

	public GoogleAnalyticsV3 googleAnalytics;
	public GameObject square;

	public GameObject gameOverPopupObject;
	public GameObject gamePausedPopupObject;
	public GameObject tapToRestartGameObject;
	public GameObject backgroundTimerGameObject;

	private int levelsCompleted = 0;
	private float currentTime = 0.0f;

	private bool playing = false;
	private bool canChooseNextHouse = true;
	private bool canInteractWithBoard = true;
	private bool hasRestarted = false;

	private GridHouse twoStepsBackHouse;

	private LevelGrid currentLevelGrid;

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.

	void Start() {

		googleAnalytics.LogScreen("TimeAttackMode");
		
		BoardSetup();

		int bestScoreInTimeAttack = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		Debug.Log("Best score: " + bestScoreInTimeAttack);

		tapToRestartGameObject.SetActive(false);

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = bestScoreInTimeAttack.ToString();

		NewGame();
	}

	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject("Board").transform;
		boardHolder.position = new Vector3(-5.0f, -8.0f, 0.0f);

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

			if(Input.touchCount > 0 ){
				var touch = Input.GetTouch(0);
				if( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled ) {
					canChooseNextHouse = true;
				}

				if( canInteractWithBoard && canChooseNextHouse && (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) ) {
					Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

					RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

					if (hit.collider && hit.collider.tag == "GridHouse" ) {

						GridHouseUI houseUI = hit.collider.gameObject.GetComponent<GridHouseUI>();

						GridHouse clickedHouse = currentLevelGrid.GetHouseInPosition(houseUI.HouseGridPosition);

						// User can click in this house
						if( clickedHouse.State == Constants.HOUSE_STATE_POSSIBLE ) {

							GridHouse activeHouse = GetActiveHouse(currentLevelGrid.GetAllHouses());

							List<GridHouse> clickedHouseSiblings = currentLevelGrid.GetSiblings(clickedHouse);

							// Set all houses temporarily to normal state.
							// This can cause problems later with animations.
							SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_NORMAL);

							List<int> possibleDirections = new List<int>();

							// All siblings from the clicked house are now possible houses to click
							foreach(GridHouse sibling in clickedHouseSiblings) {

								// Except the current active house, player can not go back
								if( !sibling.Equals(activeHouse)) {
									if(sibling.Number > 0) {

										sibling.SetState(Constants.HOUSE_STATE_POSSIBLE);
										possibleDirections.Add(GetDirectionToSibling(clickedHouse, sibling));
									}
								}
							}
								
							// The clicked house is now the active house
							clickedHouse.SetActiveHouse(possibleDirections);

							if( activeHouse != null ) {
								activeHouse.UnsetActive();
							}

							// The previous active house is now a normal house
							// At the beginning we dont have an active house
							/*
							if( twoStepsBackHouse != null ) {
								twoStepsBackHouse.UnsetActive();
							}

							if( activeHouse != null ) {
								activeHouse.SetState(Constants.HOUSE_STATE_TWO_STEPS_BACK);
								twoStepsBackHouse = activeHouse;
							}*/


							// No more places to go
							if(possibleDirections.Count == 0) {
								bool won = true;
								// check if we are some missing houses to pass
								foreach (GridHouse house in currentLevelGrid.GetAllHouses()) {
									if(house.Number > 0) {
										won = false;
										house.SetHouseMissing();
									}
								}

								if(won) {
									NextLevel();

								// Lost
								} else {
									tapToRestartGameObject.SetActive(true);
									canInteractWithBoard = false;
								}
							}

						// no possible house. player must release is finger
						} else if(clickedHouse.State == Constants.HOUSE_STATE_NORMAL){
							canChooseNextHouse = false;
							googleAnalytics.LogEvent("TimeAttackMode", "NoExitHouse", "", 0);
						}
					}
				}
			}
		}
	}

	private int GetDirectionToSibling(GridHouse fromHouse, GridHouse toHouse) {
		int possibleDirections = -1;

		if(fromHouse.position.column < toHouse.position.column) {
			possibleDirections = Constants.RIGHT;
		}

		if(fromHouse.position.column > toHouse.position.column) {
			possibleDirections = Constants.LEFT;
		}

		if(fromHouse.position.row < toHouse.position.row) {
			possibleDirections = Constants.TOP;
		}

		if(fromHouse.position.row > toHouse.position.row) {
			possibleDirections = Constants.BOTTOM;
		}

		return possibleDirections;

	}

	private void SetAllHousesToState(List<GridHouse> gridHouses, int state) {
		foreach(GridHouse house in gridHouses) {
			house.SetState(state);
		}
	}

	private GridHouse GetActiveHouse(List<GridHouse> gridHouses) {
		foreach(GridHouse house in gridHouses) {
			if(house.State == Constants.HOUSE_STATE_ACTIVE) {
				return house;
			}
		}

		return null;
	}

	public void RestartGame() {

		twoStepsBackHouse = null;

		if(!playing) {
			return;
		}

		googleAnalytics.LogEvent("TimeAttackMode", "RestartGame", "", 0);

		hasRestarted = true;
			
		StartCoroutine(CanInteractWithBoardAgain());
		tapToRestartGameObject.SetActive(false);

		foreach (var house in currentLevelGrid.GetAllHouses() ) {
			house.Restart();

			if(house.Number > 0) {
				house.SetState(Constants.HOUSE_STATE_POSSIBLE);
			}
		}

		AnimateRestart();

	}

	private IEnumerator CanInteractWithBoardAgain() {

		yield return new WaitForSeconds(0.1f);
		canInteractWithBoard = true;

	}

	public void NewGame() {

		googleAnalytics.LogEvent("TimeAttackMode", "StartNewGame", "", 0);

		gameOverPopupObject.GetComponent<GameOverPopup>().Hide();
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Hide();
		tapToRestartGameObject.SetActive(false);

		boardHolder.gameObject.SetActive(true);

		canInteractWithBoard = false;
		playing = false;

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInTimeAttack").ToString();


		levelsCompleted = 0;
		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		currentTime = Constants.TIME_ATTACK_TIME;
		StartCoroutine(CanPlay());
		StartCoroutine(CanInteractWithBoardAgain());

		NewLevel();
	}

	private IEnumerator CanPlay() {
		yield return new WaitForSeconds(0.1f);
		playing = true;
	}

	private void NextLevel() {
		
		levelsCompleted++;

		if(hasRestarted) {
			googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "HasRestarted", 0);
			currentTime = currentTime + 3;
			StartCoroutine(ShowBonusTime(3));
		} else {
			googleAnalytics.LogEvent("TimeAttackMode", "NextLevel", "NoRestarted", 0);
			currentTime = currentTime + 5;
			StartCoroutine(ShowBonusTime(5));
		}

		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		canChooseNextHouse = false;

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

	private void NewLevel() {

		int columns;                                         //Number of columns in our game board.
		int rows;                                            //Number of rows in our game board.
		int numberOfSteps;

		hasRestarted = false;

		twoStepsBackHouse = null;

		// Clear previous level
		if(currentLevelGrid != null ) {
			foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {
				house.Destroy();
			}
		}

		LevelGenerator levelGenerator =  new LevelGenerator();

		// Easy in the beginner, and it get harder.

		if(levelsCompleted <= 1 ) {
			columns = 3;
			rows = 3;
			numberOfSteps = Random.Range (4, 7 +1);

		} else if(levelsCompleted <= 3 ){
			columns = 4;
			rows = 4;
			numberOfSteps = Random.Range (8, 15 +1);

		} else if(levelsCompleted <= 5 ){
			columns = 5;
			rows = 5;
			numberOfSteps = Random.Range (8, 15 +1);

		} else {
			columns = Random.Range (4, 5 +1);
			rows = Random.Range (4, 5 +1);
			numberOfSteps = Random.Range (10, 25 +1);
		}


		Debug.Log("logs: Level:" + levelsCompleted+ " \n cols: " + columns + "; rows: " + rows + "; stepts: " +  numberOfSteps);

		currentLevelGrid = levelGenerator.CreateLevel(columns, rows, numberOfSteps);

		foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {

			GameObject toInstantiate = square;

			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject gridHouseObject =
				Instantiate (toInstantiate, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			gridHouseObject.transform.SetParent(boardHolder);

			// TODO Find a way to set the sprite position given the grid position
			gridHouseObject.transform.localPosition = new Vector3 (house.position.column * 2.5f, house.position.row * 2.5f, 0f);

			gridHouseObject.GetComponent<GridHouseUI>().SetNumber(house.Number);
			gridHouseObject.GetComponent<GridHouseUI>().HouseGridPosition = house.position;

			house.SetGameObject(gridHouseObject);

			// Do not display houses that start as zero
			if(house.Number == 0) {
				gridHouseObject.SetActive(false);
			}
		}

		AnimateEntrance(currentLevelGrid.GetAllHouses());

		// At the begginig any house can be selected
		SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_POSSIBLE);

	}

	private void AnimateEntrance(List<GridHouse> gridHouses) {
		foreach(GridHouse house in gridHouses) {
			house.gridHouseUIComponent.anim.Play("Entrance");
		}
	}

	private void AnimateRestart() {
		List<GridHouse> gridHouses = currentLevelGrid.GetAllHouses();
		foreach(GridHouse house in gridHouses) {
			house.gridHouseUIComponent.anim.Play("Restart");
		}
	}

	public void PauseGame() {
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Show();

		boardHolder.gameObject.SetActive(false);
		playing = false;

		googleAnalytics.LogEvent("TimeAttackMode", "Pause", "", 0);
	}
		
	public void ClosePausePopup() {
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Hide();
		boardHolder.gameObject.SetActive(true);
		StartCoroutine(CanPlay());

		googleAnalytics.LogEvent("TimeAttackMode", "UnPause", "", 0);
	}

}