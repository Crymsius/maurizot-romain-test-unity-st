using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void StartGame () {
		SceneManager.LoadScene ("GameScene");
	}

	public void Leaderboard () {
		SceneManager.LoadScene ("LeaderboardScene");
	}

	public void MainMenu () {
		SceneManager.LoadScene ("MenuScene");
	}

	public void ExitGame () {
		Application.Quit();
	}
}