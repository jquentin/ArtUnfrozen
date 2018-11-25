using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

	public List<Eye> eyes;

	IEnumerator Start ()
	{
		while (true)
		{
			yield return new WaitForSeconds (Random.Range (3f, 8f));
			Blink ();
		}
	}

	void Blink ()
	{
		foreach (Eye eye in eyes)
			StartCoroutine (eye.Blink ());
	}

}
