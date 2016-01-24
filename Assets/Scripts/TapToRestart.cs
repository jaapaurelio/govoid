using UnityEngine;
using System.Collections;

public class TapToRestart : MonoBehaviour {

	public void OnTouch_TM() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().RestartGame();
	}
}
