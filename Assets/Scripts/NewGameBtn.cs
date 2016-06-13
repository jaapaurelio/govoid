using UnityEngine;
using System.Collections;

public class NewGameBtn : MonoBehaviour {
	public delegate void ClickAction();
	public static event ClickAction OnClicked;

	public void OnTouch_TM() {
		Debug.Log("asdasdasdasdasdasdasd");
		if(OnClicked != null){
			OnClicked();
		}
	}
}
