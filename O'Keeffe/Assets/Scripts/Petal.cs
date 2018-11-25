using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Petal : MonoBehaviour {

	public float move = 0.1f;
	public float time = 1f;

	bool alreadyPlayed = false;

	Vector3 initPos;

	Transform _renderer;

	void Start ()
	{
		_renderer = new GameObject("Renderer").transform;
		_renderer.parent = this.transform;
		_renderer.localPosition = Vector3.zero;
		_renderer.gameObject.AddComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
		Destroy (GetComponent<SpriteRenderer>());
		initPos = transform.localPosition;
	}

	void Update () 
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPos.z = 0f;
		if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPos))
		{
			if (!alreadyPlayed)
			{
				_renderer.DOKill(true);
				_renderer.DOPunchPosition (Vector3.right * move, time, 6, 1, false);
				GetComponent<AudioSource>().Play();
				alreadyPlayed = true;
			}
		}
		else
		{
			alreadyPlayed = false;
		}
	}
}
