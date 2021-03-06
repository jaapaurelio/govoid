﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject houseToInstantiate;
    public GameObject title;

    private Transform boardHolder;

    private GameObject gridHouseObject1;
    private GameObject gridHouseObject2;
    private GameObject gridHouseObject3;

    public AudioSource houseClickedSound;
    public AudioSource levelPassedSound;

    // Use this for initialization
    void Start()
    {


        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;
        boardHolder.position = new Vector3(0f, 0f, 0f);

        title.SetActive(false);
        CreateHouses();

        DontDestroyOnLoad(levelPassedSound);

        StartCoroutine(DoAnimation());

    }

    void CreateHouses()
    {
        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        gridHouseObject1 =
            Instantiate(houseToInstantiate, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;

        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        gridHouseObject1.transform.SetParent(boardHolder);

        // TODO Find a way to set the sprite position given the grid position
        gridHouseObject1.transform.localPosition = new Vector3(-1 * 2.5f, 0 * 2.5f, 0f);

        gridHouseObject1.GetComponent<GridHouseUI>().model = new GridHouse(new GridPosition(1, 1), 1);
        gridHouseObject1.GetComponent<GridHouseUI>().SetNumber(1);
        gridHouseObject1.GetComponent<GridHouseUI>().HouseGridPosition = new GridPosition(1, 1);


        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        gridHouseObject2 =
            Instantiate(houseToInstantiate, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;

        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        gridHouseObject2.transform.SetParent(boardHolder);

        // TODO Find a way to set the sprite position given the grid position
        gridHouseObject2.transform.localPosition = new Vector3(0 * 2.5f, 0 * 2.5f, 0f);

        gridHouseObject2.GetComponent<GridHouseUI>().model = new GridHouse(new GridPosition(1, 1), 1);
        gridHouseObject2.GetComponent<GridHouseUI>().SetNumber(1);
        gridHouseObject2.GetComponent<GridHouseUI>().HouseGridPosition = new GridPosition(1, 1);


        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        gridHouseObject3 =
            Instantiate(houseToInstantiate, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;

        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        gridHouseObject3.transform.SetParent(boardHolder);

        // TODO Find a way to set the sprite position given the grid position
        gridHouseObject3.transform.localPosition = new Vector3(1 * 2.5f, 0 * 2.5f, 0f);

        gridHouseObject3.GetComponent<GridHouseUI>().model = new GridHouse(new GridPosition(1, 1), 1);
        gridHouseObject3.GetComponent<GridHouseUI>().SetNumber(1);
        gridHouseObject3.GetComponent<GridHouseUI>().HouseGridPosition = new GridPosition(1, 1);

    }

    protected IEnumerator DoAnimation()
    {

        gridHouseObject1.SetActive(false);
        gridHouseObject2.SetActive(false);
        gridHouseObject3.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        gridHouseObject1.SetActive(true);
        gridHouseObject2.SetActive(true);
        gridHouseObject3.SetActive(true);

        gridHouseObject1.GetComponent<GridHouseUI>().anim.Play("Entrance");
        gridHouseObject2.GetComponent<GridHouseUI>().anim.Play("Entrance");
        gridHouseObject3.GetComponent<GridHouseUI>().anim.Play("Entrance");

        yield return new WaitForSeconds(0.5f);

        houseClickedSound.Play();

        gridHouseObject1.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_ACTIVE);
        gridHouseObject1.GetComponent<GridHouseUI>().anim.Play("AnimateActive");
        gridHouseObject1.GetComponent<GridHouseUI>().SetNumber(0);

        gridHouseObject2.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_POSSIBLE);
        gridHouseObject2.GetComponent<GridHouseUI>().anim.Play("AnimatePossible");

        yield return new WaitForSeconds(0.5f);

        houseClickedSound.Play();

        gridHouseObject1.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_NORMAL);
        //gridHouseObject1.GetComponent<GridHouseUI>().anim.Play("Animate");

        gridHouseObject2.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_ACTIVE);
        gridHouseObject2.GetComponent<GridHouseUI>().anim.Play("AnimateActive");
        gridHouseObject2.GetComponent<GridHouseUI>().SetNumber(0);

        gridHouseObject3.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_POSSIBLE);
        gridHouseObject3.GetComponent<GridHouseUI>().anim.Play("AnimatePossible");


        yield return new WaitForSeconds(0.5f);

        houseClickedSound.Play();

        gridHouseObject1.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_NORMAL);

        gridHouseObject2.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_NORMAL);

        gridHouseObject3.GetComponent<GridHouseUI>().SetState(Constants.HOUSE_STATE_ACTIVE);
        gridHouseObject3.GetComponent<GridHouseUI>().anim.Play("AnimateActive");
        gridHouseObject3.GetComponent<GridHouseUI>().SetNumber(0);

        levelPassedSound.Play();

        yield return new WaitForSeconds(0.1f);

        boardHolder.gameObject.SetActive(false);
        title.SetActive(true);
        title.GetComponent<Animator>().Play("TitleAnimation");

        yield return new WaitForSeconds(0.5f);

        int haveSeenTutorial = PlayerPrefs.GetInt(Constants.PS_HAVE_SEEN_TUTORIAL);

        Debug.Log("haveSeenTutorial " + haveSeenTutorial);
        // Show tutorial if the user has never seen it.
        if (haveSeenTutorial == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("MainMenuScene");
        }

    }
}
