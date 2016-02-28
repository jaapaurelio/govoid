using UnityEngine;
using System.Collections;

public class SelectLevelManager : MonoBehaviour {

	public GameObject LevelNameObject;

	// Use this for initialization
	void Start () {

		TextAsset bindata= Resources.Load<TextAsset>("Levels/Pack1");

		Debug.Log(bindata);

		Pack p = JsonUtility.FromJson<Pack>(bindata.text);
		Debug.Log(p.name);
		int numberOfLevels = p.levels.Length;

		for(int i = 1; i <= numberOfLevels; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelName =
				Instantiate (LevelNameObject, new Vector3 (0, i * -1.2f, 0f), Quaternion.identity) as GameObject;
		}

	}

}
