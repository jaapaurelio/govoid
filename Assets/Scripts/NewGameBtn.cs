using UnityEngine;
using System.Collections;

public class NewGameBtn : ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().NewGame();
	}
}
