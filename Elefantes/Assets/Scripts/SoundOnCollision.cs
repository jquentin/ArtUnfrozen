using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

	public List<AudioClip> hitSounds;

	public float maxVolume = 1f;

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

	void OnCollisionEnter (Collision collision)
	{
		source.PlayOneShot (hitSounds[Random.Range (0, hitSounds.Count)], Mathf.Min (collision.impulse.magnitude, maxVolume));

		if (collision.impulse.magnitude > 1f)
			SendMessageUpwards ("HardHit");
	}
}
