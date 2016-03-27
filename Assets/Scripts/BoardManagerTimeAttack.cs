using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManagerTimeAttack : BoardManager
{

	public GameObject backgroundTimerGameObject;

	private int levelsCompleted = 0;
	private float currentTime = 0.0f;

	public override void Start(){
		base.Start();

		googleAnalytics.LogScreen("TimeAttackMode");

		int bestScoreInTimeAttack = PlayerPrefs.GetInt("BestScoreInTimeAttack");

		Debug.Log("Best score: " + bestScoreInTimeAttack);

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

							clickedHouse.gridHouseUIComponent.anim.Play("AnimateActive");

							List<GridHouse> clickedHouseSiblings = currentLevelGrid.GetSiblings(clickedHouse);

							// Set all houses temporarily to normal state.
							// TODO This can cause problems later with animations.
							SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_NORMAL);

							List<int> possibleDirections = new List<int>();

							HideAllArrows();

							// All siblings from the clicked house are now possible houses to click
							foreach(GridHouse sibling in clickedHouseSiblings) {

								// Except the current active house, player can not go back
								if( !sibling.Equals(activeHouse)) {
									if(sibling.Number > 0) {

										sibling.SetState(Constants.HOUSE_STATE_POSSIBLE);
										possibleDirections.Add(GetDirectionToSibling(clickedHouse, sibling));

										sibling.gridHouseUIComponent.anim.Play("AnimatePossible");
										ShowArrows(clickedHouse.position, GetDirectionToSibling(clickedHouse, sibling));
									}
								}
							}
								
							// The clicked house is now the active house
							clickedHouse.SetActiveHouse();

							if( activeHouse != null ) {
								ShowFromArrow(clickedHouse, activeHouse);

								activeHouse.UnsetActive();
							}

							// No more places to go
							if(possibleDirections.Count == 0) {
								bool won = true;
								// check if we are some missing houses to pass
								foreach (GridHouse house in currentLevelGrid.GetAllHouses()) {
									if(house.Number > 0) {
										won = false;
										house.SetHouseMissing();
										house.gridHouseUIComponent.anim.Play("AnimateMissing");
									}
								}

								if(won) {
									NextLevel();

								// Lost
								} else {
									canInteractWithBoard = false;
									StartCoroutine(ShowTapToRestart());
									googleAnalytics.LogEvent("TimeAttackMode", "NoExitHouse", "", 0);
								}

							}

						// no possible house. player must release is finger
						} else if(clickedHouse.State == Constants.HOUSE_STATE_NORMAL){
							canChooseNextHouse = false;
						}
					}
				}
			}
		}
	}



	public override void RestartGame() {
		base.RestartGame();

		if(!playing) {
			return;
		}
			
		googleAnalytics.LogEvent("TimeAttackMode", "RestartGame", "", 0);

	}

	public override void NewGame() {
		
		base.NewGame();

		levelsCompleted = 0;

		googleAnalytics.LogEvent("TimeAttackMode", "StartNewGame", "", 0);

		GameObject.Find("BestScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BestScoreInTimeAttack").ToString();
		GameObject.Find("CurrentScore").GetComponent<TextMesh>().text = levelsCompleted.ToString();

		currentTime = Constants.TIME_ATTACK_TIME;

		NewLevel();
	}

	protected override void NextLevel() {

		base.NextLevel();

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

		return levelGenerator.CreateLevel(columns, rows, numberOfSteps);;

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