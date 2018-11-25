using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour {

	public List<AudioClip> screamSounds;

	public List<AudioClip> roarSounds;

	public Rigidbody body;

	float lastRoarTime;

	float lastScreamTime;

	AudioSource _source = null;
	AudioSource source
	{
		get
		{
			if (_source == null)
				_source = GetComponent<AudioSource> ();
			if (_source == null)
				_source = gameObject.AddComponent <AudioSource>();
			return _source;
		}
	}

	void Update ()
	{
		if (body.velocity.magnitude > 0.2f && Time.realtimeSinceStartup - lastRoarTime > 4f)
		{
			source.PlayOneShot (roarSounds[Random.Range (0, roarSounds.Count)]);
			lastRoarTime = Time.realtimeSinceStartup;
		}
	}

	void HardHit ()
	{
		if (Time.realtimeSinceStartup - lastScreamTime > 5f)
		{
			source.PlayOneShot (screamSounds[Random.Range (0, screamSounds.Count)]);
			lastScreamTime = Time.realtimeSinceStartup;
		}
	}
}
