using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	int number = 0;

	void Start(){
		GameObject numberObj = transform.Find("Number").gameObject;
		numberObj.GetComponent<MeshRenderer>().sortingLayerName = "Board";
		numberObj.GetComponent<MeshRenderer>().sortingOrder = 1;
	}

	public void SetNumber(int newNumber) {
		number = newNumber;

		GameObject numberObj = transform.Find("Number").gameObject;
		numberObj.GetComponent<TextMesh>().text = number.ToString();
	}
}
