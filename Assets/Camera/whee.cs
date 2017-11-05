using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whee : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time), 0) * 3;
	}
}
