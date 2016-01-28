using UnityEngine;
using System.Collections;

public class NewGameBtnInfinity : MonoBehaviour {

	void OnTouch_TM() {
		Debug.Log("Clicked in new game Button");
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().NewGame();
	}
}
