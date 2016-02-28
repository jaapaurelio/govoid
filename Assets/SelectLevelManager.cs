using UnityEngine;
using System.Collections;

public class SelectLevelManager : MonoBehaviour {

	public GameObject levelButtonGameObject;
	public GameObject levelGridContainerObject;

	// Use this for initialization
	void Start () {

		TextAsset bindata= Resources.Load<TextAsset>("Levels/Pack" + GameManager.instance.currentPackage);

		Debug.Log(bindata);

		Pack p = JsonUtility.FromJson<Pack>(bindata.text);
		Debug.Log(p.name);
		int numberOfLevels = p.levels.Length;

		for(int i = 1; i <= numberOfLevels; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelButton =
				Instantiate (levelButtonGameObject, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			levelButton.GetComponent<LevelSelectButton>().SetLevel(i);

			// This will put the object in the right position inside parent
			levelButton.transform.SetParent(levelGridContainerObject.transform, false);
		}

	}

}
