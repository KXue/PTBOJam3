using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedJumpScript : MonoBehaviour {
	private bool m_ShouldJump = false;
	private float m_JumpTime = 0;
	private Animator m_Animator;
	// Use this for initialization
	void Start () {
		m_Animator = GetComponentInChildren(typeof(Animator)) as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_ShouldJump && Time.time - m_JumpTime > 0){
			m_Animator.SetTrigger("jump");
			m_ShouldJump = false;
		}
	}

	public void JumpAfterDelay(float delay){
		m_ShouldJump = true;
		m_JumpTime = Time.time + delay;
	}
}
