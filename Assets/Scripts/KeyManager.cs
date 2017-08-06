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
		MissileSpawnScript launchScript;
		for(int i = 0; i < transform.childCount; i++){
			launchScript = transform.GetChild(i).GetChild(0).GetComponent<MissileSpawnScript>();
			launchScript.HitHandler = ()=>{Destroy(gameObject);};
		}
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
		GameObject waitingMissile = UberPool.SharedInstance.GetObject(ObjectType.MissileD, GetLaunchLocation(), Quaternion.LookRotation(Vector3.up));
		MissileScript waitingMissileScript = waitingMissile.GetComponent<MissileScript>();
		waitingMissileScript.setDestinationUsingMouse();
		waitingMissileScript.enabled = false;
		waitingMissile.GetComponent<Renderer>().enabled = false;
		m_MissileWaitingRoom.Enqueue(waitingMissile);
		MissileSpawnScript spawner = jumpScript.transform.GetChild(0).GetComponent<MissileSpawnScript>();
		if(spawner.MissileHandler == null){
			spawner.MissileHandler = FireMissiles;
		}
	}
	public void FireMissiles(){
		GameObject waitingMissile = m_MissileWaitingRoom.Dequeue();
		waitingMissile.GetComponent<MissileScript>().enabled = true;
		waitingMissile.GetComponent<Renderer>().enabled = true;
	}
	public Vector3 GetLaunchLocation(){
		return transform.GetChild(transform.childCount-1).position;
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
