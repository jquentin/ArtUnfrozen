using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour {

	public ParticleSystem particleSystem;

	public float maxWindForce = 0.2f;

	public float maxWindForceMove = 200f;

	float xOffset;

	void OnMouseEnter()
	{
		xOffset = Input.mousePosition.x;
	}

	void OnMouseOver()
	{
		float move = Input.mousePosition.x - xOffset;
		var vel = particleSystem.velocityOverLifetime;
		vel.x = Mathf.Lerp(-maxWindForce, maxWindForce, Mathf.InverseLerp(-maxWindForceMove, maxWindForceMove, move));
//		particleSystem.velocityOverLifetime = vel;
		var main = particleSystem.main;
		main.simulationSpeed = 2f;
	}

	void OnMouseEnd()
	{

	}

	void Update()
	{
		var vel = particleSystem.velocityOverLifetime;
		vel.x = Mathf.Lerp(vel.x.constant, 0f, Time.deltaTime);

		var main = particleSystem.main;
		main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, 1f, Time.deltaTime);;
	}

}
