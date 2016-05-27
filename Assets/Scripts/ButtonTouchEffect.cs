using UnityEngine;
using System.Collections;

public class ButtonTouchEffect : MonoBehaviour {
	float SCALE = 0.1f;

	public void OnTouch_TM() {
		transform.localScale -= new Vector3(SCALE, SCALE, 0);
	}

	public void OnTouchBegan_TM() {
		transform.localScale += new Vector3(SCALE, SCALE, 0);
	}

	public void OnTouchLeave_TM() {
		transform.localScale -= new Vector3(SCALE, SCALE, 0);
	}
}
