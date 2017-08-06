using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float m_Speed;
	private Vector3 m_Destination;
	private Vector3 m_Direction;

	// Use this for initialization
	void Start () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float zFactor = (transform.position.z - ray.origin.z) / ray.direction.normalized.z;
		m_Destination = ray.origin + (ray.direction * zFactor);
		m_Direction = (m_Destination - transform.position).normalized;
		transform.rotation = Quaternion.LookRotation(m_Direction);
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Dot(m_Destination - transform.position, m_Direction) < 0){
			//Destroy self
		}
		else{
			transform.position += m_Direction * m_Speed;
		}
	}
}
