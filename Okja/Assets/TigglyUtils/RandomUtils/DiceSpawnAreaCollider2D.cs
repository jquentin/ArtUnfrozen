using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawnAreaCollider2D : SpawnAreaCollider2D {

	public override Vector3 PickPosition (bool local)
	{
		return PickPositions(1, float.MaxValue, local)[0];
	}

	public override List<Vector3> PickPositions (int number, float minDistance, bool local)
	{
		List<Vector3> res = new List<Vector3>();
		Vector3 center = local ? transform.TransformPoint(collider2D.bounds.center) : collider2D.bounds.center;
		switch(number)
		{
			case 1:
				res.Add(center);
				break;
			case 2:
				res.Add(center - new Vector3(-minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, -minDistance) * 0.5f);
				break;
			case 3:
				res.Add(center - new Vector3(-minDistance, minDistance) * 0.5f);
				res.Add(center);
				res.Add(center - new Vector3(minDistance, -minDistance) * 0.5f);
				break;
			case 4:
				res.Add(center - new Vector3(-minDistance, -minDistance) * 0.5f);
				res.Add(center - new Vector3(-minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, -minDistance) * 0.5f);
				break;
			case 5:
				res.Add(center);
				res.Add(center - new Vector3(-minDistance, -minDistance) * 0.5f);
				res.Add(center - new Vector3(-minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, -minDistance) * 0.5f);
				break;
			case 6:
				res.Add(center - new Vector3(-minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(0f, minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, minDistance) * 0.5f);
				res.Add(center - new Vector3(-minDistance, -minDistance) * 0.5f);
				res.Add(center - new Vector3(0f, -minDistance) * 0.5f);
				res.Add(center - new Vector3(minDistance, -minDistance) * 0.5f);
				break;
		}
		return res;
	}

}
