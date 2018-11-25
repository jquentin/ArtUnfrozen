using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BodyAngle : MonoBehaviour {

	[Range(0f, 1f)]
	public float angle;

	public List<AlphaFromRotation> members;

	public float speed = 1f;

	public AudioClip upSound;
	public AudioClip downSound;

	AudioSource _source = null;
	AudioSource source
	{
		get
		{
			if (_source == null)
				_source = GetComponent<AudioSource>();
			return _source;
		}
	}
	
	void Update () 
	{
		foreach(AlphaFromRotation member in members)
		{
			member.SetRotation(angle);
		}
		if (Application.isPlaying)
		{
			if (Input.GetMouseButton(0))
			{
				angle = angle + Time.deltaTime * speed;
			}
			else
			{
				angle = angle - Time.deltaTime * speed;
			}
			angle = Mathf.Clamp01(angle);
			if (Input.GetMouseButtonDown(0))
			{
				source.Stop();
				source.clip = upSound;
				source.Play();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				source.Stop();
				source.clip = downSound;
				source.Play();
			}

		}
	}
}
