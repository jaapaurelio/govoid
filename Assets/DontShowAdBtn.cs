using UnityEngine;
using System.Collections;

public class DontShowAdBtn : ButtonText {

	public GameObject boostPopUp;

	override public void OnTouch() {
		boostPopUp.GetComponent<BoostPopup>().DontShowAd();
	}
}
