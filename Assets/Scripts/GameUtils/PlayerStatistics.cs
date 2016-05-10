using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class LevelStatistics {
	public int packageNum = 0;
	public int levelNum = 0;
	public bool done = false;
	public bool locked = false;
}


[System.Serializable]
public class PlayerStatistics {

	public int bestScoreInInfinityMode = 0;
	public List<LevelStatistics> levels;

	public void LoadData() {
		//PlayerPrefs.DeleteAll();
		string json = PlayerPrefs.GetString(Constants.PLAYER_SETTINGS_SAVE);

		Debug.Log("loadData" + json);

		if(json.Equals("")) {
			levels = new List<LevelStatistics>();

		} else {
			PlayerStatistics loadedData = JsonUtility.FromJson<PlayerStatistics>(json);

			// Load previous data
			bestScoreInInfinityMode = loadedData.bestScoreInInfinityMode;
			levels = loadedData.levels;
		}
	}

	public void SaveData() {
		string thisJson = JsonUtility.ToJson(this);
		Debug.Log("saveData" + thisJson);

	
		PlayerPrefs.SetString(Constants.PLAYER_SETTINGS_SAVE, thisJson);
		PlayerPrefs.Save();

	}


	public int GetNumberOfDoneLevelsFromPackage(int packNum) {
		int numOfDones = 0;

		foreach (LevelStatistics level in levels) {
			if( level.packageNum == packNum && level.done == true) {
				numOfDones++;
			}
		}

		return numOfDones;
	}

	public List<int> GetLevelsDoneFromPackage(int packNum) {
		List<int> doneLevels =  new List<int>();

		foreach (LevelStatistics level in levels) {
			if( level.packageNum == packNum && level.done == true) {
				doneLevels.Add(level.levelNum);
			}
		}

		return doneLevels;
	}

	public void SetLevelDone(int packNum, int levelNum) {
		
		LevelStatistics level = GetLevel(packNum, levelNum);

		if( level != null ){
			level.done = true;

		// Create a new level
		} else {
			LevelStatistics newLevel = new LevelStatistics();
			newLevel.done = true;
			newLevel.levelNum = levelNum;
			newLevel.packageNum = packNum;
			newLevel.locked = false;

			levels.Add(newLevel);
		}
	}

	private LevelStatistics GetLevel(int packNum, int levelNum) {
		
		foreach (LevelStatistics level in levels) {
			if( level.packageNum == packNum && level.levelNum == levelNum) {
				return level;
			}
		}

		return null;
	}

}
