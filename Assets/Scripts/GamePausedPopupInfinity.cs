using UnityEngine;
using System.Collections;

public class GamePausedPopupInfinity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, -2);
	}

	public void Show() {
		gameObject.SetActive(true);
	}

	public void Hide(){
		gameObject.SetActive(false);
	}

}
