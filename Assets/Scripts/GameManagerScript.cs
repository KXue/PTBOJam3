using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public Transform m_KeysContainer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_KeysContainer.childCount == 0){
			//gameover
			Debug.Log("OVER");
		}
	}
}
