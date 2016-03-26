﻿using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public GameObject tapToRestartGameObject;
	public GameObject arrowToInstanciate;
	public GameObject gameOverPopupObject;
	public GameObject gamePausedPopupObject;

	protected bool playing = false;

	protected GameObject arrowFrom;
	protected GameObject arrowToTop;
	protected GameObject arrowToBottom;
	protected GameObject arrowToLeft;
	protected GameObject arrowToRight;
	protected Transform boardHolder;
	protected bool canInteractWithBoard = true;

	public virtual void Start() {
		Debug.Log("Board manager Start");
		BoardSetup();

		tapToRestartGameObject.SetActive(false);

		arrowFrom = Instantiate (arrowToInstanciate, new Vector3 (-2, 0, 0f), Quaternion.identity) as GameObject;
		arrowFrom.transform.SetParent(boardHolder);
		arrowFrom.GetComponent<SpriteRenderer>().color = new Color32(76, 73, 88, 255);

		arrowToTop = Instantiate (arrowToInstanciate, new Vector3 (2, 0, 0f), new Quaternion(0, 0, 90, 90)) as GameObject;
		arrowToTop.transform.SetParent(boardHolder);

		arrowToBottom = Instantiate (arrowToInstanciate, new Vector3 (3, 0, 0f), new Quaternion(0f, 0f, -90, 90)) as GameObject;
		arrowToBottom.transform.SetParent(boardHolder);

		arrowToLeft = Instantiate (arrowToInstanciate, new Vector3 (4, 0, 0f), new Quaternion(0, 0, 180, 0)) as GameObject;
		arrowToLeft.transform.SetParent(boardHolder);

		arrowToRight = Instantiate (arrowToInstanciate, new Vector3 (5, 0, 0f), Quaternion.identity) as GameObject;
		arrowToRight.transform.SetParent(boardHolder);
	}

	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject("Board").transform;
		boardHolder.position = new Vector3(-5.0f, -8.0f, 0.0f);

	}


	public virtual void NewGame() {

		gameOverPopupObject.GetComponent<GameOverPopup>().Hide();
		gamePausedPopupObject.GetComponent<GamePausedPopup>().Hide();
		tapToRestartGameObject.SetActive(false);
		boardHolder.gameObject.SetActive(true);

		canInteractWithBoard = false;
		this.playing = false;

		StartCoroutine(CanPlay());
		StartCoroutine(CanInteractWithBoardAgain());
	
	}

	protected IEnumerator CanPlay() {
		yield return new WaitForSeconds(0.1f);
		playing = true;
	}


	protected IEnumerator CanInteractWithBoardAgain() {
		yield return new WaitForSeconds(0.1f);
		canInteractWithBoard = true;
	}

}
