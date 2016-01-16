using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class LevelGenerator {

	public LevelGrid CreateLevel(int _cols, int _rows, int _numberOfSteps) {

		LevelGrid levelGrid = new LevelGrid(_cols, _rows); 

		GridHouse currentHouse = levelGrid.ChooseRandomHouse();
		GridHouse previousHouse = currentHouse;
		List<GridHouse> siblings;

		while(_numberOfSteps > 0 ) {

			siblings = levelGrid.GetSiblings(currentHouse);
			siblings = RemovePreviousHouse(siblings, previousHouse);
			previousHouse = currentHouse;
			currentHouse = ChooseRandomHouse(siblings);
			currentHouse.IncreaseNumber();

			_numberOfSteps--;
		}

		return levelGrid;
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

	public GridHouse ChooseRandomHouse(List<GridHouse> houses){
		return houses[Random.Range (0, houses.Count)];
	}
}
