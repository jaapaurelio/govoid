using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

	public int columns;                                         //Number of columns in our game board.
	public int rows;                                            //Number of rows in our game board.
	public int numberOfSteps;
	public GameObject square;


	private LevelGrid currentLevelGrid;

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.

	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject("Board").transform;
		boardHolder.position =new Vector3(-5.0f, -8.0f, 0.0f);

	}

	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup();

		NewGame();

	}

	void Update() {
		
		if (Input.touchCount > 0 ) {
			var touch = Input.GetTouch(0);

			if( touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved ) {
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

						int numberOfPossibles = 0;

						// All siblings from the clicked house are now possible houses to click
						foreach(GridHouse sibling in clickedHouseSiblings) {

							// Except the current active house, player can not go back
							if( !sibling.Equals(activeHouse)) {
								if(sibling.Number > 0) {

									numberOfPossibles++;
									sibling.SetState(Constants.HOUSE_STATE_POSSIBLE);
								}
							}
						}
							
						// The clicked house is now the active house
						clickedHouse.SetActive();

						// The previous active house is now a normal house
						// At the beginning we dont have an active house
						if( activeHouse != null ) {
							activeHouse.UnsetActive();
						}


						// No more places to go
						if(numberOfPossibles == 0) {
							bool won = true;
							// check if we are some missing houses to pass
							foreach (GridHouse house in currentLevelGrid.GetAllHouses()) {
								if(house.Number > 0) {
									won = false;
								}
							}

							if(won) {
								GameObject.Find("ResultText").GetComponent<TextMesh>().text = "You Won";
							} else {
								GameObject.Find("ResultText").GetComponent<TextMesh>().text = "You Lost";
							}
						}

					}
				}
			}

		}
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
		foreach (var house in currentLevelGrid.GetAllHouses() ) {
			house.Restart();

			if(house.Number > 0) {
				house.SetState(Constants.HOUSE_STATE_POSSIBLE);
			}
		}

		GameObject.Find("ResultText").GetComponent<TextMesh>().text = "";
	}

	public void NewGame() {
		
		GameObject.Find("ResultText").GetComponent<TextMesh>().text = "";

		// Clear previous level
		if(currentLevelGrid != null ) {
			foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {
				house.Destroy();
			}
		}

		LevelGenerator levelGenerator =  new LevelGenerator();

		columns = Random.Range (4, 7);
		rows = Random.Range (4, 7);
		numberOfSteps = Random.Range (10, 25);

		GameObject.Find("LogsText").GetComponent<TextMesh>().text = "logs: \n cols: " + columns + "; rows: " + rows + "; stepts: " +  numberOfSteps;

		currentLevelGrid = levelGenerator.CreateLevel(columns, rows, numberOfSteps);

		foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {

			GameObject toInstantiate = square;

			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject gridHouseObject =
				Instantiate (toInstantiate, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			gridHouseObject.transform.SetParent(boardHolder);

			// TODO Find a way to set the sprite position given the grid position
			gridHouseObject.transform.localPosition = new Vector3 (house.position.column * 2, house.position.row * 2, 0f);

			gridHouseObject.GetComponent<GridHouseUI>().SetNumber(house.Number);
			gridHouseObject.GetComponent<GridHouseUI>().HouseGridPosition = house.position;

			house.SetGameObject(gridHouseObject);

			// Do not display houses that start as zero
			if(house.Number == 0) {
				gridHouseObject.SetActive(false);
			}
		}


		// At the begginig any house can be selected
		SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_POSSIBLE);

	}
}