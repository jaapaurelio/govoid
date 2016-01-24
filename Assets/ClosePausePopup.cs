using UnityEngine;
using System.Collections;

public class ClosePausePopup : MonoBehaviour {

	public void OnTouch_TM() {
		Debug.Log("click close");
		GameObject.Find("BoardManagerTimeAttack").GetComponent<BoardManagerTimeAttack>().ClosePausePopup();
	}
}
