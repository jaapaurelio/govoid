using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {

	public Sprite[] numberSprites;

	public void SetNumber(int newNumber){

		// TODO Temporary fix since I dont have more than 6 sprites.
		if(newNumber > 6 ){
			newNumber = 6;
		}

		GetComponent<SpriteRenderer>().sprite = numberSprites[newNumber];

	}

}
