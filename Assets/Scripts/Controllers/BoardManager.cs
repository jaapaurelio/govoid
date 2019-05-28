using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.SceneManagement;
using System;

public class BoardManager : MonoBehaviour
{

    public GameObject houseToInstantiate;

    // Sounds
    public AudioSource houseClickedSound;
    public AudioSource levelPassedSound;
    public AudioSource noExitSound;
    public AudioSource noPossibleClick;

    protected bool playing = false;

    protected Transform boardHolder;
    protected bool canInteractWithBoard = true;
    protected bool hasRestarted = false;
    protected LevelGrid currentLevelGrid;
    protected bool canChooseNextHouse = true;

    private List<int> possibleDirections;
    private List<GridHouse> possibleHouses;

    private List<AudioSource> houseClickedSounds;

    public virtual void Awake()
    {
        // To be possible open game in any scene
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene("IntroScene");
            return;
        }
    }

    public virtual void Start()
    {
        // Prevent sleep 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        BoardSetup();

        NewGameBtn.OnClicked += NewGame;
        RestartBtn.OnClicked += RestartGame;
        TapToRestart.OnClicked += RestartGame;

        houseClickedSounds = new List<AudioSource>();
        for (int i = 0; i < 10; i++)
        {
            houseClickedSounds.Add(Instantiate(houseClickedSound, new Vector3(3, 0, 0f), new Quaternion(0f, 0f, -90, 90)) as AudioSource);
        }
    }

    public virtual void OnDisable()
    {
        NewGameBtn.OnClicked -= NewGame;
        RestartBtn.OnClicked -= RestartGame;
        TapToRestart.OnClicked -= RestartGame;
    }

    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;
        boardHolder.position = new Vector3(-5.0f, -8.0f, 0.0f);

    }

    public virtual void NewGame()
    {

        ResetForNewGame();

        StartCoroutine(CanPlay());
        StartCoroutine(CanInteractWithBoardAgain());

    }

    protected virtual void ResetForNewGame()
    {

        boardHolder.gameObject.SetActive(true);

        canInteractWithBoard = false;
        this.playing = false;
    }

    public virtual void RestartGame()
    {

        hasRestarted = true;

        StartCoroutine(CanInteractWithBoardAgain());

        foreach (var house in currentLevelGrid.GetAllHouses())
        {
            house.number = house.originalNumber;
            house.ui.SetNumber(house.number);

            if (house.number > 0)
            {
                house.state = Constants.HOUSE_STATE_POSSIBLE;
                house.ui.SetState(Constants.HOUSE_STATE_POSSIBLE);
            }
        }

        AnimateRestart();

    }

    protected virtual void NewLevel()
    {

        hasRestarted = false;

        float houseSizeX = 4.8f / currentLevelGrid.columns;
        float houseSizeY = 5.3f / currentLevelGrid.rows;

        float houseSize = houseSizeX < houseSizeY ? houseSizeX : houseSizeY;

        if (houseSize > 1.0f)
        {
            houseSize = 1.0f;
        }

        boardHolder.localScale = new Vector3(houseSize, houseSize, houseSize);

        if (currentLevelGrid.columns < 5)
        {
            boardHolder.localPosition = new Vector3((currentLevelGrid.columns * -5f) / 5f, -8, 0);
        }
        else
        {
            boardHolder.localPosition = new Vector3(-5f, -8, 0);
        }


        foreach (GridHouse house in currentLevelGrid.GetAllHouses())
        {

            //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
            GameObject gridHouseObject =
                Instantiate(houseToInstantiate, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;

            house.ui = gridHouseObject.GetComponent<GridHouseUI>();
            house.ui.model = house;

            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            gridHouseObject.transform.SetParent(boardHolder);

            // TODO Find a way to set the sprite position given the grid position
            gridHouseObject.transform.localPosition = new Vector3(house.position.column * 2.5f, house.position.row * 2.5f, 0f);

            if (house.actions != null)
            {

                if (Array.IndexOf(house.actions, "TELEPORT_1") != -1)
                {
                    house.isTeleport = true;
                    house.ui.ShowTeleport();

                }

                if (Array.IndexOf(house.actions, "END") != -1)
                {
                    house.isEndpoint = true;
                    house.ui.ShowEndPoint();
                }
            }


            gridHouseObject.GetComponent<GridHouseUI>().SetNumber(house.number);
            gridHouseObject.GetComponent<GridHouseUI>().HouseGridPosition = house.position;

            // Do not display houses that start as zero
            if (house.number == 0)
            {
                gridHouseObject.SetActive(false);
            }
        }

        AnimateEntrance(currentLevelGrid.GetAllHouses());

        // At the begginig any house can be selected
        SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_POSSIBLE);

    }

    protected void BoardInteraction()
    {


        if (BoardInputHelper.GetTouches().Count > 0)
        {

            var touch = BoardInputHelper.GetTouches()[0];
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                canChooseNextHouse = true;
            }

            if (canInteractWithBoard && canChooseNextHouse && (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider && hit.collider.tag == "GridHouse")
                {

                    GridHouse clickedHouse = hit.collider.gameObject.GetComponent<GridHouseUI>().model;

                    // User can click in this house
                    if (clickedHouse.state == Constants.HOUSE_STATE_POSSIBLE)
                    {

                        PossibleHouseClicked(clickedHouse);

                        PlayHouseClickSound();

                        GridHouse activeHouse = GetActiveHouse(currentLevelGrid.GetAllHouses());

                        if (clickedHouse.isTeleport)
                        {
                            clickedHouse = FindHouseToTeleport(clickedHouse);
                        }

                        //clickedHouse.gridHouseUIComponent.anim.Play("AnimateActive");

                        List<GridHouse> clickedHouseSiblings = currentLevelGrid.GetPossibleSiblings(clickedHouse);

                        // Set all houses temporarily to normal state.
                        // TODO This can cause problems later with animations.
                        SetAllHousesToState(currentLevelGrid.GetAllHouses(), Constants.HOUSE_STATE_NORMAL);

                        possibleDirections = new List<int>();
                        possibleHouses = new List<GridHouse>();

                        // All siblings from the clicked house are now possible houses to click
                        foreach (GridHouse sibling in clickedHouseSiblings)
                        {

                            // Except the current active house, player can not go back
                            if (!sibling.Equals(activeHouse))
                            {
                                if (sibling.number > 0 || sibling.isTeleport)
                                {
                                    possibleDirections.Add(GetDirectionToSibling(clickedHouse, sibling));
                                    possibleHouses.Add(sibling);
                                }
                            }
                        }

                        // The clicked house is now the active house
                        clickedHouse.state = Constants.HOUSE_STATE_ACTIVE;
                        clickedHouse.ui.SetState(Constants.HOUSE_STATE_ACTIVE);

                        if (!clickedHouse.isTeleport)
                        {
                            clickedHouse.number--;
                            clickedHouse.ui.SetNumber(clickedHouse.number);
                        }

                        if (activeHouse != null)
                        {
                            activeHouse.state = Constants.HOUSE_STATE_PREVIOUS;
                            clickedHouse.ui.SetState(Constants.HOUSE_STATE_PREVIOUS);
                        }

                        List<GridHouse> missingHouses = new List<GridHouse>();

                        // check if we are some missing houses to pass
                        foreach (GridHouse house in currentLevelGrid.GetAllHouses())
                        {
                            if (house.number > 0 && !house.isTeleport)
                            {
                                missingHouses.Add(house);
                            }
                        }

                        if (missingHouses.Count == 0)
                        {
                            clickedHouse.state = Constants.HOUSE_STATE_NORMAL;
                            clickedHouse.ui.SetState(Constants.HOUSE_STATE_NORMAL);

                            // check for end point house
                            GridHouse endPointHouse = null;
                            foreach (GridHouse house in currentLevelGrid.GetAllHouses())
                            {
                                if (house.isEndpoint)
                                {
                                    endPointHouse = house;
                                }
                            }

                            if (endPointHouse == null)
                            {
                                WonLevel();
                                return;
                            }

                            if (endPointHouse == clickedHouse)
                            {
                                WonLevel();
                                return;

                            }


                            endPointHouse.state = Constants.HOUSE_STATE_MISSING;
                            endPointHouse.ui.SetState(Constants.HOUSE_STATE_MISSING);

                            LostLevel();

                            return;
                        }

                        // No more places to go
                        if (possibleHouses.Count == 0)
                        {
                            clickedHouse.state = Constants.HOUSE_STATE_NORMAL;
                            clickedHouse.ui.SetState(Constants.HOUSE_STATE_NORMAL);

                            foreach (GridHouse house in missingHouses)
                            {
                                house.state = Constants.HOUSE_STATE_MISSING;
                                house.ui.SetState(Constants.HOUSE_STATE_MISSING);
                                //house.gridHouseUIComponent.anim.Play("AnimateMissing");
                            }

                            LostLevel();
                        }

                        foreach (GridHouse house in possibleHouses)
                        {
                            house.state = Constants.HOUSE_STATE_POSSIBLE;
                            house.ui.SetState(Constants.HOUSE_STATE_POSSIBLE);
                            house.ui.anim.Play("AnimatePossible");
                        }

                        // no possible house. player must release is finger
                    }
                    else if (clickedHouse.state == Constants.HOUSE_STATE_NORMAL)
                    {
                        canChooseNextHouse = false;

                        if (!clickedHouse.isTeleport)
                        {
                            noPossibleClick.Play();
                        }

                        AnimatePossibleHouses();

                    }
                    else if (clickedHouse.state == Constants.HOUSE_STATE_ACTIVE)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            AnimatePossibleHouses();
                        }
                    }
                }
            }
        }
    }

    private GridHouse FindHouseToTeleport(GridHouse house)
    {

        foreach (GridHouse possibleHouse in currentLevelGrid.GetAllHouses())
        {
            if (possibleHouse.isTeleport && !possibleHouse.Equals(house))
            {
                return possibleHouse;
            }
        }

        return null;
    }

    protected virtual void PossibleHouseClicked(GridHouse house)
    {
        // overrided by game board
    }

    private void PlayHouseClickSound()
    {

        // Multiple sources to be possible play multiple sounds at the same time 
        for (int i = 0; i < houseClickedSounds.Count; i++)
        {
            if (!houseClickedSounds[i].isPlaying)
            {
                houseClickedSounds[i].Play();
                return;
            }
        }
    }

    private void AnimatePossibleHouses()
    {
        foreach (GridHouse house in possibleHouses)
        {
            house.ui.anim.Play("AnimatePossible");
        }
    }

    protected virtual void LostLevel()
    {

        noExitSound.Play();
        canInteractWithBoard = false;

    }

    protected virtual void WonLevel()
    {

        levelPassedSound.Play();
        canChooseNextHouse = false;

        StartCoroutine(AnimateWon());

    }

    protected IEnumerator AnimateWon()
    {

        foreach (GridHouse house in currentLevelGrid.GetAllHouses())
        {
            house.ui.anim.Play("WonLevel");
        }

        yield return new WaitForSeconds(0.6f);
        AfterWonAnimation();
    }


    protected virtual void AfterWonAnimation()
    {
        // This function will be defined in the game modes.
    }

    protected void SetAllHousesToState(List<GridHouse> gridHouses, int state)
    {
        foreach (GridHouse house in gridHouses)
        {
            house.state = state;
            house.ui.SetState(state);
        }
    }

    private void AnimateEntrance(List<GridHouse> gridHouses)
    {
        foreach (GridHouse house in gridHouses)
        {
            house.ui.anim.Play("Entrance");
        }
    }

    protected void AnimateRestart()
    {
        List<GridHouse> gridHouses = currentLevelGrid.GetAllHouses();
        foreach (GridHouse house in gridHouses)
        {
            house.ui.anim.Play("Restart");
        }
    }


    protected IEnumerator CanPlay()
    {
        yield return new WaitForSeconds(0.1f);
        playing = true;
    }


    protected IEnumerator CanInteractWithBoardAgain()
    {
        yield return new WaitForSeconds(0.1f);
        canInteractWithBoard = true;
    }


    protected int GetDirectionToSibling(GridHouse fromHouse, GridHouse toHouse)
    {
        int possibleDirections = -1;

        if (fromHouse.position.column < toHouse.position.column)
        {
            possibleDirections = Constants.RIGHT;
        }

        if (fromHouse.position.column > toHouse.position.column)
        {
            possibleDirections = Constants.LEFT;
        }

        if (fromHouse.position.row < toHouse.position.row)
        {
            possibleDirections = Constants.TOP;
        }

        if (fromHouse.position.row > toHouse.position.row)
        {
            possibleDirections = Constants.BOTTOM;
        }

        return possibleDirections;

    }


    protected GridHouse GetActiveHouse(List<GridHouse> gridHouses)
    {
        foreach (GridHouse house in gridHouses)
        {
            if (house.state == Constants.HOUSE_STATE_ACTIVE)
            {
                return house;
            }
        }

        return null;
    }

    protected void DestroyCurrentLevel()
    {
        // Clear previous level
        if (currentLevelGrid != null)
        {
            foreach (GridHouse house in currentLevelGrid.GetAllHouses())
            {
                house.ui.DestroyHouse();
            }
        }

        currentLevelGrid = null;
    }

    void OnDestroy()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

}
