using UnityEngine;
using System.Collections;

public class TapToRestartInfinity : MonoBehaviour {

	public void OnTouch_TM() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().RestartGame();
	}
}
