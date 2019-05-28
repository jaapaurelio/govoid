using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class GridHouse
{

    public int originalNumber;
    public GridPosition position;
    public int number;
    public int state = Constants.HOUSE_STATE_NORMAL;
    public bool isHole = false;
    public bool isTeleport = false;
    public bool isEndpoint = false;

    public string[] actions;
    public GridHouseUI ui;

    public GridHouse(GridPosition _position, int _number)
    {
        position = _position;
        number = _number;
        originalNumber = number;
    }
}