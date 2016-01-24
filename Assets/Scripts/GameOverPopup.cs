using UnityEngine;
using System.Collections;

public class GameOverPopup : MonoBehaviour {

	void Start(){
		transform.position = new Vector3(0, 0, -2);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void Show(int score, bool isHighscore) {

		transform.Find("GameOverScore").GetComponent<TextMesh>().text = score + " levels done.";

		if(isHighscore) {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(true);
		} else {
			transform.Find("GameOverNewHighscore").gameObject.SetActive(false);
		} 

		gameObject.SetActive(true);
	}
}
