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

		for(int i = 0; i < level.grid.Length; i++) {

			Debug.Log("col " + col + " " + " row " + row);

			// For an easy level definition in the json, row and col are switch
			gridhouse = levelGrid.GetHouseInPosition(new GridPosition(col, row));

			gridhouse.SetFinalNumber(level.grid[i]);

			if(col == level.columns - 1) {
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
