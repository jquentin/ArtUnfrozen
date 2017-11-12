using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnAreaCollider2D : SpawnAreaMonoBehaviour {

	public bool useExternalCollider = false;

	public Collider2D externalCollider;

	Collider2D _collider2D;
	protected Collider2D collider2D
	{
		get
		{
			if (_collider2D == null)
			{
				if (useExternalCollider)
					_collider2D = externalCollider;
				else
					_collider2D = GetComponent<Collider2D>();
			}
			return _collider2D;
		}
	}


	public List<Collider2D> excludedAreas = new List<Collider2D>();

}
