using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float m_Speed;
	public Transform m_ExplosionPrefab;
	private Vector3 m_Destination;
	private Vector3 m_Direction;

	// Use this for initialization
	void Awake () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float zFactor = (transform.position.z - ray.origin.z) / ray.direction.normalized.z;
		m_Destination = ray.origin + (ray.direction * zFactor);
		m_Direction = (m_Destination - transform.position).normalized;
		transform.rotation = Quaternion.LookRotation(m_Direction);
	}
	
	// Update is called once per frame
	void Update () {}
	void FixedUpdate()
	{
		if(Vector3.Dot(m_Destination - transform.position, m_Direction) < 0){
			Transform explosion = Instantiate(m_ExplosionPrefab, transform.position, Quaternion.LookRotation(Vector3.up));
			Destroy(explosion.gameObject, 3);
			//Destroy self
			Destroy(gameObject);
		}
		else{
			transform.position += m_Direction * m_Speed * Time.deltaTime;
		}
	}
}
