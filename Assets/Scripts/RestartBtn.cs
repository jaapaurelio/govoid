using UnityEngine;
using System.Collections;

public class RestartBtn : MonoBehaviour {
	public delegate void ClickAction();
	public static event ClickAction OnClicked;

	void OnTouch_TM() {
		if(OnClicked != null){
			OnClicked();
		}
	}
}
