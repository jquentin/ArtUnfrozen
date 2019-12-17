using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class BallGroup : MonoBehaviour
{
	[SerializeField] float speed = 1f;

	int i;

	void Start()
	{
		foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
		{
			trail.enabled = false;
		}

	}

	void Update()
	{
		transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
		//foreach(TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
		//{
		//	Instantiate(trail.gameObject, trail.transform.position, trail.transform.rotation);
		//}
		List<TrailRenderer> orderedTrails = GetComponentsInChildren<TrailRenderer>().OrderByDescending(r => r.transform.position.y).ToList();
		for (int i = 0; i < orderedTrails.Count; i++)
		{
			//orderedTrails[i].sortingOrder = i;
			Vector3 pos = orderedTrails[i].transform.position;
			pos.z = -i * 0.0001f - Time.frameCount * 0.1f;
			orderedTrails[i].transform.position = pos;
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
		i = i + (left ? 1 : -1);
		transform.DORotate(i * Vector3.fwd * 45f, 0.5f);
	}
}
