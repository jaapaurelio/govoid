using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayTimeAttack : MonoBehaviour {
	
	public GoogleAnalyticsV3 googleAnalytics;

	void Start() {
		googleAnalytics.LogScreen("MainMenu");
	}

	public void OnTouch_TM() {
		SceneManager.LoadScene("TimeAttackScene");
	}
}
