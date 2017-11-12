using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Random spawn area using a collider2d as area.
/// This will pick a random position with a uniform distribution.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class RandomSpawnAreaCollider2D : SpawnAreaCollider2D
{

	bool IsInArea(Vector3 pos, bool local)
	{
		Vector3 worldPos;
		if (local)
			worldPos = transform.TransformPoint(pos);
		else
			worldPos = pos;
		bool isInArea = collider2D.OverlapPoint(worldPos);
		if (isInArea)
		{
			// If is also in an excludedArea, return false
			foreach(Collider2D excludeArea in excludedAreas)
				if (excludeArea.OverlapPoint(worldPos))
					return false;
			return true;
		}
		else
		{
			return false;
		}
	}

	public override Vector3 PickPosition(bool local = false)
	{
		// Get the appropriate bounding rect values, local or not
		Bounds bounds = collider2D.bounds;
		if (local)
		{
			bounds.min = transform.InverseTransformPoint(bounds.min);
			bounds.max = transform.InverseTransformPoint(bounds.max);
		}
		Vector2 pos = MathUtils.NaNVector2;
		// Pick a position in the bounding rect, check if it's in the area
		// If it's not, keep trying until it is
		while (pos.IsNaN())
		{
			Vector2 posAttempt = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
			if (IsInArea(posAttempt, local))
				pos = posAttempt;
		}
		return pos;
	}

	public override List<Vector3> PickPositions(int number, float minDistance, bool local = false)
	{
		List<Vector3> res = new List<Vector3>();
		int maxAttempts = 100;
		for (int i = 0 ; i < number ; i++)
		{
			Vector3 pos = MathUtils.NaNVector3;
			int nbAttempts = 0;
			while (pos.IsNaN())
			{
				float actualMinDistance = minDistance * (1f - Mathf.InverseLerp(0, maxAttempts, nbAttempts));
				Vector3 posTry = PickPosition(local);
				bool overlapOther = false;
				foreach(Vector3 target in res)
				{
					if ((target - (Vector3) posTry).magnitude < actualMinDistance)
					{
						overlapOther = true;
					}
				}
				if (!overlapOther)
					pos = posTry;
				nbAttempts++;
				if (nbAttempts > maxAttempts)
				{
					Debug.LogErrorFormat("Impossible to fit {0} toppings in area: {1}", number, name);
					break;
				}
			}
			res.Add(pos);
		}

		return res;
	}

	[ContextMenu("Test Spawn: World Position")]
	public void TestSpawnWorld()
	{
		GameObject go = new GameObject("TestSpawnWorld");
		go.transform.position = PickPosition(false);
		go.transform.parent = transform;
	}


	[ContextMenu("Test Spawn: Local Position")]
	public void TestSpawnLocal()
	{
		GameObject go = new GameObject("TestSpawnLocal");
		go.transform.parent = transform;
		go.transform.localPosition = PickPosition(true);
	}

}

