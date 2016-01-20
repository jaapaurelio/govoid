using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GridHouseUI : MonoBehaviour {

	public Sprite[] possibleBackgrounds;

	private GridPosition houseGridPosition;

	public GridPosition HouseGridPosition {
		get{
			return houseGridPosition;
		}
		set {
			houseGridPosition = value;
		}
	}

	void Start() {
		transform.Find("SquareBackground").GetComponent<SpriteRenderer>().sprite = 
			possibleBackgrounds[0];
	}

	public void SetNumber(int newNumber) {
		transform.Find("Number").GetComponent<Number>().SetNumber(newNumber);
	}

	public void SetState(int newState ) {
		
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		GameObject selectecBackground = transform.Find("SelectedBackground").gameObject;

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.color = new Color32(255, 255, 255, 255);
			selectecBackground.SetActive(true);
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.color = new Color32(255, 255, 255, 90);
			selectecBackground.SetActive(false);
			break;

		case Constants.HOUSE_STATE_POSSIBLE:
			background.color = new Color32(255, 255, 255, 255);
			selectecBackground.SetActive(false);
			break;
		}

	}
		
}
