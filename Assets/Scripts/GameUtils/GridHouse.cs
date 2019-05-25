using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouse
{

	private int originalNumber;

	public GridPosition position;   
	private int number;
    private string[] actions;

	public int Number { get{return number;} }

	private int state = Constants.HOUSE_STATE_NORMAL;

	public int State { get{return state;} }
	public bool isHole = false;

	private GameObject gridHouseUIGameObject;
	public GridHouseUI gridHouseUIComponent;

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

	public void ResetHouse(){
		state = Constants.HOUSE_STATE_POSSIBLE;
		gridHouseUIComponent.ResetHouse();
	}

	public void SetHouseState(int newState) {
		state = newState;
		gridHouseUIComponent.SetState(newState);
	}

	public void SetActiveHouse() {
		number--;
		state = Constants.HOUSE_STATE_ACTIVE;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_ACTIVE);
		gridHouseUIComponent.SetNumber(number);

	}


    public void SetAsPrevious()
    {
        state = Constants.HOUSE_STATE_PREVIOUS;
        gridHouseUIComponent.SetState(Constants.HOUSE_STATE_PREVIOUS);
    }

    public void UnsetActive() {
		state = Constants.HOUSE_STATE_NORMAL;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_NORMAL);
	}

	public void SetGameObject(GameObject newGridHouseUI) {
		gridHouseUIGameObject = newGridHouseUI;
		gridHouseUIComponent = gridHouseUIGameObject.GetComponent<GridHouseUI>();

        gridHouseUIComponent.SetActions(actions);
    }

    public void Restart() {
		number = originalNumber;
		gridHouseUIComponent.SetNumber(number);

		if(number > 0) {
			gridHouseUIGameObject.SetActive(true);
		}
	}

	public void SetHouseMissing(){
		state = Constants.HOUSE_STATE_MISSING;
		gridHouseUIComponent.SetState(Constants.HOUSE_STATE_MISSING);
	}

	public void SetFinalNumber(int newNumber) {
		number = newNumber;
		originalNumber = newNumber;
	}

    public void SetActions(string[] _actions) {
        if(_actions != null)
        {
            actions = _actions;

        } else {
            actions = new string[0];
        }
    }

    public void Destroy() {
		Object.Destroy(gridHouseUIGameObject);
	}
}