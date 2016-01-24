using UnityEngine;
using System.Collections;

public class NewGameBtn : MonoBehaviour {

	void OnTouch_TM() {
		Debug.Log("Clicked in new game Button");
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().NewGame();
	}
}
