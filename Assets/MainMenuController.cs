using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	void Start() {
		googleAnalytics.LogScreen("MainMenu");
	}
}
