using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayTimeAttack : ButtonText {
	
	public GoogleAnalyticsV3 googleAnalytics;

	void Start() {
		googleAnalytics.LogScreen("MainMenu");
	}


	override public void OnTouch() {
		SceneManager.LoadScene("TimeAttackScene");
	}

}
