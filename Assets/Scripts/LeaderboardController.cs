using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour {

	public GameObject leaderboardItem;
	public GameObject leaderboardItemEditable;
	int scoreToSave;

	GameObject RestartButton;
	GameObject MainMenuButton;
	GameObject SaveScoreButton;
	GameObject nameSaved;

	// Use this for initialization
	void Start () {
		RestartButton = GameObject.Find("RestartButton");
		MainMenuButton = GameObject.Find("MainMenuButton");
		SaveScoreButton = GameObject.Find("SaveScoreButton");

		DisplayAndSaveHighScores(PlayerPrefs.GetInt("LastScore"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void callSave() {
		string name = GameObject.Find("NameToSave").GetComponent<Text>().text;
		SaveScore(name, scoreToSave);
		RestartButton.SetActive(true);
		MainMenuButton.SetActive(true);
		SaveScoreButton.SetActive(false);

		GameObject.Find("InputField").SetActive(false);
		nameSaved.SetActive(true);
		nameSaved.GetComponent<Text>().text = name;
	}

	public void SaveScore(string name, int score) {
		int newScore;
		string newName;
		int oldScore;
		string oldName;
		newScore = score;
		newName = name;

		for(int i=0; i < 10; i++) {
			if(PlayerPrefs.HasKey(i + "Score")) {
				if(PlayerPrefs.GetInt(i + "Score") < newScore){
					// new score is higher than the stored score
					oldScore = PlayerPrefs.GetInt(i + "Score");
					oldName = PlayerPrefs.GetString(i + "ScoreName");
					PlayerPrefs.SetInt(i + "Score", newScore);
					PlayerPrefs.SetString(i + "ScoreName", newName);
					newScore = oldScore;
					newName = oldName;
				}
			} else {
				PlayerPrefs.SetInt(i + "Score", newScore);
				PlayerPrefs.SetString(i + "ScoreName", newName);
				newScore = 0;
				newName = "";
			}
		}
	}

	public void DisplayAndSaveHighScores(int score) {
		int displacement = 0; //used when the player makes a highscore to move all the low score by 1 rank
		//This isn't very pretty...
		scoreToSave = score;

		for(int i = 0; i < 10; i++) {
			int storedScore = PlayerPrefs.GetInt(i + displacement + "Score");
			string storedName = PlayerPrefs.GetString(i + displacement + "ScoreName");

			if (storedScore < score && displacement >=0) {
				GameObject scoreObject = GameObject.Instantiate(leaderboardItemEditable, GameObject.Find("ScoreContainer").transform);
				nameSaved = GameObject.Find("NameSaved");
				nameSaved.SetActive(false);
				scoreObject.transform.GetChild(0).Find("Rank").GetComponent<Text>().text = (i+1).ToString();
				scoreObject.transform.GetChild(0).Find("Score").GetComponent<Text>().text = score.ToString();
				displacement = -1;
			} else {
				GameObject scoreObject = GameObject.Instantiate(leaderboardItem, GameObject.Find("ScoreContainer").transform);
				string placeholder = "xxxx";
				if (string.IsNullOrEmpty(PlayerPrefs.GetString(i + "ScoreName"))) {
					storedName = placeholder;
				}
				scoreObject.transform.GetChild(0).Find("Rank").GetComponent<Text>().text = (i+1).ToString();
				scoreObject.transform.GetChild(0).Find("Name").GetComponent<Text>().text = storedName;
				scoreObject.transform.GetChild(0).Find("Score").GetComponent<Text>().text = storedScore.ToString();
			}
		}

		if (displacement < 0) {
			RestartButton.SetActive(false);
			MainMenuButton.SetActive(false);
			SaveScoreButton.SetActive(true);
		} else {
			RestartButton.SetActive(true);
			MainMenuButton.SetActive(true);
			SaveScoreButton.SetActive(false);
		}

		PlayerPrefs.SetInt("LastScore",-1);
	}
}
