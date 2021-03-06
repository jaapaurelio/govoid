﻿using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using System.Collections;
using UnityEngine.SceneManagement;

public class BoardManagerInfinity : BoardManager
{
    public GameObject endLevelPopup;

    public override void Start()
    {

        base.Start();

        GameManager.instance.googleAnalytics.LogScreen("InfinityMode");

        NewGame();
    }

    public void Update()
    {
        if (playing)
        {
            base.BoardInteraction();
        }
    }

    public override void RestartGame()
    {
        base.RestartGame();

        if (!playing)
        {
            return;
        }

        GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "RestartGame", "level" + GameManager.instance.currentLevelFromPackage, 0);

    }

    public override void NewGame()
    {
        base.NewGame();

        GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "StartNewGame", "", 0);

        SetLevelNumber();

        NewLevel();
    }

    private void SetLevelNumber()
    {
        TextMesh levelNumberTextObject = GameObject.Find("CurrentLevel").GetComponent<TextMesh>();

        levelNumberTextObject.text = GameManager.instance.currentLevelFromPackage.ToString();

        List<int> levelsDone = GameManager.instance.playerStatistics.GetLevelsDoneFromPackage(GameManager.instance.currentPackageNum);

        if (levelsDone.Contains(GameManager.instance.currentLevelFromPackage))
        {
            levelNumberTextObject.color = new Color32(0, 255, 184, 255);
        }
        else
        {
            levelNumberTextObject.color = new Color32(255, 255, 255, 255);
        }

    }

    protected override void LostLevel()
    {
        base.LostLevel();

        GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "NoExitHouse", "level" + GameManager.instance.currentLevelFromPackage, 0);
    }


    protected override void WonLevel()
    {
        base.WonLevel();

        GameManager.instance.playerStatistics.SetLevelDone(GameManager.instance.currentPackageNum, GameManager.instance.currentLevelFromPackage);

        GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "WonLevel", "level" + GameManager.instance.currentLevelFromPackage, 0);

        Social.ReportScore(GameManager.instance.playerStatistics.GetNumberOfDoneLevelsFromPackage(1), "CgkI2ab42cEaEAIQBw", (bool success) =>
        {
            // handle success or failure
        });

    }

    protected override void AfterWonAnimation()
    {
        endLevelPopup.GetComponent<EndLevelPopup>().Show(GameManager.instance.currentLevelFromPackage);
    }


    // End level popup will call this
    public void GoNextLevel()
    {

        GameManager.instance.currentLevelFromPackage++;

        int availableLevels = PlayerPrefs.GetInt(Constants.PS_AVAIABLE_LEVELS);
        if (GameManager.instance.currentLevelFromPackage <= availableLevels)
        {//GameManager.instance.currentPackage.levels.Length){
            NewLevel();
        }
        else
        {
            Debug.Log("NO MORE LEVELS IN THS PACK");
            SceneManager.LoadScene(Constants.SELECT_LEVEL_SCENE);

            return;
        }

        endLevelPopup.GetComponent<EndLevelPopup>().Hide();

        SetLevelNumber();
    }

    public void GoPrevLevel()
    {
        GameManager.instance.currentLevelFromPackage--;

        if (GameManager.instance.currentLevelFromPackage > 0)
        {
            NewLevel();
        }
        else
        {
            Debug.Log("NO MORE LEVELS IN THS PACK");
            SceneManager.LoadScene(Constants.SELECT_LEVEL_SCENE);
        }

        SetLevelNumber();
    }

    protected override void NewLevel()
    {

        base.DestroyCurrentLevel();

        int levelNumber = GameManager.instance.currentLevelFromPackage;
        Pack package = GameManager.instance.currentPackage;
        Debug.Log("LEVELSSSSSS");
        Debug.Log(package.levels.Length);

        // First levels are static
        if (levelNumber <= package.levels.Length)
        {
            currentLevelGrid = LevelJsonGenerator.CreateLevel(package, levelNumber);
            // Next levels are automatically generated
        }
        else
        {
            System.Random newRandom = new System.Random(levelNumber);

            LevelGenerator levelGenerator = new LevelGenerator(newRandom);
            int rows = newRandom.Next(4, 6 + 1);
            int cols = newRandom.Next(4, 6 + 1);



            // Calculate the number of steps base on the Menten Kinetics formula.
            // https://en.wikipedia.org/wiki/Michaelis%E2%80%93Menten_kinetics
            //int maxNumberOfSteps = 120;
            //int middleOfDificulty = 100;

            int maxNumberOfSteps = 80;
            int middleOfDificulty = 80;

            // Create a harder level from time to time.
            if (levelNumber % 4 == 0)
            {
                maxNumberOfSteps = 90;
                middleOfDificulty = 80;
                Debug.Log("Hard Level");
            }

            // Create a easy leavel after a hard one so that user win confidence again
            if ((levelNumber - 1) % 4 == 0)
            {
                maxNumberOfSteps = 70;
                middleOfDificulty = 80;
                Debug.Log("Easy Level");
            }

            int numberOfSteps = (maxNumberOfSteps * levelNumber) / (middleOfDificulty + levelNumber);

            Debug.Log("level:" + levelNumber + " " + rows + " " + cols + " nsteps " + numberOfSteps);
            currentLevelGrid = levelGenerator.CreateLevel(rows, cols, numberOfSteps);

        }

        // No messages for now

        if (!string.IsNullOrEmpty(currentLevelGrid.message))
        {
            GameObject.Find("Messages").GetComponent<TextMesh>().text = currentLevelGrid.message;
        }
        else
        {
            GameObject.Find("Messages").GetComponent<TextMesh>().text = "";
        }

        base.NewLevel();

    }

}