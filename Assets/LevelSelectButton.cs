using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {
	int level = 0;
		
	void Start() {
		gameObject.GetComponent<Button>().onClick.AddListener(delegate { BtnClicked(); });	
	}

	public void SetLevel(int num) {
		level = num;
		gameObject.GetComponentInChildren<Text>().text = "level " + num;
	}

	public void BtnClicked() {
		
		SceneManager.LoadScene("InfinityScene");
	}
}
