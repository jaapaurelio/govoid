using UnityEngine;
using System.Collections;

public class EndLevelPopup : MonoBehaviour {

	public GameObject levelDoneNumber;

	// Use this for initialization
	void Awake () {
		transform.position = new Vector3(90, 90, -2);
	}

	public void Hide() {
		transform.position = new Vector3(90, 90, -2);
		gameObject.SetActive(false);
	}

	public void Show (int levelCompleted) {
		transform.position = new Vector3(0, 0, -2);
		gameObject.SetActive(true);

		levelDoneNumber.GetComponent<TextMesh>().text = levelCompleted.ToString();
	}
}
