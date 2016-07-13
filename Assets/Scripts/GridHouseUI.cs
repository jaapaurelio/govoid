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

		if(newNumber==0){
			SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();
			background.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
		}
	}

	public void ResetHouse() {
		TextMesh numberText = transform.Find("Number").GetComponent<TextMesh>();
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		background.sprite = backgroundPossible;
		background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

		numberText.color = new Color32(38, 166, 154, 255);

	}


	public void SetState(int newState ) {
		TextMesh numberText = transform.Find("Number").GetComponent<TextMesh>();
		SpriteRenderer background = transform.Find("SquareBackground").GetComponent<SpriteRenderer>();

		switch(newState) {
		case Constants.HOUSE_STATE_ACTIVE:
			background.sprite = backgroundActive;
			//background.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

			numberText.color = new Color32(38, 166, 154, 255);
			break;
		case Constants.HOUSE_STATE_NORMAL:
			background.sprite = backgroundNormal;
			//background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			numberText.color = new Color32(127, 127, 127, 255);
			break;
		case Constants.HOUSE_STATE_POSSIBLE:
			background.sprite = backgroundPossible;
			//background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			numberText.color = new Color32(38, 166, 154, 255);


			break;
		case Constants.HOUSE_STATE_MISSING:
			background.sprite = backgroundMissing;
			//background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

			numberText.color = new Color32(240, 98, 146, 255);
			break;
		}

	}
		
}
