using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouseUI : MonoBehaviour {

	private GridPosition houseGridPosition;
	public Sprite backgroundPossible;
	public Sprite backgroundActive;
	public Sprite backgroundNormal;
	public Sprite backgroundMissing;

	public GridPosition HouseGridPosition {
		get{
			return houseGridPosition;
		}
		set {
			houseGridPosition = value;
		}
	}

	void Start() {
		transform.Find("SquareBackground").GetComponent<SpriteRenderer>().sortingLayerName = "Board";

	}

	public void SetNumber(int newNumber) {
		transform.Find("Number").GetComponent<Number>().SetNumber(newNumber);
	}

	public void SetState(int newState ) {
		
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.sprite = backgroundActive;
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.sprite = backgroundNormal;
			break;
		case Constants.HOUSE_STATE_POSSIBLE:
			background.sprite = backgroundPossible;
			break;
		case Constants.HOUSE_STATE_MISSING:
			background.sprite = backgroundMissing;
			break;
		}

	}
		
}
