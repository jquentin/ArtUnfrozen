using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jaw : MonoBehaviour {

	float animTime = 0.5f;

	bool isPlaying = false;

	public List<AudioClip> squeakOpen;
	public List<AudioClip> squeakClose;

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

	void OnMouseOver () 
	{
		if (!isPlaying)
		{
			isPlaying = true;
			Open ();
		}
	}
	void OnMouseExit () 
	{
		if (isPlaying)
		{
			StartCoroutine (Close ());
		}
	}

	void Open ()
	{
		transform.DOLocalRotate (Vector3.forward * 10f, animTime).SetEase (Ease.OutQuad);
		source.Stop ();
		source.clip = (squeakOpen[Random.Range (0, squeakOpen.Count)]);
		source.Play ();
	}

	IEnumerator Close ()
	{
		transform.DOKill ();
		transform.DOLocalRotate (Vector3.zero, animTime * 2f).SetEase (Ease.OutBounce);
		source.Stop ();
		source.clip = (squeakClose[Random.Range (0, squeakClose.Count)]);
		source.Play ();
		yield return new WaitForSeconds (animTime * 2f + 0.1f);
		isPlaying = false;
	}

}
