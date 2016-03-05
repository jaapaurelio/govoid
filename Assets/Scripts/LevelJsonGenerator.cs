using UnityEngine;
using System.Collections;

public static class LevelJsonGenerator  {

	public static LevelGrid CreateLevel(Pack pack, int levelNum) {

		Level level = pack.levels[levelNum];

		LevelGrid levelGrid = new LevelGrid(level.columns, level.rows); 

		Debug.Log("level " + level.columns + " " + level.rows);
		int row = 0;
		int col = 0;
		GridHouse gridhouse; 
		for(int i = 0; i < level.grid.Length; i++) {

			Debug.Log("col " + col + " " + " row " + row);

			// For an easy level definition in the json, row and col are switch
			gridhouse = levelGrid.GetHouseInPosition(new GridPosition(row, col));

			gridhouse.SetFinalNumber(level.grid[i]);

			if(row == level.rows - 1 ) {
				Debug.Log("reset cols");
				row = -1;
				col++;
			}

			row++;
		}

		Debug.Log("level grid");
		Debug.Log(level.grid);

		return levelGrid;

	}

}
