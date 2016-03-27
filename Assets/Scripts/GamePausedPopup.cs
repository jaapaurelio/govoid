using UnityEngine;
using System.Collections;

public class GamePausedPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, -2);
	}
		
	public void Show() {
		transform.position = new Vector3(0,0,-2);
	}

	public void Hide(){
		transform.position = new Vector3(90,0,-2);
	}

}
