using UnityEngine;
using System.Collections;

public class NewGameBtn : ButtonText {
	public delegate void ClickAction();
	public static event ClickAction OnClicked;

	override public void OnTouch() {
		if(OnClicked != null){
			OnClicked();
		}
	}
}
