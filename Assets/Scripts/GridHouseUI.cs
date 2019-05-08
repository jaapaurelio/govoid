using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouseUI : MonoBehaviour {

    private int numCircles = 9;
	private GameObject[] circles;
	public Sprite backgroundNormal;
	public Animator anim;
    public GameObject background;
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

        Vector2[] positions = new Vector2[10];

        positions[0] = new Vector2(0f, 0f);
        positions[1] = new Vector2(-0.5f, 0.5f);
        positions[2] = new Vector2(0.5f, -0.5f);
        positions[3] = new Vector2(0f, 0.5f);
        positions[4] = new Vector2(0f, -0.5f);
        positions[5] = new Vector2(0.5f, 0.5f);
        positions[6] = new Vector2(-0.5f, -0.5f);
        positions[7] = new Vector2(-0.5f, 0f);
        positions[8] = new Vector2(0.5f, 0f);

        circles = new GameObject[numCircles];

        for (int i = 0; i < numCircles; i++)
        {
            GameObject b = Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
            b.transform.parent = gameObject.transform;
            b.transform.localPosition = new Vector3(positions[i].x, positions[i].y, 0f);

            b.GetComponent<SpriteRenderer>().sortingLayerName = "Board";
            b.SetActive(false); // false to hide, true to show

            circles[i] = b;
        }
    }

	void Start() {
    }

    public void SetNumber(int newNumber) {
        float scale = 1.0f;

        if(newNumber > 0) {
            scale = 1.0f / newNumber;
        }

        for (int i = newNumber; i < numCircles; i++) {
            circles[i].SetActive(false);
        }

        for (int i = 0; i < newNumber; i++)
        {
            SpriteRenderer background = circles[i].GetComponent<SpriteRenderer>();
            circles[i].SetActive(true);
            float scaleP = 0.2f;
            circles[i].transform.localScale = new Vector3(scaleP, scaleP, scaleP);
        }

        if (newNumber==0) {
            SpriteRenderer background = circles[0].GetComponent<SpriteRenderer>();
            background.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
	}

	public void ResetHouse() {

        for (int i = 0; i < numCircles; i++)
        {
            SpriteRenderer background2 = circles[i].GetComponent<SpriteRenderer>();
            background2.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            background2.color = new Color(
    Random.Range(colorPossibleR - colorRange, colorPossibleR + colorRange),
    Random.Range(colorPossibleG - colorRange, colorPossibleG + colorRange),
    Random.Range(colorPossibleB - colorRange, colorPossibleB + colorRange));

        }
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

            backgroundSquare.color = new Color(
                Random.Range(colorR - colorRange, colorR + colorRange),
                Random.Range(colorG - colorRange, colorG + colorRange),
                Random.Range(colorB - colorRange, colorB + colorRange),
                colorA);
        }

    }
		
}
