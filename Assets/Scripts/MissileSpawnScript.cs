using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MissileDelegate();
public class MissileSpawnScript : MonoBehaviour {
	public MissileDelegate MissileHandler;
	public void SpawnMissile(){
		if(MissileHandler != null){
			MissileHandler();
		}
	}
}
