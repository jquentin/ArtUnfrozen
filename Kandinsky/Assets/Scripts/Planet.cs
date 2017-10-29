using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	Rigidbody2D _rigidbody;
	Rigidbody2D rigidbody
	{
		get
		{
			if (_rigidbody == null)
				_rigidbody = GetComponent<Rigidbody2D>();
			return _rigidbody;
		}
	}

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

	void Update()
	{
//		rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode2D.Force);
	}

}
