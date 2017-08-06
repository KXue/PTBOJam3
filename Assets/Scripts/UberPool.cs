using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType {MissileA, MissileS, MissileD, Explosion};
public class UberPool : MonoBehaviour {
	public bool m_Growing;
	public int m_InitSize;
	public Transform[] m_PooledPrefabs;
	public ObjectType[] m_PooledIndices;
	public static UberPool SharedInstance;
	private Dictionary<ObjectType, List<GameObject>> m_Pool;
	void Awake() {
		SharedInstance = this;
		PopulatePool();
	}
	void PopulatePool(){
		m_Pool = new Dictionary<ObjectType, List<GameObject>>();
		for(int i = 0; i < m_PooledIndices.Length; i++){
			List<GameObject> currentList = new List<GameObject>();
			m_Pool[m_PooledIndices[i]] = currentList;
			for(int j = 0; j < m_InitSize; j++){
				GameObject newObject = Instantiate(m_PooledPrefabs[(int)m_PooledIndices[i]]).gameObject;
				newObject.SetActive(false);
				currentList.Add(newObject);
			}
		}
	}
	public GameObject GetObject(ObjectType type, Vector3 newPosition, Quaternion newRotation){
		GameObject retVal = FindNextAvailable(type);
		if(retVal == null){
			if(m_Growing){
				retVal = GrowPool(type);
			}
		}
		if(retVal != null){
			retVal.transform.position = newPosition;
			retVal.transform.rotation = newRotation;
			retVal.SetActive(true);
		}
		return retVal;
	}
	private GameObject FindNextAvailable(ObjectType type){
		GameObject retVal = null;
		foreach(GameObject o in m_Pool[type]){
			if(!o.activeInHierarchy){
				retVal = o;
				break;
			}
		}
		return retVal;
	}
	private GameObject GrowPool(ObjectType type){
		int foundIndex = 0;
		for(int i = 0; i < m_PooledIndices.Length; i++){
			if(m_PooledIndices[i] == type){
				foundIndex = i;
				break;
			}
		}
		GameObject freshObject = Instantiate(m_PooledPrefabs[foundIndex]).gameObject;
		m_Pool[type].Add(freshObject);
		return freshObject;
	}
}
