using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectLevelManager : MonoBehaviour {

	public GameObject levelButtonGameObject;
	public GameObject levelGridContainerObject;

	// Use this for initialization
	void Start () {

		TextAsset bindata= Resources.Load<TextAsset>("Levels/Pack" + GameManager.instance.currentPackageNum);

		Pack p = JsonUtility.FromJson<Pack>(bindata.text);
		GameManager.instance.currentPackage = p;
		Debug.Log(p.name);
		int numberOfLevels = p.levels.Length;

		List<int> levelsDone =  GameManager.instance.playerStatistics.GetLevelsDoneFromPackage(GameManager.instance.currentPackageNum);

		for(int i = 1; i <= numberOfLevels; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelButton =
				Instantiate (levelButtonGameObject, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			string done = "not done";

			if(levelsDone.Contains(i)) {
				done = "done";
			}

			levelButton.GetComponent<LevelSelectButton>().SetLevel(i, done);

			// This will put the object in the right position inside parent
			levelButton.transform.SetParent(levelGridContainerObject.transform, false);
		}

	}

}
