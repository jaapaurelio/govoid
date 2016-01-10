using UnityEngine;
using System.Collections;

public class GridHouse
{
	public GridPosition position;           
	public int number;

	//Assignment constructor.
	public GridHouse (GridPosition _position, int _number)
	{
		position = _position;
		number = _number;
	}

	public int IncreaseNumber() {
		number++;

		return number;
	}

	public int DecreaseNumber() {
		number--;

		return number;
	}
}