using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution(256, 224, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Debug.Log(src.ToString());
		Debug.Log(dest.ToString());
	}
}
