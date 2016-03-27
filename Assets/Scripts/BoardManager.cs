using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class BoardManager : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public GameObject tapToRestartGameObject;
	public GameObject arrowToInstanciate;
	public GameObject gameOverPopupObject;
	public GameObject gamePausedPopupObject;
	public GameObject houseToInstantiate;

	protected bool playing = false;

	protected GameObject arrowFrom;
	protected GameObject arrowToTop;
	protected GameObject arrowToBottom;
	protected GameObject arrowToLeft;
	protected GameObject arrowToRight;
	protected Transform boardHolder;
	protected bool canInteractWithBoard = true;
	protected bool hasRestarted = false;
	protected LevelGrid currentLevelGrid;
	protected bool canChooseNextHouse = true;

	public virtual void Start() {
		Debug.Log("Board manager Start");
		BoardSetup();

		tapToRestartGameObject.SetActive(false);

		arrowFrom = Instantiate (arrowToInstanciate, new Vector3 (-2, 0, 0f), Quaternion.identity) as GameObject;
		arrowFrom.transform.SetParent(boardHolder);
		arrowFrom.GetComponent<SpriteRenderer>().color = new Color32(76, 73, 88, 255);

		arrowToTop = Instantiate (arrowToInstanciate, new Vector3 (2, 0, 0f), new Quaternion(0, 0, 90, 90)) as GameObject;
		arrowToTop.transform.SetParent(boardHolder);

		arrowToBottom = Instantiate (arrowToInstanciate, new Vector3 (3, 0, 0f), new Quaternion(0f, 0f, -90, 90)) as GameObject;
		arrowToBottom.transform.SetParent(boardHolder);

		arrowToLeft = Instantiate (arrowToInstanciate, new Vector3 (4, 0, 0f), new Quaternion(0, 0, 180, 0)) as GameObject;
		arrowToLeft.transform.SetParent(boardHolder);

		arrowToRight = Instantiate (arrowToInstanciate, new Vector3 (5, 0, 0f), Quaternion.identity) as GameObject;
		arrowToRight.transform.SetParent(boardHolder);
	}

	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject("Board").transform;
		boardHolder.position = new Vector3(-5.0f, -8.0f, 0.0f);

	}


	public virtual void NewGame() {

		gameOverPopupObject.GetComponent<GameOverPopup>().Hide();
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Hide();
		tapToRestartGameObject.SetActive(false);
		boardHolder.gameObject.SetActive(true);

		canInteractWithBoard = false;
		this.playing = false;

		StartCoroutine(CanPlay());
		StartCoroutine(CanInteractWithBoardAgain());
	
	}

	public virtual void RestartGame() {

		if(!playing) {
			return;
		}

		hasRestarted = true;

		HideAllArrows();
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

	protected virtual void NextLevel() {

		canChooseNextHouse = false;
	}

	protected virtual void NewLevel() {

		hasRestarted = false;

		HideAllArrows();

		foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {

			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject gridHouseObject =
				Instantiate (houseToInstantiate, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

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

	public virtual void PauseGame() {
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Show();
		boardHolder.gameObject.SetActive(false);
		playing = false;
	}

	public virtual void ClosePausePopup() {
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Hide();
		boardHolder.gameObject.SetActive(true);
		StartCoroutine(CanPlay());
	}

	protected void SetAllHousesToState(List<GridHouse> gridHouses, int state) {
		foreach(GridHouse house in gridHouses) {
			house.SetState(state);
		}
	}

	private void AnimateEntrance(List<GridHouse> gridHouses) {
		foreach(GridHouse house in gridHouses) {
			house.gridHouseUIComponent.anim.Play("Entrance");
		}
	}

	protected void AnimateRestart() {
		List<GridHouse> gridHouses = currentLevelGrid.GetAllHouses();
		foreach(GridHouse house in gridHouses) {
			house.gridHouseUIComponent.anim.Play("Restart");
		}
	}

	// just move arrow outside screen
	// TODO find a better way without setActive
	protected void HideAllArrows() {
		arrowFrom.transform.localPosition = new Vector3(50,10,0);
		arrowToTop.transform.localPosition = new Vector3(50,10,0);
		arrowToBottom.transform.localPosition = new Vector3(50,10,0);
		arrowToRight.transform.localPosition = new Vector3(50,10,0);
		arrowToLeft.transform.localPosition = new Vector3(50,10,0);
	}


	protected IEnumerator CanPlay() {
		yield return new WaitForSeconds(0.1f);
		playing = true;
	}


	protected IEnumerator CanInteractWithBoardAgain() {
		yield return new WaitForSeconds(0.1f);
		canInteractWithBoard = true;
	}

	protected IEnumerator ShowTapToRestart() {

		yield return new WaitForSeconds(0.1f);
		tapToRestartGameObject.SetActive(true);

	}


	protected void ShowArrows(GridPosition fromPosition, int direction) {

		switch(direction) {
		case Constants.TOP:
			arrowToTop.transform.localPosition = new Vector3(fromPosition.column * 2.5f , fromPosition.row * 2.5f + 1.24f, 0);
			break;
		case Constants.BOTTOM:
			arrowToBottom.transform.localPosition = new Vector3(fromPosition.column * 2.5f , fromPosition.row * 2.5f - 1.24f, 0);
			break;
		case Constants.RIGHT:
			arrowToRight.transform.localPosition = new Vector3(fromPosition.column * 2.5f + 1.24f, fromPosition.row * 2.5f, 0);
			break;
		case Constants.LEFT:
			arrowToLeft.transform.localPosition = new Vector3(fromPosition.column * 2.5f - 1.24f, fromPosition.row * 2.5f, 0);
			break;
		}
	}

	protected void ShowFromArrow(GridHouse fromP, GridHouse toP ) {
		int direction = GetDirectionToSibling(fromP, toP);

		switch(direction) {
		case Constants.TOP:
			arrowFrom.transform.localPosition = new Vector3(fromP.position.column * 2.5f , fromP.position.row * 2.5f + 1.24f, 0);
			arrowFrom.transform.rotation = Quaternion.Euler(0,0,-90);
			break;
		case Constants.BOTTOM:
			arrowFrom.transform.localPosition = new Vector3(fromP.position.column * 2.5f , fromP.position.row * 2.5f - 1.24f, 0);
			arrowFrom.transform.rotation = Quaternion.Euler(0,0,90);
			break;
		case Constants.RIGHT:
			arrowFrom.transform.localPosition = new Vector3(fromP.position.column * 2.5f + 1.24f, fromP.position.row * 2.5f, 0);
			arrowFrom.transform.rotation = Quaternion.Euler(0,0,180);
			break;
		case Constants.LEFT:
			arrowFrom.transform.localPosition = new Vector3(fromP.position.column * 2.5f - 1.24f, fromP.position.row * 2.5f, 0);
			arrowFrom.transform.rotation = Quaternion.Euler(0,0,0);
			break;
		}
	}


	protected int GetDirectionToSibling(GridHouse fromHouse, GridHouse toHouse) {
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


	protected GridHouse GetActiveHouse(List<GridHouse> gridHouses) {
		foreach(GridHouse house in gridHouses) {
			if(house.State == Constants.HOUSE_STATE_ACTIVE) {
				return house;
			}
		}

		return null;
	}

	protected void DestroyCurrentLevel(){
		// Clear previous level
		if(currentLevelGrid != null ) {
			foreach(GridHouse house in currentLevelGrid.GetAllHouses() ) {
				house.Destroy();
			}
		}
	}

}
