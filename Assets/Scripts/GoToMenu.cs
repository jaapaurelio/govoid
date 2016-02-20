using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMenu : ButtonText {

	override public void OnTouch() {
		SceneManager.LoadScene("MainMenuScene");
	}

}
