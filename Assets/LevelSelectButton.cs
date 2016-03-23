using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {
	int level = 0;
		
	void Start() {
		gameObject.GetComponent<Button>().onClick.AddListener(delegate { BtnClicked(); });	
	}

	public void SetLevel(int num, string levelInfo) {
		level = num;
		gameObject.GetComponentInChildren<Text>().text = "level " + num + " " + levelInfo;
	}

	public void BtnClicked() {
		GameManager.instance.currentLevelFromPackage = level;
		SceneManager.LoadScene("InfinityScene");
	}
}
