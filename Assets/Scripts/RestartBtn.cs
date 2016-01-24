using UnityEngine;
using System.Collections;

public class RestartBtn : MonoBehaviour {

	void OnTouch_TM() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().RestartGame();
	}
}
