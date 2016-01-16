using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

	private float targetAspect = 2.0f / 3.0f ;

	void Start () 
	{
		float windowAspect = (float)Screen.width / (float)Screen.height;
		float scaleHeight = windowAspect / targetAspect;
		Camera camera = GetComponent<Camera>();

		if (scaleHeight < 1.0f)
		{  
			camera.orthographicSize = camera.orthographicSize / scaleHeight;
		}
	}

}
