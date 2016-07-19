using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialManager : BoardManager {

	Pack package;
	int levelNumber = 1;
	int levelStep = 1;
	bool followingTutorial = true;

	public GameObject hand;

	public override void Start(){

		base.Start();

		GameManager.instance.googleAnalytics.LogScreen("Tutorial");

		// Get json with level layout
		TextAsset bindata= Resources.Load<TextAsset>("Levels/Tutorial");

		package = JsonUtility.FromJson<Pack>(bindata.text);

		boardHolder.position = new Vector3(-5, -5, 0);
		NewGame();

	}

	public void Update() {
		if (playing) {
			base.BoardInteraction();
		}
	}

	public override void NewGame() {
		base.NewGame();

		GameManager.instance.googleAnalytics.LogEvent("InfinityMode", "StartNewGame", "", 0);

		NewLevel();
	}

	protected IEnumerator AnimateHand() {
		while (true) {
			yield return new WaitForSeconds(1.5f);
			hand.GetComponent<Animator>().Play("TutorialHand1");
		}
	}

	protected override void NewLevel() {

		base.DestroyCurrentLevel();

		currentLevelGrid = LevelJsonGenerator.CreateLevel(package, levelNumber);

		StartCoroutine(AnimateHand());

		StartTutorial();

		base.NewLevel();

	}

	private void StartTutorial(){
		
		followingTutorial = true;

		levelStep = 1;

		if( levelNumber == 1) {
			HandPosition(1, 1);
			GameObject.Find("TutorialMessage").GetComponent<TextMesh>().text = "Tap or slide the numbers to decrease it.\nYou win when the board is clear.";

		} else if(levelNumber == 2) {
			HandPosition(1, 1);
			GameObject.Find("TutorialMessage").GetComponent<TextMesh>().text = "You can start anywhere.\nChoose your path using the green circles.";
		
		} else if(levelNumber == 3) {
			HandPosition(1, 1);
			GameObject.Find("TutorialMessage").GetComponent<TextMesh>().text = "Numbers indicate how many times\nyou have to pass through them.";
		}

	}

	protected override void PossibleHouseClicked(GridHouse clickedHouse) {
		
		if(!followingTutorial){
			return;
		}

		levelStep++;

		switch (levelNumber) {

			case 1:

			if (levelStep == 2 && clickedHouse.position.column == 1 && clickedHouse.position.row == 1){
				HandPosition(2, 1);

			} else if (levelStep == 3 && clickedHouse.position.column == 2 && clickedHouse.position.row == 1){
				HandPosition(3, 1);

			} else if (levelStep <= 3) {
				WrongHouse();
			}
				
			break;

			case 2:

			if(levelStep == 2 && clickedHouse.position.column == 1 && clickedHouse.position.row == 1) {
				HandPosition(2, 1);

			} else if(levelStep == 3 && clickedHouse.position.column == 2 && clickedHouse.position.row == 1) {
				HandPosition(2, 2);

			} else if(levelStep == 4 && clickedHouse.position.column == 2 && clickedHouse.position.row == 2) {
				HandPosition(3, 2);

			} else if(levelStep == 5 && clickedHouse.position.column == 3 && clickedHouse.position.row == 2) {
				HandPosition(3, 1);

			} else if(levelStep == 6 && clickedHouse.position.column == 3 && clickedHouse.position.row == 1) {
				HandPosition(3, 0);
			
			} else if(levelStep == 7 && clickedHouse.position.column == 3 && clickedHouse.position.row == 0) {
				HandPosition(2, 0);

			} else if(levelStep == 8 && clickedHouse.position.column == 2 && clickedHouse.position.row == 0) {
				HandPosition(1, 0);

			} else if(levelStep == 9 && clickedHouse.position.column == 1 && clickedHouse.position.row == 0) {
				HandPosition(0, 0);

			} else if(levelStep == 10 && clickedHouse.position.column == 0 && clickedHouse.position.row == 0) {
				HandPosition(0, 1);

			} else if(levelStep == 11 && clickedHouse.position.column == 0 && clickedHouse.position.row == 1) {
				HandPosition(0, 2);

			} else if(levelStep == 12 && clickedHouse.position.column == 0 && clickedHouse.position.row == 2) {
				HandPosition(1, 2);

			} else if (levelStep <= 12) {
				WrongHouse();
			}

			break;

			case 3:

			if(levelStep == 2 && clickedHouse.position.column == 1 && clickedHouse.position.row == 1) {
				HandPosition(2, 1);

			} else if(levelStep == 3 && clickedHouse.position.column == 2 && clickedHouse.position.row == 1) {
				HandPosition(3, 1);

			} else if(levelStep == 4 && clickedHouse.position.column == 3 && clickedHouse.position.row == 1) {
				HandPosition(3, 2);
			
			} else if(levelStep == 5 && clickedHouse.position.column == 3 && clickedHouse.position.row == 2) {
				HandPosition(2, 2);

			} else if(levelStep == 6 && clickedHouse.position.column == 2 && clickedHouse.position.row == 2) {
				HandPosition(2, 1);
			
			} else if(levelStep == 7 && clickedHouse.position.column == 2 && clickedHouse.position.row == 1) {
				HandPosition(2, 0);

			} else if (levelStep <= 7) {
				WrongHouse();
			}

			break;

			default:
				break;
		}


	}

	private void HandPosition(int col, int row) {
		hand.transform.position = new Vector3(col * 2.5f - 5f, row * 2.5f - 5 - 1.3f , 0f);
	}

	private void WrongHouse(){
		followingTutorial = false;
		HideHand();
		GameObject.Find("TutorialMessage").GetComponent<TextMesh>().text = "You're bold!\nCan you solve it alone?";
	}

	protected override void LostLevel(){
		base.LostLevel();
	
		// click the restart button
		GameObject.Find("TutorialMessage").GetComponent<TextMesh>().text = "No possible way out.\nTap the button to restart level.";
		hand.transform.position = new Vector3(0.25f, 3f, 0f);
	}

	protected override void WonLevel() {
		base.WonLevel();
		followingTutorial = true;
		levelStep = 1;
		levelNumber++;
		HideHand();
	}

	public override void RestartGame(){
		base.RestartGame();

		StartTutorial();
	}
		
	protected override void AfterWonAnimation() {

		if(levelNumber <= package.levels.Length) {
			NewLevel();

		} else {
			PlayerPrefs.SetInt(Constants.PS_HAVE_SEEN_TUTORIAL, 1);
			SceneManager.LoadScene("MainMenuScene");
		}
	}

	private void HideHand(){
		hand.transform.position = new Vector3(90f,90f,90f);
	}


}
