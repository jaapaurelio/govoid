using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CloseTutorialBtn : MonoBehaviour {

	private void OnTouch_TM() {
		PlayerPrefs.SetInt(Constants.PS_HAVE_SEEN_TUTORIAL, 1);
		SceneManager.LoadScene("MainMenuScene");
	}
}
