using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour {

	public GameObject levelButtonGameObject;
	public GameObject levelGridContainerObject;

	// Use this for initialization
	void Start () {

		// To be possible open game in any scene
		if(GameManager.instance == null) {
			SceneManager.LoadScene("MainMenuScene");
			return;
		}

		TextAsset bindata= Resources.Load<TextAsset>("Levels/Pack" + GameManager.instance.currentPackageNum);

		Pack p = JsonUtility.FromJson<Pack>(bindata.text);
		GameManager.instance.currentPackage = p;

		List<int> levelsDone =  GameManager.instance.playerStatistics.GetLevelsDoneFromPackage(GameManager.instance.currentPackageNum);

		// TODO remove 100
		for(int i = 1; i <= 100; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject levelButton =
				Instantiate (levelButtonGameObject, new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;

			bool done = false;

			if(levelsDone.Contains(i)) {
				done = true;
			}

			levelButton.GetComponent<LevelSelectButton>().SetLevel(i, done);

			// This will put the object in the right position inside parent
			levelButton.transform.SetParent(levelGridContainerObject.transform, false);
		}

	}

}
