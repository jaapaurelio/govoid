using System;
using UnityEngine;

public class GridHouseUI : MonoBehaviour {

    private const int numCircles = 9;
	private GameObject[] circles;
	public Sprite backgroundNormal;
	public Animator anim;
    public GameObject HouseDot;
    public GameObject Teleport;
    private float colorPossibleR = 0.75f;
    private float colorPossibleG = 0.17f;
    private float colorPossibleB = 0.6f;
    public GridHouse model;

    public GridPosition HouseGridPosition { get; set; }

    void Awake(){

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

    private Vector2 pToPosition(int x, int y) {
        return new Vector2(x * 0.60f, y * 0.60f);
    }

	public void ResetHouse() {
        SetState(Constants.HOUSE_STATE_POSSIBLE);
	}


	public void SetState(int newState ) {

        float colorR = 1f;
        float colorG = 1f;
        float colorB = 1f;
        float colorA = 1f;
        bool possible = false;

        switch (newState) {
    		case Constants.HOUSE_STATE_ACTIVE:
            case Constants.HOUSE_STATE_PREVIOUS:
                colorR = 1f;
                colorG = 1f;
                colorB = 1f;
                colorA = 0.3f;

                break;
    		case Constants.HOUSE_STATE_NORMAL:
                colorR = 1f;
                colorG = 1f;
                colorB = 1f;
                colorA = 0.3f;

                break;
    		case Constants.HOUSE_STATE_POSSIBLE:
                colorR = colorPossibleR;
                colorG = colorPossibleG;
                colorB = colorPossibleB;

                possible = true;

                break;
    		case Constants.HOUSE_STATE_MISSING:
                colorR = 1f;
                colorG = 1f;
                colorB = 1f;
                colorA = 0.3f;

                break;
		}

        if(!model.isTeleport) {
            for (int i = 0; i < numCircles; i++)
            {
                SpriteRenderer circleBackground = circles[i].GetComponent<SpriteRenderer>();

                circleBackground.color = new Color(colorR, colorG, colorB, colorA);

                circles[i].GetComponent<HouseDot>().SetActiveDot(possible);
            }
        }

        if(model.isTeleport) {
            Teleport.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB, colorA);
        }
    }

    public void SetNumber(int newNumber)
    {

        if(model.isTeleport) {
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
