using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float m_Speed;
	public Transform m_ExplosionPrefab;
	private Vector3 m_Destination;
	private Vector3 m_Direction;


	public void setDestinationUsingMouse(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float zFactor = (transform.position.z - ray.origin.z) / ray.direction.normalized.z;
		setDestination(ray.origin + (ray.direction * zFactor));
		
	}
	public void setDestination(Vector3 destination){
		m_Destination = destination;
		m_Direction = (m_Destination - transform.position).normalized;
		transform.rotation = Quaternion.LookRotation(m_Direction);
	}
	// Update is called once per frame
	void Update () {}
	void FixedUpdate()
	{
		if(Vector3.Dot(m_Destination - transform.position, m_Direction) < 0){
			SpawnExplosion();
		}
		else{
			transform.position += m_Direction * m_Speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter(){
		SpawnExplosion();
	}
	void SpawnExplosion(){
		UberPool.SharedInstance.GetObject(ObjectType.Explosion, transform.position, Quaternion.LookRotation(Vector3.up));
		gameObject.SetActive(false);
	}
	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		TrailRenderer tr = transform.GetComponent<TrailRenderer>();
		tr.Clear();
	}
}
