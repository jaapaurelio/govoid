using UnityEngine;
using System.Collections;

public class ClosePausePopupInfinity: ButtonText {

	override public void OnTouch() {
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().ClosePausePopup();
	}
}
