﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PackageLister : MonoBehaviour {

	public GameObject packagename;

	void Start() {

		CreatePackageList();
	}

	void CreatePackageList() {
		for(var i = 1; i <= PackagesInfo.numberOfPackages; i++) {
			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			GameObject packName =
				Instantiate (packagename, new Vector3 (0, (i * -2f) + 4f, 0f), Quaternion.identity) as GameObject;

			string packInfo = " " + GameManager.instance.playerStatistics.GetNumberOfDoneLevelsFromPackage(i) + " from " + 15;
			packName.GetComponent<PackgeName>().SetNumber(i, packInfo);
		}
	}

	public void OpenPackage(int num) {
		GameManager.instance.currentPackageNum = num;
        SceneManager.LoadScene("SelectLevelScene");
	}
}
