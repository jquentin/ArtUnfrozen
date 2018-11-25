using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hair : MonoBehaviour {

	const float tweenTime = 0.6f;

	public static System.Action OnHairEffected;

	void Start ()
	{
		Idle ();
	}

	void Idle ()
	{
		transform.DOKill ();
		transform.DOLocalRotate (Vector3.back * 5f, 3f * Random.Range (0.9f,1.1f)).SetEase (Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}

	public void ApplyWind(Vector2 speed)
	{
		if (speed.magnitude > 0.1f)
		{
			transform.DOKill ();
			transform.DOLocalRotate (Vector3.back * speed.x, tweenTime).SetEase (Ease.OutQuad);
			OnHairEffected ();
			Invoke ("SetBack", tweenTime + 0.1f);
		}
	}

	void SetBack ()
	{
		transform.DOKill ();
		transform.DOLocalRotate (Vector3.zero, tweenTime * 2f).SetEase (Ease.InOutQuad).OnComplete (Idle);
	}
}
