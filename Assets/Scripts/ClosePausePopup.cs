using UnityEngine;
using System.Collections;

public class ClosePausePopup : ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().ClosePausePopup();
	}
}
