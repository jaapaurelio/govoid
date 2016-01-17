using UnityEngine;
using System.Collections;

public class GridHouseUI : MonoBehaviour {

	private GridPosition houseGridPosition;

	public GridPosition HouseGridPosition {
		get{
			return houseGridPosition;
		}
		set {
			houseGridPosition = value;
		}
	}

	public void SetNumber(int newNumber) {
		transform.Find("Number").GetComponent<Number>().SetNumber(newNumber);
	}

	public void SetState(int newState ) {
		
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.color = new Color(0f, 255.0f, 255.0f);
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.color = new Color(255.0f, 0.0f, 0f);
			break;

		case Constants.HOUSE_STATE_POSSIBLE:
			background.color = new Color(3.0f, 169.0f, 0f);
			break;
		}

	}
		
}
