using UnityEngine;
using System.Collections;

public class RestartBtnInfinity : MonoBehaviour {

	void OnTouch_TM() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().RestartGame();
	}
}
