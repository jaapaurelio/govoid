using UnityEngine;
using System.Collections;

public class GamePausedPopupInfinity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, -2);
		Debug.Log("Start game pause popup");
	}

	public void Show() {
		gameObject.SetActive(true);
	}

	public void Hide(){
		gameObject.SetActive(false);
	}

	void OnDestroy() {
		Debug.Log("Destoy do pause");
	}
}
