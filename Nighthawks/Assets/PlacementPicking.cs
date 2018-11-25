using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPicking : MonoBehaviour {

	public List<GameObject> placements;

	public SpriteRenderer darkOverlay;

	int indexPlacement = 0;

	bool lightsOn;

	Queue<int> lastPlacements = new Queue<int>();

	public List<AudioClip> switchSounds;

	public AudioClip flickerSound;

	public AudioClip lightSound;

	AudioSource loopSource;
	AudioSource shotSource;

	int countdownBeforeNextFlicker;

	void Start()
	{
		countdownBeforeNextFlicker = Random.Range (2,4);
		loopSource = GetComponent<AudioSource>();
		shotSource = gameObject.AddComponent<AudioSource>();
		shotSource.volume = 0.1f;
		PickPlacement(0);
		LightOn();
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (lightsOn)
				TurnLightOff();
			else
			{
				PickNewPlacement();
				TurnLightOn();
			}
		}
	}

	void TurnLightOff()
	{
		LightOff ();
		shotSource.PlayOneShot (switchSounds[Random.Range (0, switchSounds.Count)]);
	}

	void TurnLightOn()
	{
		LightOn ();
		shotSource.PlayOneShot (switchSounds[Random.Range (0, switchSounds.Count)]);
		countdownBeforeNextFlicker--;
		if (countdownBeforeNextFlicker <= 0)
		{
			StartCoroutine (Flicker ());
			countdownBeforeNextFlicker = Random.Range (5,10);
		}
	}

	void LightOff ()
	{
		darkOverlay.color = new Color(1f, 1f, 1f, 0.98f);
		lightsOn = false;
		loopSource.Stop ();
	}

	void LightOn ()
	{
		darkOverlay.color = Color.clear;
		lightsOn = true;
		loopSource.Play ();
	}

	IEnumerator Flicker()
	{
		float flickerTime = 0.02f;
		shotSource.PlayOneShot (flickerSound);
		yield return new WaitForSeconds (flickerTime * 3f);
		LightOff ();
		yield return new WaitForSeconds (0.3f);
		LightOn ();
		yield return new WaitForSeconds (flickerTime);
		LightOff ();
		yield return new WaitForSeconds (flickerTime);
		LightOn ();
		yield return new WaitForSeconds (flickerTime);
		LightOff ();
		yield return new WaitForSeconds (flickerTime);
		LightOn ();
		yield return new WaitForSeconds (flickerTime);
		LightOff ();
		yield return new WaitForSeconds (flickerTime);
		LightOn ();
		yield return new WaitForSeconds (flickerTime);
		LightOff ();
		yield return new WaitForSeconds (flickerTime);
		LightOn ();
		yield return new WaitForSeconds (flickerTime);
		LightOff ();
		yield return new WaitForSeconds (flickerTime);
		LightOn ();
	}

	void PickNewPlacement()
	{
		IEnumerable<int> choices = placements.Select ((p, i) => i).Where (i => i != indexPlacement).OrderBy (p => Random.Range(0f, 1f));
		PickPlacement(choices.ToList()[0]);
	}

	void Shuffle (IEnumerable<GameObject> objects)
	{
		
	}

	void PickPlacement(int index)
	{
		foreach(GameObject placement in placements)
			placement.SetActive(placement == placements[index]);
		indexPlacement = index;
		lastPlacements.Enqueue (index);
		if (lastPlacements.Count > 3)
			lastPlacements.Dequeue ();
	}
}
