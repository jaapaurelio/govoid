using UnityEngine;
using System.Collections;

public class DontShowAdBtn : ButtonText {

	public GameObject boostPopUp;

	override public void OnTouch() {
		Debug.Log("ttt");
		boostPopUp.GetComponent<BoostPopup>().DontShowAd();
	}
}
