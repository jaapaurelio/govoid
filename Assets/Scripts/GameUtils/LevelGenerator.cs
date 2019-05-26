using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = System.Random;

public class LevelGenerator {

	private Random rng;

	public LevelGenerator(Random _rng) {
		rng = _rng;
	}

	public LevelGenerator() {
		rng = new Random();
	}

	public LevelGrid CreateLevel(int _cols, int _rows, int _numberOfSteps) {

		LevelGrid levelGrid = new LevelGrid(_cols, _rows, rng); 

		GridHouse currentHouse = levelGrid.ChooseRandomPossibleHouse();
		GridHouse previousHouse = currentHouse;
		List<GridHouse> siblings;

		// Find number of holes
		int numberOfHoles = 0;
		Debug.Log("_cols " + _cols + " rows " + _rows);

		// 3 * 3 or 4 * 2
		if(_cols * _rows <= 9 ) {
			Debug.Log("if 1");
			numberOfHoles = 0;

		// 4 * 4
		} else if (_cols * _rows <= 16) {
			Debug.Log("if 2");
			numberOfHoles = 1;
		
		// 5 * 5
		} else if (_cols * _rows <= 24) {
			Debug.Log("if 3");
			numberOfHoles = rng.Next (1, 3);
		
		// bigger than 5 * 5 
		} else {
			Debug.Log("if 4");
			numberOfHoles = rng.Next (2, 5);
		}

		Debug.Log("=== numberOfHoles " + numberOfHoles);

		Debug.Log("Gera nivel =========");

		//Debug.Log(currentHouse.position.column + " + " + currentHouse.position.row);

		// Create holes
		while(numberOfHoles > 0 ) {

			do {
				currentHouse = levelGrid.ChooseRandomPossibleHouse();
				siblings = levelGrid.GetPossibleSiblings(currentHouse);

				currentHouse.isHole = true;

				// Not a valid hole, find a better house
				if(!AllHousesHaveWayOut(levelGrid, siblings)){
					currentHouse.isHole = false;
				}

			} while(!currentHouse.isHole);

			Debug.Log("House hole " + currentHouse.position.column + " " +  currentHouse.position.row);
			numberOfHoles--;
		}

		currentHouse = levelGrid.ChooseRandomPossibleHouse();

		while(_numberOfSteps > 0 ) {

			siblings = levelGrid.GetPossibleSiblings(currentHouse);
			siblings = RemovePreviousHouse(siblings, previousHouse);

			previousHouse = currentHouse;
			currentHouse = ChooseRandomHouse(siblings);
            currentHouse.number = currentHouse.number + 1;
            currentHouse.originalNumber = currentHouse.number + 1;

			_numberOfSteps--;
		}

		return levelGrid;
	}

	private bool AllHousesHaveWayOut(LevelGrid levelGrid, List<GridHouse> houses) {
		List<GridHouse> siblings;

		foreach(GridHouse house in houses) {
			siblings = levelGrid.GetPossibleSiblings(house);

			if (siblings.Count < 2) {
				return false;
			}
		}

		return true;
	}

	private List<GridHouse> RemovePreviousHouse(List<GridHouse> siblings, GridHouse previousHouse) {
		List<GridHouse> possibleSiblings = new List<GridHouse>();

		foreach(GridHouse house in siblings) {
			if( !house.Equals(previousHouse) ) {
				possibleSiblings.Add(house);
			}
		}

		return possibleSiblings;

	}

	public GridHouse ChooseRandomPossibleHouse(List<GridHouse> houses){
		int h = rng.Next (0, houses.Count);
		return houses[h];
	}

	public GridHouse ChooseRandomHouse(List<GridHouse> houses){
		int h = rng.Next (0, houses.Count);
		return houses[h];
	}
		
}
