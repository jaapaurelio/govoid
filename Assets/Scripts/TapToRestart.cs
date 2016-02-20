using UnityEngine;
using System.Collections;

public class TapToRestart : ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().RestartGame();
	}
}
