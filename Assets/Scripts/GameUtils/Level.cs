using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

[System.Serializable]
public struct Level
{

	public int columns;
	public int rows;
	public string message;
	public int[] grid;

}
