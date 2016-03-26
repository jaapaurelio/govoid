using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {

	public void SetNumber(int newNumber){

		GetComponent<TextMesh>().text = newNumber.ToString();

	}

}
