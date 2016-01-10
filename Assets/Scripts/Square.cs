using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	int number = 0;


	public void SetNumber(int newNumber) {
		number = newNumber;

		transform.Find("Number").GetComponent<Number>().SetNumber(number);

	}
}
