using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	[SerializeField] SpriteRenderer ball;

	[SerializeField] ParticleSystem particles;

	public void Init(Color ballColor, Color trailColor)
	{
		ball.color = ballColor;
		particles.startColor = trailColor;
	}

}
