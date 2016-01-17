using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class LevelGrid
{
	private int columns;           
	private int rows; 
	private List <GridHouse> gridHouses = new List <GridHouse> ();

	//Assignment constructor.
	public LevelGrid (int _cols, int _rows)
	{
		columns = _cols;
		rows = _rows;

		//Loop through x axis (columns).
		for(int x = 0; x < columns; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 0; y < rows; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridHouses.Add (new GridHouse(new GridPosition(x, y), 0));
			}
		}
	}

	public GridHouse ChooseRandomHouse(){
		return gridHouses[Random.Range (0, gridHouses.Count)];
	}

	public List<GridHouse> GetSiblings(GridHouse currentHouse) {
		
		List<GridHouse> siblings =  new List<GridHouse>();

		GridPosition currentPosition = currentHouse.position;

		GridPosition testPosition = new GridPosition(currentPosition.column + 1, currentPosition.row);
	
		if(IsValidPosition(testPosition)){
			siblings.Add(GetHouseInPosition(testPosition));
		}

		testPosition = new GridPosition(currentPosition.column - 1, currentPosition.row);

		if(IsValidPosition(testPosition)){
			siblings.Add(GetHouseInPosition(testPosition));
		}

		testPosition = new GridPosition(currentPosition.column, currentPosition.row + 1);

		if(IsValidPosition(testPosition)){
			siblings.Add(GetHouseInPosition(testPosition));
		}

		testPosition = new GridPosition(currentPosition.column, currentPosition.row - 1);

		if(IsValidPosition(testPosition)){
			siblings.Add(GetHouseInPosition(testPosition));
		}

		return siblings;
	}

	public GridHouse GetHouseInPosition(GridPosition position) {

		foreach( GridHouse house in gridHouses )
		{
			if( house.position.column == position.column && house.position.row == position.row) {
				return house;
			}
		}

		return null;
	}

	public bool IsValidPosition(GridPosition gridPosition) {
		if( gridPosition.column >= 0 &&
			gridPosition.column < columns - 1 &&
			gridPosition.row >= 0 &&
			gridPosition.row <= rows - 1 ) {

			return true;
		} else {
			return false;
		}
	}

	public List <GridHouse> GetAllHouses() {
		return gridHouses;
	}

}
