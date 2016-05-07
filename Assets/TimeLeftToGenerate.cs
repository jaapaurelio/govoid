using UnityEngine;
using System.Collections;
using DateTime = System.DateTime;

public class TimeLeftToGenerate : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		string dateToGenerate = PlayerPrefs.GetString(Constants.PS_DATE_TO_GENERATE_LEVELS);

		// Only show if we have a time
		if(dateToGenerate != "") {
			DateTime date = DateTime.Parse(dateToGenerate);

			System.TimeSpan timeLeft = date.Subtract(DateTime.Now);

			string text = "new levels in ";

			if(timeLeft.Minutes > 1) {
				text += timeLeft.Minutes + " and " + timeLeft.Seconds + " seconds";
			} else {
				text += timeLeft.Seconds + " seconds";
			}

			gameObject.GetComponent<TextMesh>().text = text;
		} else {
			gameObject.GetComponent<TextMesh>().text = "complete all levels to generate more";
		}
	}
}
