using UnityEngine;
using System.Collections;

public class NewGameBtn : MonoBehaviour {

	void OnMouseDown() {
		GameObject.Find("GameManager").GetComponent<BoardManager>().NewGame();
	}
}
