﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = System.Random;      //Tells Random to use the Unity Engine random number generator.

public class LevelGrid
{
    public int columns;
    public int rows;
    private List<GridHouse> gridHouses = new List<GridHouse>();
    public string message = "";
    private Random rng;

    public LevelGrid(int _cols, int _rows, Random _rng)
    {
        rng = _rng;

        columns = _cols;
        rows = _rows;
        rng = _rng;

        CreateGrid();
    }

    public LevelGrid(int _cols, int _rows)
    {

        columns = _cols;
        rows = _rows;
        rng = new Random();

        CreateGrid();
    }

    //Assignment constructor.
    public void CreateGrid()
    {

        //Loop through x axis (columns).
        for (int x = 0; x < columns; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 0; y < rows; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridHouses.Add(new GridHouse(new GridPosition(x, y), 0));
            }
        }
    }

    public GridHouse ChooseRandomHouse()
    {
        return gridHouses[rng.Next(0, gridHouses.Count)];
    }

    public GridHouse ChooseRandomPossibleHouse()
    {
        GridHouse house;

        do
        {
            house = gridHouses[rng.Next(0, gridHouses.Count)];

        } while (house.isHole);

        return house;
    }

    public List<GridHouse> GetSiblings(GridHouse currentHouse)
    {

        List<GridHouse> siblings = new List<GridHouse>();

        GridPosition currentPosition = currentHouse.position;

        // Right
        GridPosition testPosition = new GridPosition(currentPosition.column + 1, currentPosition.row);
        if (IsValidPosition(testPosition))
        {
            siblings.Add(GetHouseInPosition(testPosition));
        }

        // Left
        testPosition = new GridPosition(currentPosition.column - 1, currentPosition.row);
        if (IsValidPosition(testPosition))
        {
            siblings.Add(GetHouseInPosition(testPosition));
        }

        // Top
        testPosition = new GridPosition(currentPosition.column, currentPosition.row + 1);
        if (IsValidPosition(testPosition))
        {
            siblings.Add(GetHouseInPosition(testPosition));
        }

        // Bottom
        testPosition = new GridPosition(currentPosition.column, currentPosition.row - 1);
        if (IsValidPosition(testPosition))
        {
            siblings.Add(GetHouseInPosition(testPosition));
        }

        return siblings;
    }

    public List<GridHouse> GetPossibleSiblings(GridHouse currentHouse)
    {

        List<GridHouse> siblings = GetSiblings(currentHouse);
        List<GridHouse> possibleSiblings = new List<GridHouse>();

        foreach (GridHouse house in siblings)
        {
            if (!house.isHole)
            {
                possibleSiblings.Add(house);
            }
        }

        return possibleSiblings;

    }


    public GridHouse GetHouseInPosition(GridPosition position)
    {

        foreach (GridHouse house in gridHouses)
        {
            if (house.position.column == position.column && house.position.row == position.row)
            {
                return house;
            }
        }

        return null;
    }

    public bool IsValidPosition(GridPosition gridPosition)
    {
        if (gridPosition.column >= 0 &&
            gridPosition.column < columns &&
            gridPosition.row >= 0 &&
            gridPosition.row < rows)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public List<GridHouse> GetAllHouses()
    {
        return gridHouses;
    }

}
