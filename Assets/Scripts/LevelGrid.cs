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

	public GridHouse ChooseRandomSibling(GridHouse currentHouse) {

		List<GridPosition> possibleDirections =  new List<GridPosition>();
		GridPosition currentPosition = currentHouse.position;

		if(isValidPosition(currentPosition.column + 1, currentPosition.row)){
			possibleDirections.Add(new GridPosition(currentPosition.column + 1, currentPosition.row));
		}

		if(isValidPosition(currentPosition.column - 1, currentPosition.row)){
			possibleDirections.Add(new GridPosition(currentPosition.column - 1, currentPosition.row));
		}

		if(isValidPosition(currentPosition.column, currentPosition.row + 1)){
			possibleDirections.Add(new GridPosition(currentPosition.column , currentPosition.row + 1));
		}

		if(isValidPosition(currentPosition.column, currentPosition.row - 1)){
			possibleDirections.Add(new GridPosition(currentPosition.column , currentPosition.row - 1));
		}

		int randomIndex = Random.Range (0, possibleDirections.Count);

		return GetHouseInPosition(possibleDirections[randomIndex]);

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

	public bool isValidPosition(int _col, int _row) {
		if( _col >= 0 && _col < columns - 1 && _row >= 0 && _row <= rows - 1 ) {
			return true;
		} else {
			return false;
		}
	}

	public List <GridHouse> GetAllHouses() {
		return gridHouses;
	}

}
