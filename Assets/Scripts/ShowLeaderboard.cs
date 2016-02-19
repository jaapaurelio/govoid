using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ShowLeaderboard : MonoBehaviour {

	public void OnTouch_TM() {
		if(!Social.localUser.authenticated){
			Social.localUser.Authenticate((bool success) => {
				if(success){
					Social.ShowLeaderboardUI();
				}
			});
		} else {
			Social.ShowLeaderboardUI();
		}
	}
}
