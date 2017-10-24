using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public float size
	{
		get
		{
			return GetComponentsInChildren<SpriteRenderer>().Min((SpriteRenderer arg) => arg.bounds.size.magnitude);
		}
	}

	public int orderInLayer
	{
		set
		{
			int index = value;
			foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
			{
				if (sr.gameObject != this.gameObject)
				{
					sr.sortingOrder = index;
					index++;
				}
			}
			GetComponent<SpriteRenderer>().sortingOrder = index;
		}
	}

}
