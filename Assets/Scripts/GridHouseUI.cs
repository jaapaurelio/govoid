using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouseUI : MonoBehaviour {

    private int numCircles = 9;
	private GameObject[] circles;
	public Sprite backgroundNormal;
	public Animator anim;
    public GameObject ParticleNumber;
    public GameObject SquareBackground;
    private GridPosition houseGridPosition;


    private float colorPossibleR = 0.75f;
    private float colorPossibleG = 0.17f;
    private float colorPossibleB = 0.6f;
    private float colorRange = 0.1f;

    public GridPosition HouseGridPosition {
		get{
			return houseGridPosition;
		}
		set {
			houseGridPosition = value;
		}
	}

	void Awake(){


        anim = GetComponent<Animator>();


        circles = new GameObject[numCircles];

        for (int i = 0; i < numCircles; i++)
        {
            GameObject b = Instantiate(ParticleNumber, new Vector3(0, 0, 0), Quaternion.identity);
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

        switch (newState) {
    		case Constants.HOUSE_STATE_ACTIVE:
                colorR = 1f;
                colorG = 1f; 
                colorB = 1f;
               
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

                break;
    		case Constants.HOUSE_STATE_MISSING:
                colorR = 0.8f;
                colorG = 0.2f;
                colorB = 0.1f;

    			break;
		}


        for (int i = 0; i < numCircles; i++)
        {
            SpriteRenderer backgroundSquare = circles[i].GetComponent<SpriteRenderer>();

            backgroundSquare.color = new Color(colorR, colorG, colorB, colorA);

        }

    }

    public void SetNumber(int newNumber)
    {
        float scale = 1.0f;

        if (newNumber > 0)
        {
            scale = 1.0f / newNumber;
        }

        for (int i = newNumber; i < numCircles; i++)
        {
            circles[i].SetActive(false);
        }


        if (newNumber == 1)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(pToPosition(0, 0));
        }

        if (newNumber == 2)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.2f, 0.2f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.2f, -0.2f));
        }

        if (newNumber == 3)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.4f, 0.4f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.4f, -0.4f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 4)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.3f, 0.3f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.3f, -0.3f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.3f, 0.3f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.3f, -0.3f));
        }

        if (newNumber == 5)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.4f, 0.4f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.4f, -0.4f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.4f, 0.4f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.4f, -0.4f));

            circles[4].SetActive(true);
            circles[4].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 6)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, 0.3f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, -0.3f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, 0.3f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, -0.3f));

            circles[4].SetActive(true);
            circles[4].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, -0.3f));

            circles[5].SetActive(true);
            circles[5].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0.3f));
        }

        if (newNumber == 7)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0));
        }

        if (newNumber == 8)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5F, 0));

            circles[7].SetActive(true);
            circles[7].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5F, 0));
        }

        if (newNumber == 9)
        {
            circles[0].SetActive(true);
            circles[0].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, 0.5f));

            circles[1].SetActive(true);
            circles[1].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, -0.5f));

            circles[2].SetActive(true);
            circles[2].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5f, 0.5f));

            circles[3].SetActive(true);
            circles[3].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5f, -0.5f));

            circles[4].SetActive(true);
            circles[4].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, -0.5f));

            circles[5].SetActive(true);
            circles[5].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0.5f));

            circles[6].SetActive(true);
            circles[6].GetComponent<SquareParticle>().SetPositionPP(new Vector2(-0.5F, 0));

            circles[7].SetActive(true);
            circles[7].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0.5F, 0));

            circles[8].SetActive(true);
            circles[8].GetComponent<SquareParticle>().SetPositionPP(new Vector2(0, 0));
        }

    }

}
