using UnityEngine;
using System.Collections;

public class GameOverPopup : MonoBehaviour {

	void Awake(){
		transform.position = new Vector3(0, 0, -2);
	}

	public void Hide() {
		transform.position = new Vector3(90, 0, -2);
	}

	public void Show(int score, bool isHighscore) {

		transform.Find("GameOverScore").GetComponent<TextMesh>().text = score + " levels done";

		if(isHighscore) {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(true);
		} else {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(false);
		} 

		transform.position = new Vector3(0, 0, -2);
	}
}
