using UnityEngine;
using System.Collections;

public class BackgroundTimer : MonoBehaviour {

	public void UpdateTime(float newTime) {
		if(newTime > 20) {
			transform.position = new Vector3(0,0,0);
		} else {
			transform.position = new Vector3(0, newTime * -(20f/-19.7f), 0);
		}

	}

}
