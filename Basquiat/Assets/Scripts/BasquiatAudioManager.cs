using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasquiatAudioManager : MonoBehaviour {

	public List<AudioClip> mumbles;
	public AudioClip grunt_start;
	public AudioClip grunt_loop;
	public AudioClip grunt_end;

	bool isHairBeingEffected = false;

	float lastTimeHairEffected;

	AudioSource _source;
	AudioSource source
	{
		get
		{
			if (_source == null)
				_source = GetComponent<AudioSource>();
			return _source;
		}
	}

	void Awake () 
	{
		Hair.OnHairEffected += HairEffected;
		Eye.OnEyeMoved += EyeMoved;
	}

	void OnDestroy ()
	{
		Hair.OnHairEffected -= HairEffected;
		Eye.OnEyeMoved -= EyeMoved;
	}

	void EyeMoved ()
	{
		if (!source.isPlaying)
			source.PlayOneShot (mumbles[Random.Range (0, mumbles.Count)]);
	}

	void HairEffected ()
	{
		lastTimeHairEffected = Time.realtimeSinceStartup;
		isHairBeingEffected = true;
		if (!source.isPlaying || source.clip != grunt_start && source.clip != grunt_loop && source.clip != grunt_end)
		{
			source.clip = grunt_start;
			source.Play ();
			Invoke ("PlayGruntLoop", grunt_start.length);
		}
	}

	void PlayGruntLoop ()
	{
		source.clip = grunt_loop;
		source.loop = true;
		source.Play ();
	}

	void Update ()
	{
		if (Time.realtimeSinceStartup - lastTimeHairEffected > 0.5f && source.clip == grunt_loop && source.isPlaying)
		{
			source.clip = grunt_end;
			source.loop = false;
			source.Play ();
		}
	}

}
