﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
	public float m_LifeTime;
	public float m_MaxSize;
	public static AudioSource ExplosionSFX;
	private float m_TimeOfBirth;
	// Use this for initialization
	void Start () {
		m_TimeOfBirth = Time.time;
		Destroy(gameObject, m_LifeTime);
		transform.localScale = Vector3.zero;
		if(ExplosionSFX != null){
			ExplosionSFX.PlayOneShot(ExplosionSFX.clip);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		float timeElapsed = Time.time - m_TimeOfBirth;
		float halfLife = m_LifeTime * 0.5f;
		float newScale = timeElapsed / halfLife;
		if(timeElapsed > halfLife){
			newScale = (1 - ((timeElapsed - halfLife) / halfLife));
		}
		newScale *= m_MaxSize;
		transform.localScale = new Vector3(newScale, newScale, newScale);
	}

	public static void setAudioSource(AudioSource source){
		if(ExplosionSFX == null || ExplosionSFX != source){
			ExplosionSFX = source;
		}
	}
}
