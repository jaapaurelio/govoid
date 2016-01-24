using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	public void OnTouch_TM() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().PauseGame();
	}

}
