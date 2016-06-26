using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenTutorial  : ButtonText {

	override public void OnTouch() {
		SceneManager.LoadScene("TutorialScene");
	}

}
