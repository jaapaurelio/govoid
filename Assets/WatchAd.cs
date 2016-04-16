using UnityEngine;
using System.Collections;

public class WatchAd : ButtonText {

	public GameObject boostPopUp;

	override public void OnTouch() {
		boostPopUp.GetComponent<BoostPopup>().ShowAd();
	}
}
