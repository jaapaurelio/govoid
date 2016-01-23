﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouse
{

	private int originalNumber;

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
		originalNumber = number;
	}

	public void IncreaseNumber() {
		number++;
	}

	public void SetState(int newState) {
		state = newState;
		gridHouseUIComponent.SetState(newState);
	}

	public void SetActiveHouse(List<int> possibleDirections) {

		gridHouseUIComponent.SetPossibleDirections(possibleDirections);
		SetActiveHouse();
	}

	public void SetActiveHouse() {
		number--;
		state = Constants.HOUSE_STATE_ACTIVE;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_ACTIVE);
		gridHouseUIComponent.SetNumber(number);
	}

	public void UnsetActive() {
		state = Constants.HOUSE_STATE_NORMAL;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_NORMAL);
		gridHouseUIComponent.HidePossibleDirections();
	}

	public void SetGameObject(GameObject newGridHouseUI) {
		gridHouseUIGameObject = newGridHouseUI;
		gridHouseUIComponent = gridHouseUIGameObject.GetComponent<GridHouseUI>();
	}

	public void Restart() {
		number = originalNumber;
		gridHouseUIComponent.SetNumber(number);
		gridHouseUIComponent.HidePossibleDirections();
	}

	public void SetFinalNumber(int newNumber) {
		number = newNumber;
		originalNumber = newNumber;
	}

	public void Destroy() {
		Object.Destroy(gridHouseUIGameObject);
	}

}