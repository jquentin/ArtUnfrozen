using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Heart : MonoBehaviour {

	[NonSerialized]
	public float beatrate = 1f;

	public float minBeatrate = 0.5f;
	public float maxBeatrate = 2f;

	public AudioClip beatUp;
	public AudioClip beatDown;

	public float distMultiplier = 1f;

	public List<Transform> hearts;
	public List<Transform> rightEyeBrows;
	public List<Transform> leftEyeBrows;

	bool eyeBrowsUp = false;

	float lastBeatTime;

	AudioSource _source = null;
	AudioSource source
	{
		get
		{
			if (_source == null)
				_source = GetComponent <AudioSource> ();
			return _source;
		}
	}

//	IEnumerator Start () 
//	{
//		while (true)
//		{
//			yield return new WaitForSeconds (beatrate * 0.7f);
//			source.PlayOneShot (beatUp);
//			hearts.ForEach(t => t.DOPunchScale (Vector3.one * 0.1f, beatrate * 0.4f, 3, 0f));
//			yield return new WaitForSeconds (beatrate * 0.3f);
//			source.PlayOneShot (beatDown);
//		}
//	}

	IEnumerator OneBeat () 
	{
		source.PlayOneShot (beatUp);
		hearts.ForEach(t => t.DOPunchScale (Vector3.one * 0.1f, beatrate * 0.4f, 3, 0f));
		yield return new WaitForSeconds (beatrate * 0.3f);
		source.PlayOneShot (beatDown);
	}

	void Update () 
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float distance = Mathf.Min (hearts.Select (h => ((Vector2)(h.position - mousePos)).magnitude).ToArray ());
		beatrate = Mathf.Max (minBeatrate, Mathf.Min (maxBeatrate, distance * distMultiplier));

		if (Input.GetMouseButton (0))
		{
			Debug.Log ("click");
		}
		if (hearts.Any (h => h.GetComponent<BoxCollider2D>().OverlapPoint (mousePos)) && Input.GetMouseButton (0))
		{
			Debug.Log ("clicked");
			beatrate = 60f;
			float frownAngle = 15f;
			rightEyeBrows.ForEach (t => t.DOLocalRotate (new Vector3(0f, 0f, frownAngle), 0.15f).SetLoops(-1, LoopType.Yoyo));
			leftEyeBrows.ForEach (t => t.DOLocalRotate (new Vector3(0f, 0f, -frownAngle), 0.15f).SetLoops(-1, LoopType.Yoyo));
			hearts.ForEach(t => t.localScale = Vector3.one * 1.1f);
			eyeBrowsUp = true;
		}
		else if (eyeBrowsUp)
		{
			Debug.Log ("unclicked");
			rightEyeBrows.ForEach (t => {t.DOKill();  t.DOLocalRotate (new Vector3(0f, 0f, 0f), 0.15f);});
			leftEyeBrows.ForEach (t => {t.DOKill();  t.DOLocalRotate (new Vector3(0f, 0f, 0f), 0.15f);});
			hearts.ForEach(t => t.localScale = Vector3.one * 1f);
			eyeBrowsUp = false;
		}
		if (Time.realtimeSinceStartup > lastBeatTime + beatrate)
		{
			StartCoroutine (OneBeat ());
			lastBeatTime = Time.realtimeSinceStartup;
		}

	}
}
