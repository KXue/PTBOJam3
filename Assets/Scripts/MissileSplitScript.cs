using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSplitScript : MonoBehaviour {
	public Transform m_SubMissilePrefab;
	public Transform m_KeysTransform;
	public int m_NumSubMissiles;
	public float m_SplitDelay; //-1 for no split;
	private float m_TimeofSplit;
	// Use this for initialization
	void Start () {
		m_TimeofSplit = Time.time + m_SplitDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_SplitDelay >= 0 && Time.time > m_TimeofSplit){
			for(int i = 0; i < m_NumSubMissiles; i++){
				int newKeyIndex = (int)(Random.value * m_KeysTransform.childCount);
				Transform subMissile = Instantiate(m_SubMissilePrefab, transform.position, transform.rotation);
				subMissile.gameObject.GetComponent<MissileScript>().setDestination(m_KeysTransform.GetChild(newKeyIndex).GetComponent<KeyManager>().GetLaunchLocation());
			}
			Destroy(gameObject);
		}
	}
}
