using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleOnHover : MonoBehaviour {

	public float time = 0.35f;

	public float scale = 1.2f;

	void OnMouseEnter()
	{
		transform.DOKill();
		transform.DOScale(Vector3.one * scale, time);
	}

	void OnMouseExit()
	{
		transform.DOKill();
		transform.DOScale(Vector3.one * 1f, time);
	}

}
