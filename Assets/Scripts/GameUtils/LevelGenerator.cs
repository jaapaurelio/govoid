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

		GridHouse currentHouse = levelGrid.ChooseRandomHouse();
		GridHouse previousHouse = currentHouse;
		List<GridHouse> siblings;

		while(_numberOfSteps > 0 ) {

			siblings = levelGrid.GetSiblings(currentHouse);
			siblings = RemovePreviousHouse(siblings, previousHouse);

			previousHouse = currentHouse;
			currentHouse = ChooseRandomHouse(siblings);
			currentHouse.SetFinalNumber(currentHouse.Number + 1);

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
		int h = rng.Next (0, houses.Count);
		return houses[h];
	}
}
