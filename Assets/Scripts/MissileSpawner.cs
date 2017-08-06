using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
	public float m_SpawnInterval;
	public float m_MinSpawnInterval;
	public float m_SpawnDecayFactor;
	public float m_SplitProbability;
	public float m_SplitGrowthFactor;
	public float m_XRange;
	public Transform m_KeysReference;
	public Transform m_AttackMissilePrefab;
	private float m_NextLaunchTime;
	private float[] m_SpawnCountFraction = {1f/2f, 1f/3f, 1f/4f, 1f/5f};
	private float m_SpawnTotalFraction;
	// Use this for initialization
	void Start () {
		m_NextLaunchTime = Time.time + m_SpawnInterval;
		m_SpawnTotalFraction = 0;
		for(int i = 0; i < m_SpawnCountFraction.Length; i++){
			m_SpawnTotalFraction += m_SpawnCountFraction[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > m_NextLaunchTime){
			float missileNumberDecider = Random.value * m_SpawnTotalFraction;
			for(int i = 0; i < m_SpawnCountFraction.Length; i++){
				missileNumberDecider -= m_SpawnCountFraction[i];
				if(missileNumberDecider < 0){
					SpawnMissiles(i+1);
					break;
				}
			}
			m_SplitProbability = Mathf.Min(1, m_SplitProbability * m_SplitGrowthFactor);
			m_SpawnInterval = Mathf.Max(m_SpawnInterval * m_SpawnDecayFactor, m_MinSpawnInterval);
			m_NextLaunchTime = Time.time + m_SpawnInterval;
		}
	}
	private void SpawnMissiles(int numMissiles){
		if(m_KeysReference.childCount > 0){
			for(int i = 0; i < numMissiles; i++){
				SpawnMissile();
			}
		}
	}
	private void SpawnMissile(){
		float missileX = Random.Range(-m_XRange, m_XRange);
		Transform missile = UberPool.SharedInstance.GetObject(ObjectType.MissileA, transform.position + new Vector3(missileX, 0, 0), Quaternion.LookRotation(Vector3.down)).transform;
		MissileScript missileMovement = missile.gameObject.GetComponent<MissileScript>();
		int newKeyIndex = Random.Range(0, m_KeysReference.childCount - 1);
		missileMovement.setDestination(m_KeysReference.GetChild(newKeyIndex).GetComponent<KeyManager>().GetLaunchLocation());
		
		MissileSplitScript missileSplit = missile.gameObject.GetComponent<MissileSplitScript>();
		missileSplit.m_KeysTransform = m_KeysReference;
		missileSplit.m_NumSubMissiles = Random.Range(2, 4);
		//splitting missile
		if(Random.value < m_SplitProbability){
			missileSplit.m_SplitDelay = Random.Range(1.0f, 2.0f);
		}
		else{
			missileSplit.m_SplitDelay = -1.0f;
		}
	}
}
