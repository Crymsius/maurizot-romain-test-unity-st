using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	int level;
	int score;
	int lines;
	int time;
	int[] tableScore;

	GameObject overlayPanel;
	GameObject pausePanel;
	GameObject gameOverPanel;

	Text levelDisplay;
	Text scoreDisplay;
	Text linesDisplay;
	Text timeDisplay;

	// Use this for initialization
	void Start () {
		overlayPanel = GameObject.Find("OverlayPanel");
		pausePanel = GameObject.Find("PausePanel");
		gameOverPanel = GameObject.Find("GameOverPanel");
		overlayPanel.SetActive(false);
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);


		levelDisplay = GameObject.Find("LevelValue").GetComponent<Text>();
		scoreDisplay = GameObject.Find("ScoreValue").GetComponent<Text>();
		linesDisplay = GameObject.Find("LinesValue").GetComponent<Text>();
		timeDisplay = GameObject.Find("TimeValue").GetComponent<Text>();


		level = 0;
		score = 0;
		lines = 0;
		time = 0;

		UpdateDisplay();

		tableScore = new int[4];
		tableScore[0]=40;
		tableScore[1]=100;
		tableScore[2]=300;
		tableScore[3]=1200;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")) {
			Pause();
			overlayPanel.SetActive(true);
			pausePanel.SetActive(true);
		}
		
		InvokeRepeating("UpdateTime", 0f, 1f);
	}

	public void Pause() {
		Debug.Log("paused game");
	}

	public void NewGame() {
		Debug.Log("new game");
	}

	public void GameOver() {
		Debug.Log("game over");
		overlayPanel.SetActive(true);
		gameOverPanel.SetActive(true);
	}

	public void IncreaseLevel() {
		level++;
	}

	public void IncreaseScore(int m_lines) {
		lines += m_lines;
		score += (int)tableScore[m_lines-1] * (level+1);

		UpdateDisplay();
	}

	void UpdateDisplay() {
		levelDisplay.text = level.ToString();
		scoreDisplay.text = score.ToString();
		linesDisplay.text = lines.ToString();
	}

	void UpdateTime(){
		time = (int)Time.time;
		timeDisplay.text = time.ToString();
	}

}
