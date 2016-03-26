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
	public Sprite backgroundTwoStepsBack;
	public Animator anim;

	public GridPosition HouseGridPosition {
		get{
			return houseGridPosition;
		}
		set {
			houseGridPosition = value;
		}
	}

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Start() {
		transform.Find("SquareBackground").GetComponent<SpriteRenderer>().sortingLayerName = "Board";

	}

	public void SetNumber(int newNumber) {
		transform.Find("Number").GetComponent<Number>().SetNumber(newNumber);
	}

	public void SetState(int newState ) {
		TextMesh numberText = transform.Find("Number").GetComponent<TextMesh>();
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();
		numberText.color = new Color32(172, 172, 172, 255);

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.sprite = backgroundActive;
			numberText.color = new Color32(38, 166, 154, 255);
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.sprite = backgroundNormal;
			break;
		case Constants.HOUSE_STATE_POSSIBLE:
			numberText.color = new Color32(38, 166, 154, 255);
			background.sprite = backgroundPossible;
			break;
		case Constants.HOUSE_STATE_MISSING:
			numberText.color = new Color32(240, 98, 146, 255);
			background.sprite = backgroundMissing;
			break;
		break;
		}

	}
		
}
