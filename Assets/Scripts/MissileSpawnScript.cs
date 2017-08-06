using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MissileDelegate();
public delegate void HitDelegate();
public class MissileSpawnScript : MonoBehaviour {
	public MissileDelegate MissileHandler;
	public HitDelegate HitHandler;
	public void SpawnMissile(){
		if(MissileHandler != null){
			MissileHandler();
		}
	}
	void OnTriggerEnter()
	{
		HitHandler();
	}
}
