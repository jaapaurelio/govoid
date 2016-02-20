using UnityEngine;
using System.Collections;

public class NewGameBtnInfinity : ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().NewGame();
	}
}
