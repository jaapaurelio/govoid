using UnityEngine;
using System.Collections;

public class TapToRestartInfinity : ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().RestartGame();
	}
}
