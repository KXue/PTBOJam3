using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource m_MusicSource;
	public AudioSource m_ExplosionSource;
	// Use this for initialization
	void Start () {
		ExplosionScript.setAudioSource(m_ExplosionSource);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
