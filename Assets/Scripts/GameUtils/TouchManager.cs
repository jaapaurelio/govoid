using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	private Collider2D colliderBegan;

	public void Update() {


        if (BoardInputHelper.GetTouches().Count > 0){
			var touch = BoardInputHelper.GetTouches()[0];

			if (touch.phase == TouchPhase.Began) {
				Collider2D hitCollider = GetHitCollider(Input.mousePosition);
				colliderBegan = hitCollider;
				SendMessageToHit(hitCollider, "OnTouchBegan_TM");

			} else if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {
				
				if(colliderBegan == null) {
					return;
				}

				Collider2D currentCullider = GetHitCollider(Input.mousePosition);

				if(currentCullider != null && currentCullider.name == colliderBegan.name) {
					SendMessageToHit(colliderBegan, "OnTouch_TM");
				} else {
					SendMessageToHit(colliderBegan, "OnTouchLeave_TM");
				}

				colliderBegan = null;

			}

		}else if(Input.GetMouseButtonDown(0)){

			//RaycastHit2D hit = getHit(Input.mousePosition);
		}
	}


	private static void SendMessageToHit(Collider2D currentCollider, string message){
		if (currentCollider) {
			currentCollider.gameObject.SendMessage(message, SendMessageOptions.DontRequireReceiver);
		}
	}

	private Collider2D GetHitCollider(Vector3 position){
		Vector3 pos = Camera.main.ScreenToWorldPoint (position);

		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

		return hit.collider;
	}
}
