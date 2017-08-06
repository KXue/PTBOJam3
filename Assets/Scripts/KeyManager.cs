using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {
	private static float PitchShiftFactor = 1.0f/12.0f;
	private static Dictionary<string, int> NoteMappings;
	public string m_Key;
	public float m_NoteTravelTime = 0.5f;
	public float m_KeyCoolDown = 1;
	public Transform m_MissilePrefab;
	private float m_LastPressed = 0;
	private AudioSource m_AudioSource;
	// Use this for initialization
	void Start () {
		InitializeMapping();
		m_AudioSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
		m_AudioSource.pitch = Mathf.Pow(2f, NoteMappings[m_Key] * PitchShiftFactor);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton(m_Key)){
			if(Time.time - m_LastPressed > m_KeyCoolDown){
				m_LastPressed = Time.time;
				FireMissiles();
			}
			if(!m_AudioSource.isPlaying){
				m_AudioSource.Play();
			}
		}
		else if(Input.GetButtonUp(m_Key)){
			if(m_AudioSource.isPlaying){
				m_AudioSource.Stop();
			}
		}
	}
	void FireMissiles(){
		for(int i = 0; i < transform.childCount; i++){
			DelayedJumpScript jumpScript = transform.GetChild(i).GetComponent(typeof(DelayedJumpScript)) as DelayedJumpScript;
			jumpScript.JumpAfterDelay(i * m_NoteTravelTime);
		}
		//use last transform to spawn missiles
		Transform lastTrasform = transform.GetChild(transform.childCount-1);
		Instantiate(m_MissilePrefab, lastTrasform.position, Quaternion.LookRotation(Vector3.up));
	}
	static void InitializeMapping(){
		if(NoteMappings == null){
			NoteMappings = new Dictionary<string, int>();
			string[] notes = {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"};
			int gIndex = 7;
			for(int i = 0; i < notes.Length; i++){
				NoteMappings[notes[i]] = i-gIndex;
			}
		}
	}
}
