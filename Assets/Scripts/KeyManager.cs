using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {
	private static float PitchShiftFactor = 1.0f/12.0f;
	private static Dictionary<string, int> NoteMappings;
	public string m_Key;
	public float m_NoteTravelTime;
	public float m_KeyCoolDown = 1;
	public Transform m_MissilePrefab;
	private float m_LastPressed = 0;
	private AudioSource m_AudioSource;
	private Queue<GameObject> m_MissileWaitingRoom;
	// Use this for initialization
	void Start () {
		InitializeMapping();
		m_AudioSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
		m_AudioSource.pitch = Mathf.Pow(2f, NoteMappings[m_Key] * PitchShiftFactor);
		m_MissileWaitingRoom = new Queue<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton(m_Key)){
			if(Time.time - m_LastPressed > m_KeyCoolDown){
				m_LastPressed = Time.time;
				StartWave();
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
	void StartWave(){
		DelayedJumpScript jumpScript = null;
		for(int i = 0; i < transform.childCount; i++){
			jumpScript = transform.GetChild(i).GetComponent(typeof(DelayedJumpScript)) as DelayedJumpScript;
			jumpScript.JumpAfterDelay(i * m_NoteTravelTime);
		}
		GameObject waitingMissile = Instantiate(m_MissilePrefab, jumpScript.transform.position, Quaternion.LookRotation(Vector3.up)).gameObject;
		waitingMissile.SetActive(false);
		m_MissileWaitingRoom.Enqueue(waitingMissile);
		MissileSpawnScript spawner = jumpScript.transform.GetChild(0).GetComponent(typeof(MissileSpawnScript)) as MissileSpawnScript;
		if(spawner.MissileHandler == null){
			spawner.MissileHandler = FireMissiles;
		}
	}
	public void FireMissiles(){
		m_MissileWaitingRoom.Dequeue().SetActive(true);
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
