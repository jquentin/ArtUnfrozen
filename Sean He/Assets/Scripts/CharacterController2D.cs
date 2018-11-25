using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	public float speed = 1f;

	public float velocityForFullAnimSpeed = 1f;

	public float mousePosDifThreshold = 1f;

	float lastPosX = float.NaN;

	Rigidbody2D _rigidbody;
	Rigidbody2D rigidbody
	{
		get
		{
			if (_rigidbody == null)
				_rigidbody = GetComponent<Rigidbody2D>();
			return _rigidbody;
		}
	}
	Animator _animator;
	Animator animator
	{
		get
		{
			if (_animator == null)
				_animator = GetComponent<Animator>();
			return _animator;
		}
	}

	void Update () 
	{

		float velocity = 0f;
		if (!float.IsNaN(lastPosX))
			velocity = (transform.position.x - lastPosX) / Time.deltaTime;
		lastPosX = transform.position.x;
//		Debug.Log(Mathf.Abs(this.rigidbody.velocity.x));
		Debug.Log(velocity);
		if (Mathf.Abs(velocity) > 0.1f && 
			((velocity > 0f && transform.localScale.x < 0f) || 
				(velocity < 0f && transform.localScale.x > 0f)))
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		float animSpeed = Mathf.Abs(velocity) / velocityForFullAnimSpeed;
		animator.speed = animSpeed;
		animator.SetFloat("speed", animSpeed);
		if (Input.GetMouseButton(0))
		{
			float difX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this.transform.position.x;
			if (Mathf.Abs(difX) >= mousePosDifThreshold)
			{
				bool goingRight = difX > 0f;
				Vector2 direction = goingRight ? 0.85f * Vector2.right : Vector2.left;
				this.rigidbody.velocity = direction * speed * Time.deltaTime;
			}
		}
	}

	void LateUpdate()
	{
	}
}
