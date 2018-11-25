using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInstantiator : MonoBehaviour {

	public GameObject drop;

	float dropTime = 1f;
	float lastDropTime;

	void Drop ()
	{
		GameObject dropInstance = Instantiate (drop, this.transform);
		dropInstance.transform.localPosition = Vector3.zero;
	}

	void Update ()
	{
		dropTime = FindObjectOfType<Heart>().beatrate * 0.3f;
		if (Time.realtimeSinceStartup > lastDropTime + dropTime)
		{
			Drop ();
			lastDropTime = Time.realtimeSinceStartup;
		}
	}
}
