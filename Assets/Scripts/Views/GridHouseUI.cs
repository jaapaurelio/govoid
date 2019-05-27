using System;
using UnityEngine;

public class GridHouseUI : MonoBehaviour
{

    private const int numCircles = 9;
    private GameObject[] circles;
    public GameObject backgroundGameObject;
    public GameObject borderGameObject;
    public Animator anim;
    public GameObject HouseDot;
    public GameObject Teleport;
    private Color32 activeColor = new Color32(0, 255, 184, 255);
    public GridHouse model;

    public GridPosition HouseGridPosition { get; set; }

    void Awake()
    {

        circles = new GameObject[numCircles];

        for (int i = 0; i < numCircles; i++)
        {
            GameObject b = Instantiate(HouseDot, new Vector3(0, 0, 0), Quaternion.identity);
            b.transform.parent = gameObject.transform;

            b.GetComponent<SpriteRenderer>().sortingLayerName = "Board";
            b.SetActive(false); // false to hide, true to show

            circles[i] = b;
        }
    }

    private Vector2 pToPosition(int x, int y)
    {
        return new Vector2(x * 0.60f, y * 0.60f);
    }

    public void ResetHouse()
    {
        SetState(Constants.HOUSE_STATE_POSSIBLE);
    }


    public void SetState(int newState)
    {
        Color32 dotColor = new Color32(178, 178, 178, 255);

        bool possible = false;

        borderGameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        backgroundGameObject.GetComponent<SpriteRenderer>().color = new Color(0.085f, 0.085f, 0.085f, 1);

        switch (newState)
        {
            case Constants.HOUSE_STATE_ACTIVE:
            case Constants.HOUSE_STATE_PREVIOUS:
                dotColor = new Color32(255, 255, 255, 255);

                backgroundGameObject.GetComponent<SpriteRenderer>().color = activeColor;
                borderGameObject.GetComponent<SpriteRenderer>().color = activeColor;

                break;
            case Constants.HOUSE_STATE_NORMAL:

                borderGameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0);

                break;
            case Constants.HOUSE_STATE_POSSIBLE:
                dotColor = activeColor;

                borderGameObject.GetComponent<SpriteRenderer>().color = activeColor;

                possible = true;

                break;
            case Constants.HOUSE_STATE_MISSING:
                dotColor = new Color32(255, 255, 255, 255);

                borderGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0f, 1);

                break;
        }

        if (!model.isTeleport)
        {
            for (int i = 0; i < numCircles; i++)
            {
                SpriteRenderer circleBackground = circles[i].GetComponent<SpriteRenderer>();

                circleBackground.color = dotColor;

                circles[i].GetComponent<HouseDot>().SetActiveDot(possible);
            }
        }

        if (model.isTeleport)
        {
            Teleport.GetComponent<SpriteRenderer>().color = dotColor;
        }
    }

    public void SetNumber(int newNumber)
    {

        if (model.isTeleport)
        {
            return;
        }

        for (int i = newNumber; i < numCircles; i++)
        {
            circles[i].SetActive(false);
        }

        if (newNumber == 1)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(pToPosition(0, 0));
        }

        if (newNumber == 2)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.2f, 0.2f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.2f, -0.2f));
        }

        if (newNumber == 3)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.4f, 0.4f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.4f, -0.4f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 4)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.3f, 0.3f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.3f, -0.3f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.3f, 0.3f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.3f, -0.3f));
        }

        if (newNumber == 5)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.4f, 0.4f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.4f, -0.4f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.4f, 0.4f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.4f, -0.4f));

            circles[4].SetActive(true);
            circles[4].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 6)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, 0.3f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, -0.3f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, 0.3f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, -0.3f));

            circles[4].SetActive(true);
            circles[4].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, -0.3f));

            circles[5].SetActive(true);
            circles[5].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0.3f));
        }

        if (newNumber == 7)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 8)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5F, 0));

            circles[7].SetActive(true);
            circles[7].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5F, 0));
        }

        if (newNumber == 9)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<HouseDot>().SetPositionPP(new Vector2(-0.5F, 0));

            circles[7].SetActive(true);
            circles[7].GetComponent<HouseDot>().SetPositionPP(new Vector2(0.5F, 0));

            circles[8].SetActive(true);
            circles[8].GetComponent<HouseDot>().SetPositionPP(new Vector2(0, 0));
        }

    }

    internal void ShowTeleport()
    {
        Teleport.SetActive(true);
    }

    internal void DestroyHouse()
    {
        Destroy(gameObject);
    }
}
