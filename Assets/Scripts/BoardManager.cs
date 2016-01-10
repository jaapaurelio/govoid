using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.


public class BoardManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum;             //Minimum value for our Count class.
		public int maximum;             //Maximum value for our Count class.


		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}


	public int columns;                                         //Number of columns in our game board.
	public int rows;                                            //Number of rows in our game board.
	public int numberOfSteps;
	public GameObject[] floorTiles;                                 //Array of floor prefabs.

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();

		//Loop through x axis (columns).
		for(int x = 1; x < columns-1; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 1; y < rows-1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}


	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
		boardHolder.position =new Vector3(-5.0f,-8.0f,0.0f);

		LevelGenerator levelGenerator =  new LevelGenerator();
		LevelGrid levelGrid = levelGenerator.CreateLevel(columns, rows, numberOfSteps);

		foreach(GridHouse house in levelGrid.GetAllHouses() ) {
			//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
			GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject instance =
				Instantiate (toInstantiate, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			instance.transform.SetParent (boardHolder);
			instance.transform.localPosition = new Vector3 (house.position.column * 2, house.position.row * 2, 0f);

			instance.GetComponent<Square>().SetNumber(house.number);
		}
	}

	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup ();

		//Reset our list of gridpositions.
		InitialiseList ();

	}
}