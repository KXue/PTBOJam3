using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public Text m_HighScoreText;
	// Use this for initialization
	void Start () {
		m_HighScoreText.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("F1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartGame(){
		SceneManager.LoadSceneAsync("mainScene");
 		// Application.LoadLevel ("mainScene");
	}
	public void ExitGame(){
		Application.Quit();
	}
	void Exit(){
		
	}
}
