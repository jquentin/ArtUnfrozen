using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[ContextMenu("InitOrderInLayer")]
	public void InitOrderInLayer () 
	{
		int orderIndex = 0;
		IEnumerable<Planet> planets = FindObjectsOfType<Planet>().OrderByDescending((Planet arg) => arg.size);
		foreach(Planet p in planets)
		{
			p.orderInLayer = orderIndex;
			orderIndex += 10;
		}
	}

}
