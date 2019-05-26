using UnityEngine;
using System.Collections;

public class SoundIcon : MonoBehaviour {

	public GameObject muteObject;
	public GameObject soundObject;


	// Use this for initialization
	void Start () {
	
		soundObject.SetActive(false);
		muteObject.SetActive(false);

		int soundState = PlayerPrefs.GetInt(Constants.PS_SOUND_STATE_KEY);

		if(soundState == 1 ) {
			soundObject.SetActive(true);

		} else {
			muteObject.SetActive(true);
		}

		AudioListener.volume = soundState;

	}

	void OnTouch_TM() {

		int soundState = PlayerPrefs.GetInt(Constants.PS_SOUND_STATE_KEY);

		if(soundState == 1 ) {
			soundObject.SetActive(false);
			muteObject.SetActive(true);
			AudioListener.volume = 0;
			PlayerPrefs.SetInt(Constants.PS_SOUND_STATE_KEY, 0);

		} else {
			soundObject.SetActive(true);
			muteObject.SetActive(false);
			AudioListener.volume = 1;
			PlayerPrefs.SetInt(Constants.PS_SOUND_STATE_KEY, 1);
		}
		
	}

}
