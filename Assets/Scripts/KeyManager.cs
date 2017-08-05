using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {

	public string m_Key;
	public float m_NoteTravelTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(m_Key)){
			//play music and stuff here. Also maybe launch missile
		}
	}
}
