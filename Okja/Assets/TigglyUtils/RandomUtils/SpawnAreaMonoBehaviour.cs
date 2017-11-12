using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class implementing the SpawnArea interface, that is also a MonoBehaviour.
/// Using this class to declare a variable in a script will allow Unity Editor to
/// expose the variable, leaving you free to select an instance of any sub-class.
/// Example:
/// public class Spawner : MonoBehaviour {
/// 	public SpawnAreaMonoBehaviour spawnArea;
/// }
/// The editor will let you drag in any object inheriting SpawnAreaMonoBehaviour,
/// no matter what technical implementation this object uses.
/// </summary>
public abstract class SpawnAreaMonoBehaviour : MonoBehaviour, SpawnArea 
{ 

	public abstract Vector3 PickPosition(bool local);

	public abstract List<Vector3> PickPositions(int number, float minDistance, bool local);

}

