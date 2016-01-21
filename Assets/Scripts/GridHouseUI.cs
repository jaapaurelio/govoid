using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;       //Allows us to use Lists.

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

	public void HidePossibleDirections(){
		transform.Find("HouseArrowTop").gameObject.SetActive(false);
		transform.Find("HouseArrowBottom").gameObject.SetActive(false);
		transform.Find("HouseArrowLeft").gameObject.SetActive(false);
		transform.Find("HouseArrowRight").gameObject.SetActive(false);
	}

	public void SetPossibleDirections(List<int> possibleDirections) {
		if(possibleDirections.Contains(Constants.TOP)) {
			transform.Find("HouseArrowTop").gameObject.SetActive(true);
		}

		if(possibleDirections.Contains(Constants.BOTTOM)) {
			transform.Find("HouseArrowBottom").gameObject.SetActive(true);
		}

		if(possibleDirections.Contains(Constants.LEFT)) {
			transform.Find("HouseArrowLeft").gameObject.SetActive(true);
		}

		if(possibleDirections.Contains(Constants.RIGHT)) {
			transform.Find("HouseArrowRight").gameObject.SetActive(true);
		}
	}
	public void SetState(int newState ) {
		
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		GameObject selectecBackground = transform.Find("SelectedBackground").gameObject;

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.color = new Color32(105, 192, 132, 255);
			selectecBackground.SetActive(true);
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.color = new Color32(154, 210, 171, 255);
			selectecBackground.SetActive(false);
			break;

		case Constants.HOUSE_STATE_POSSIBLE:
			background.color = new Color32(105, 192, 132, 255);
			selectecBackground.SetActive(false);
			break;
		}

	}
		
}
