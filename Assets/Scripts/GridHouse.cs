using UnityEngine;
using System.Collections;

public class GridHouse
{
	public GridPosition position;   
	private int number;

	public int Number { get{return number;} }

	private int state = Constants.HOUSE_STATE_NORMAL;

	public int State { get{return state;} }

	private GameObject gridHouseUIGameObject;
	private GridHouseUI gridHouseUIComponent;

	//Assignment constructor.
	public GridHouse (GridPosition _position, int _number)
	{
		position = _position;
		number = _number;
	}

	public void IncreaseNumber() {
		number++;
	}

	public void SetState(int newState) {
		state = newState;
		gridHouseUIComponent.SetState(newState);
	}

	public void SetActive() {
		number--;
		state = Constants.HOUSE_STATE_ACTIVE;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_ACTIVE);
		gridHouseUIComponent.SetNumber(number);
	}

	public void UnsetActive() {
		state = Constants.HOUSE_STATE_NORMAL;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_NORMAL);
	}

	public void SetGameObject(GameObject newGridHouseUI) {
		gridHouseUIGameObject = newGridHouseUI;
		gridHouseUIComponent = gridHouseUIGameObject.GetComponent<GridHouseUI>();
	}
}