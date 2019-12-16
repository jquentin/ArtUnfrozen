using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class BallGroup : MonoBehaviour
{
	[SerializeField] float speed = 1f;

	void Update()
	{
		transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
		List<TrailRenderer> orderedTrails = GetComponentsInChildren<TrailRenderer>().OrderByDescending(r => r.transform.position.y).ToList();
		for (int i = 0; i < orderedTrails.Count; i++)
		{
			orderedTrails[i].sortingOrder = i;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Turn(true);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Turn(false);
		}
	}

	void Turn(bool left)
	{
		transform.DORotate((left ? 1f : -1f) * Vector3.fwd * 45f, 0.5f).SetRelative(true);
	}
}
