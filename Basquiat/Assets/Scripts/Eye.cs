using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

	const float blinkTime = 0.2f;

	public float maxRadius = 1f;

	public float distMultiplier = 1f;

	public float distForGrunt = 1f;

	public SpriteRenderer eyelid;

	Vector3 initPos;

	Vector3 lastGruntPos;

	public static System.Action OnEyeMoved;

	void Awake ()
	{
		initPos = transform.position;
		eyelid.enabled = false;
	}

	public IEnumerator Blink ()
	{
		eyelid.enabled = true;
		yield return new WaitForSeconds (blinkTime);
		eyelid.enabled = false;
	}

	void Update () {
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos.z = 0f;
		if ((mouseWorldPos - lastGruntPos).magnitude > distForGrunt)
		{
			OnEyeMoved ();
			lastGruntPos = mouseWorldPos;
		}
		transform.position = initPos + (mouseWorldPos - initPos).normalized * Mathf.Min (maxRadius, (mouseWorldPos - initPos).magnitude * distMultiplier);
	}
}
