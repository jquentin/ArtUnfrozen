using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for classes that describe an area in which we want to 
/// pick positions, typically to spawn objects in.
/// </summary>
public interface SpawnArea 
{

	Vector3 PickPosition(bool local);

	List<Vector3> PickPositions(int number, float minDistance, bool local);

}

