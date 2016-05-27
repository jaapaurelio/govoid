using UnityEngine;
using System.Collections;

public class NextLevelBtn : MonoBehaviour {
	public GameObject boardManagerInfinity;

	public void OnTouch_TM() {
		boardManagerInfinity.GetComponent<BoardManagerInfinity>().GoNextLevel();
	}
}
