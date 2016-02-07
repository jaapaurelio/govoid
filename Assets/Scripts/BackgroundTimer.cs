using UnityEngine;
using System.Collections;

public class BackgroundTimer : MonoBehaviour {

	float lastWorldScreenHeight = 0;

	void Start() {
		ResizeSpriteToScreen();
	}

	void Update() {

		// If a resize happen, we need to calculate the background size again
		if(lastWorldScreenHeight != GetWorldScreenHeight()) {
			ResizeSpriteToScreen();
		}
	}

	void ResizeSpriteToScreen() {

		transform.localScale = new Vector3(1,1,1);

		float worldScreenHeight = GetWorldScreenHeight();

		transform.localScale =  new Vector3( 24f , worldScreenHeight, 1);
		lastWorldScreenHeight = worldScreenHeight;
	}

	public void UpdateTime(float newTime) {
		float worldScreenHeight = GetWorldScreenHeight();

		if(newTime > Constants.TIME_ATTACK_TIME) {
			transform.position = new Vector3(0, worldScreenHeight, 0);
		} else {
			transform.position = new Vector3(0, newTime * (worldScreenHeight / Constants.TIME_ATTACK_TIME), 0);
		}

	}

	private float GetWorldScreenHeight() {
		// orthographicSize is half the screen height, so we multiply by 2
		return Camera.main.orthographicSize * 2.0f;
	}

}
