using UnityEngine;
using System.Collections;

public class RestartBtn : MonoBehaviour {

	void OnMouseDown() {
		GameObject.Find("GameManager").GetComponent<BoardManager>().RestartGame();
	}
}
