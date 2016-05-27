using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void Start() {
		GameManager.instance.googleAnalytics.LogScreen("MainMenu");
	}
}
