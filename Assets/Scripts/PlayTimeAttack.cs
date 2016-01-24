using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayTimeAttack : MonoBehaviour {

	public void OnTouch_TM() {
		SceneManager.LoadScene("TimeAttackScene");
	}
}
