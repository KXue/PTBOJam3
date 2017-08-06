using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSplitScript : MonoBehaviour {
	public Transform m_SubMissilePrefab;
	public Transform m_KeysTransform;
	public int m_NumSubMissiles;
	public float m_SplitDelay; //-1 for no split;
	private float m_TimeofSplit;
	void OnEnable () {
		m_TimeofSplit = Time.time + m_SplitDelay;
	}
	// Update is called once per frame
	void Update () {
		if(m_SplitDelay >= 0 && Time.time > m_TimeofSplit){
			for(int i = 0; i < m_NumSubMissiles; i++){
				int newKeyIndex = Random.Range(0, m_KeysTransform.childCount - 1);
				Transform subMissile = UberPool.SharedInstance.GetObject(ObjectType.MissileS, transform.position, transform.rotation).transform;
				subMissile.gameObject.GetComponent<MissileScript>().setDestination(m_KeysTransform.GetChild(newKeyIndex).GetComponent<KeyManager>().GetLaunchLocation());
			}
			gameObject.SetActive(false);
		}
	}
}
