using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {
	int level = 0;
		
	void Start() {
		gameObject.GetComponent<Button>().onClick.AddListener(delegate { BtnClicked(); });	
	}

	public void SetLevel(int num, bool levelDone) {
		level = num;
		gameObject.GetComponentInChildren<Text>().text = num.ToString();

		if(levelDone) {
			gameObject.GetComponentInChildren<Text>().color = new Color32(38, 166, 154, 255);
		}

	}

	public void BtnClicked() {
		GameManager.instance.currentLevelFromPackage = level;
		SceneManager.LoadScene("InfinityScene");
	}
}
