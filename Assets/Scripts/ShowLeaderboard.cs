using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ShowLeaderboard : ButtonText {

	override public void OnTouch() {
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
