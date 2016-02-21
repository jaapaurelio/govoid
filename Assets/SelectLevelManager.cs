using UnityEngine;
using System.Collections;

public class SelectLevelManager : MonoBehaviour {

	public GameObject LevelNameObject;
	private int packageNumber = 1;

	// Use this for initialization
	void Start () {
		int numOfLevels = 0;

		switch(packageNumber) {
		case 1:
			numOfLevels = Package1.numberOfLevels;
			break;
		default:
			numOfLevels = 0;
			break;
		}

		for(int i = 1; i <= numOfLevels; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelName =
				Instantiate (LevelNameObject, new Vector3 (0, i * -1.2f, 0f), Quaternion.identity) as GameObject;
		}
	}
	

}
