using UnityEngine;
using System.Collections;

public class PauseButtonInfinity : MonoBehaviour {

	public void OnTouch_TM() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().PauseGame();
	}

}
