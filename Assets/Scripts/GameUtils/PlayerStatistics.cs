using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class LevelStatistics
{
    public int packageNum = 0;
    public int levelNum = 0;
    public bool done = false;
}


[System.Serializable]
public class PlayerStatistics
{

    public int bestScoreInInfinityMode = 0;
    public List<LevelStatistics> levels;

    private bool loaded = false;

    public void LoadData()
    {
        //PlayerPrefs.DeleteAll();
        string json = PlayerPrefs.GetString(Constants.PLAYER_SETTINGS_SAVE);

        if (json.Equals(""))
        {
            levels = new List<LevelStatistics>();

        }
        else
        {
            PlayerStatistics loadedData = JsonUtility.FromJson<PlayerStatistics>(json);

            // Load previous data
            bestScoreInInfinityMode = loadedData.bestScoreInInfinityMode;
            levels = loadedData.levels;
        }

        loaded = true;
    }

    public void SaveData()
    {

        if (!loaded)
        {
            Debug.Log("Prevent save data without loading it first.");
            return;
        }

        string thisJson = JsonUtility.ToJson(this);

        PlayerPrefs.SetString(Constants.PLAYER_SETTINGS_SAVE, thisJson);
        PlayerPrefs.Save();

    }


    public int GetNumberOfDoneLevelsFromPackage(int packNum)
    {
        int numOfDones = 0;

        foreach (LevelStatistics level in levels)
        {
            if (level.packageNum == packNum && level.done == true)
            {
                numOfDones++;
            }
        }

        return numOfDones;
    }

    public List<int> GetLevelsDoneFromPackage(int packNum)
    {
        List<int> doneLevels = new List<int>();

        foreach (LevelStatistics level in levels)
        {
            if (level.packageNum == packNum && level.done == true)
            {
                doneLevels.Add(level.levelNum);
            }
        }

        return doneLevels;
    }

    public void SetLevelDone(int packNum, int levelNum)
    {

        LevelStatistics level = GetLevel(packNum, levelNum);

        if (level != null)
        {
            level.done = true;

            // Create a new level
        }
        else
        {
            LevelStatistics newLevel = new LevelStatistics();
            newLevel.done = true;
            newLevel.levelNum = levelNum;
            newLevel.packageNum = packNum;

            levels.Add(newLevel);
        }
    }

    private LevelStatistics GetLevel(int packNum, int levelNum)
    {

        foreach (LevelStatistics level in levels)
        {
            if (level.packageNum == packNum && level.levelNum == levelNum)
            {
                return level;
            }
        }

        return null;
    }

}
