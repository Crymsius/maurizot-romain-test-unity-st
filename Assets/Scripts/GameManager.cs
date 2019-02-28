using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	int level;
	int score;
	int lines;
	int time;
	int[] tableScore;
	float[] tableSpeedLevel;

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

		tableSpeedLevel = new float[10];
		tableSpeedLevel[0]=1f;
		tableSpeedLevel[1]=2f;
		tableSpeedLevel[2]=5f;
		tableSpeedLevel[3]=8f;
		tableSpeedLevel[4]=10f;
		tableSpeedLevel[5]=12f;
		tableSpeedLevel[6]=14f;
		tableSpeedLevel[7]=16f;
		tableSpeedLevel[8]=18f;
		tableSpeedLevel[9]=20f;

		Time.timeScale = 1;
		InvokeRepeating("UpdateTime", 0f, 1f);
		InvokeRepeating("IncreaseLevel", 5f, 5f);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")) {
			Pause();
			overlayPanel.SetActive(true);
			pausePanel.SetActive(true);
		}
	}

	public void Pause() {
		Time.timeScale = 0;
	}

	public void Resume() {
		Time.timeScale = 1;
	}

	public void GameOver() {
		Time.timeScale = 0;
		overlayPanel.SetActive(true);
		gameOverPanel.SetActive(true);
		PlayerPrefs.SetInt("LastScore", score);
		SceneManager.LoadScene ("LeaderboardScene");
	}

	public void IncreaseLevel() {
		if (level < 9) {
			level++;
			UpdateDisplay();
		}
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

	public float GetCurrentSpeed() {
		return tableSpeedLevel[level];
	}
}
