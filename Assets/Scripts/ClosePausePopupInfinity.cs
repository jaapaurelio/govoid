using UnityEngine;
using System.Collections;

public class ClosePausePopupInfinity: MonoBehaviour {

	public void OnTouch_TM() {
		Debug.Log("click close");
		GameObject.Find("BoardManagerInfinity").GetComponent<BoardManagerInfinity>().ClosePausePopup();
	}
}
