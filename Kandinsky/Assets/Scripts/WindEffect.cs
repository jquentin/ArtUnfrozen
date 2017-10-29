using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour {

	public float windForce = 1f;

	Vector2 offset;

	Vector2 lastPos;

	Rigidbody2D _rb;
	Rigidbody2D rb
	{
		get
		{
			if (_rb == null)
				_rb = GetComponent<Rigidbody2D>();
			return _rb;
		}
	}

	void OnMouseEnter()
	{
		offset = lastPos = Input.mousePosition;
	}

	void OnMouseOver()
	{
		Vector2 move = (Vector2)Input.mousePosition - lastPos;
		Vector2 speed = move / Time.deltaTime;

//		Vector2 speed2 = speed.normalized * ((speed.magnitude + 1) * (speed.magnitude + 1) - 1f);
//		rb.AddForce(speed2 * Time.deltaTime * windForce, ForceMode2D.Force);

		Vector2 targetVelocity = speed * windForce;
		if (targetVelocity.magnitude > rb.velocity.magnitude)
			rb.velocity = targetVelocity;

		lastPos = Input.mousePosition;
	}

	void OnMouseEnd()
	{

	}

	void Update()
	{
	}

}
