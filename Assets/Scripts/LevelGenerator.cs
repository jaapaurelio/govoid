using UnityEngine;
using System.Collections;

public class LevelGenerator {

	public LevelGrid CreateLevel(int _cols, int _rows, int _numberOfSteps) {
		LevelGrid levelGrid = new LevelGrid(_cols, _rows); 

		GridHouse currentHouse = levelGrid.ChooseRandomHouse();

		while(_numberOfSteps > 0 ){

			currentHouse.IncreaseNumber();

			currentHouse = levelGrid.ChooseRandomSibling(currentHouse);

			_numberOfSteps--;
		}

		return levelGrid;
	}

}
