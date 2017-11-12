using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeadPivot : MonoBehaviour {

	public float min;
	public float max;

	public float time = 1f;

	public float headDragAngleMultiplier = 0.1f;

	bool rotatingUp = false;

	float headAngleOffset;

	float inputAngleOffset;

	public Vector3 axis = Vector3.forward;

	void Start () 
	{
//		transform.eulerAngles = axis * min;
		RestartRotating();
	}

	void StartRotating()
	{
		transform.DORotate(axis * max, time, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
	void RestartRotating()
	{
		float currentHeadAngle = GetAngle180(transform.eulerAngles.z);
		if (currentHeadAngle > 0.5f * (min + max))
		{
			float timeFirst = Mathf.InverseLerp(min, max, currentHeadAngle) * time * 0.4f;
			transform.DORotate(axis * min, timeFirst, RotateMode.LocalAxisAdd).OnComplete(() => 
				transform.DORotate(axis * max, time, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
		}
		else
		{
			float timeFirst = Mathf.InverseLerp(max, min, currentHeadAngle) * time * 0.4f;
			transform.DORotate(axis * max, timeFirst, RotateMode.LocalAxisAdd).OnComplete(() => 
				transform.DORotate(axis * min, time, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
		}
	}
	void StopRotating()
	{
		transform.DOKill();
	}

	void OnMouseEnter()
	{
		StopRotating();
		headAngleOffset = GetAngle180(transform.eulerAngles.z);
		Vector2 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		inputAngleOffset = GetAngle180(MathUtils.SignedAngle(dif));
	}

	void OnMouseOver()
	{
		Vector2 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float inputAngle = GetAngle180(MathUtils.SignedAngle(dif));
		float targetHeadAngle = headAngleOffset + headDragAngleMultiplier * (inputAngle - inputAngleOffset);
		Debug.LogFormat("inputAngleOffset={0} ; headAngleOffset={1} ; inputAngle={2} ; targetHeadAngle={3}", inputAngleOffset, headAngleOffset, inputAngle, targetHeadAngle);
		targetHeadAngle = Mathf.Clamp(targetHeadAngle, min, max);
		transform.eulerAngles = Vector3.forward * targetHeadAngle;
	}

	void OnMouseExit()
	{
		RestartRotating();
	}

	float GetAngle180(float angle)
	{
		return (angle + 180f) % 360f - 180f;
	}

}
