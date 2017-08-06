using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIScript : MonoBehaviour {
	public Text m_ScoreText;
	public Text m_HighScoreText;
	private float m_Score;
	private float m_HighScore;

	// Use this for initialization
	void Start () {
		m_HighScore = PlayerPrefs.GetFloat("HighScore");
		m_Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		m_Score += Time.deltaTime;
		if(m_Score > m_HighScore){
			m_HighScore = m_Score;
		}
		RefreshUI();
	}
	void RefreshUI(){
		m_ScoreText.text = "Score: " + m_Score.ToString("F1");
		m_HighScoreText.text = "HighScore: " + m_HighScore.ToString("F1");
	}
	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		if(m_HighScore >= PlayerPrefs.GetFloat("HighScore")){
			PlayerPrefs.SetFloat("HighScore", m_HighScore);
		}
		PlayerPrefs.Save();
	}
}
