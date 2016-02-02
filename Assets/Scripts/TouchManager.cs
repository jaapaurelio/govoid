using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	void Update() {
		
		if(Input.touchCount > 0 ){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

			var touch = Input.GetTouch(0);
			if( touch.phase == TouchPhase.Began) {
				if( hit.collider ) {
					hit.collider.gameObject.SendMessage("OnTouch_TM", SendMessageOptions.DontRequireReceiver);
				}
			}

		}
	}
}
