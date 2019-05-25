using UnityEngine;
using System.Collections;

public static class LevelJsonGenerator  {

	public static LevelGrid CreateLevel(Pack pack, int levelNum) {

		Level level = pack.levels[levelNum - 1];

		Debug.Log("message" + level.message);

		LevelGrid levelGrid = new LevelGrid(level.columns, level.rows); 

		if(level.message != null){
			levelGrid.message = level.message;
		}

		Debug.Log("level " + level.columns + " " + level.rows);
        int row = level.rows - 1;
        int col = 0;
		GridHouse gridhouse;
        Debug.Log(level.grid);

		for(int i = 0; i < level.grid.Length; i++) {

            gridhouse = levelGrid.GetHouseInPosition(new GridPosition(col, row));

            gridhouse.SetFinalNumber(level.grid[i].number);
            gridhouse.SetActions(level.grid[i].actions);

            if (col == level.columns - 1)
            {
                Debug.Log("reset cols");
                col = -1;
                row--;
            }

            col++;

        }

        Debug.Log("level grid");
		Debug.Log(level.grid);

		return levelGrid;

	}

}
