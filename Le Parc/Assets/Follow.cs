using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

	[SerializeField] Transform target;

	Vector3 offset;

	void Awake()
	{
		offset = transform.position - target.position;
	}

	void Update()
	{
		transform.position = target.position + offset;
	}
}
