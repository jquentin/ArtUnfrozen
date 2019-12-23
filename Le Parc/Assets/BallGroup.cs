using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class BallGroup : MonoBehaviour
{

	public const float DIST_BETWEEN_BALLS = 0.58f;

	[SerializeField] float delayBeforeTurn = 0.2f;

	[SerializeField] float speed = 1f;

	int i;

	float lastClick = float.MaxValue;

	void Start()
	{
		foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
		{
			trail.enabled = false;
		}

	}

	void Update()
	{
		transform.position += new Vector3(0f, Time.deltaTime * speed, -0.01f);
		//foreach(TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
		//{
		//	Instantiate(trail.gameObject, trail.transform.position, trail.transform.rotation);
		//}
		List<TrailRenderer> orderedTrails = GetComponentsInChildren<TrailRenderer>().OrderByDescending(r => r.transform.position.y).ToList();
		for (int i = 0; i < orderedTrails.Count; i++)
		{
			//orderedTrails[i].sortingOrder = i;
			Vector3 pos = orderedTrails[i].transform.localPosition;
			pos.z = -i * 0.0001f;
			orderedTrails[i].transform.localPosition = pos;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetMouseButtonDown(0))
		{
			//Turn(true);
			i++;
			lastClick = Time.time;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetMouseButtonDown(1))
		{
			//Turn(false);
			i--;
			lastClick = Time.time;
		}
		if (Time.time > lastClick + delayBeforeTurn)
		{
			Turn();
			lastClick = float.MaxValue;
		}
	}

	void Turn()
	{
		transform.DOLocalRotate(((i % 8) + 8) * Vector3.fwd * 45f, 2f, RotateMode.Fast).SetEase(Ease.InOutQuad);
	}

	void Turn(bool left)
	{
		i = i + (left ? 1 : -1);
		transform.DORotate(i * Vector3.fwd * 45f, 2f, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad);
	}

}
