using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	public void Update() {
		if(Input.touchCount > 0){
			TriggerEvent(true);
		}else if(Input.GetMouseButtonDown(0)){
			TriggerEvent(false);
		}
	}

	private static void TriggerEvent(bool mouseevent){
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

		if (mouseevent) {
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				HitCollider(hit);
			}
		} else {
			HitCollider(hit);
		}
	}

	private static void HitCollider(RaycastHit2D hit){
		if (hit.collider) {
			hit.collider.gameObject.SendMessage("OnTouch_TM", SendMessageOptions.DontRequireReceiver);
		}
	}
}
