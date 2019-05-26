using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

	private float targetAspect = 2.0f / 3.0f ;
	private float originalOrthographicSize = 9.6f;
	private float windowAspect = 0;

	void Start () 
	{
		Resize();
	}
		
	void Update() {
		float currentWindowAspect = (float)Screen.width / (float)Screen.height;

		// check if we have a new resolution
		if(currentWindowAspect != windowAspect) {
			Resize();
		}
	}

	private void Resize() {
		windowAspect = (float)Screen.width / (float)Screen.height;
		float scaleHeight = windowAspect / targetAspect;
		Camera camera = GetComponent<Camera>();

		if (scaleHeight < 1.0f)
		{  
			camera.orthographicSize = originalOrthographicSize / scaleHeight;
		}
	}
}
