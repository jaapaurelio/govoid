using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayInfinity : MonoBehaviour {
	
	public void OnTouch_TM() {
		SceneManager.LoadScene("InfinityScene");
	}
}
