using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainGame : MonoBehaviour {

	// Static instance of GameManager which allows it to be accessed by any other script.
	private static MainGame instance = null;

	// Awake is always called before any Start functions
	void Awake()
	{
		// Check if instance already exists
		if (instance == null)
		{
			// if not, set instance to this
			instance = this;
		}
		else if (instance != this)
		{ 
			Debug.Log("destoy main game");
			// If instance already exists and it's not this:
			// Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);   
		}

		// Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

	}

	// Use this for initialization
	void Start () {

		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
		});
	}

}
