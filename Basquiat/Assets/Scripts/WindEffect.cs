using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour {

	public float windForce = 1f;

	Vector2 lastPos = new Vector2(float.NaN, float.NaN);

	void Update()
	{

		Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (!float.IsNaN(lastPos.x))
		{
			Vector2 move = currentPos - lastPos;
			Vector2 speed = move / Time.deltaTime;

			Collider2D[] col = Physics2D.OverlapPointAll(currentPos);

			if(col.Length > 0){
				foreach(Collider2D c in col)
				{
					Hair hair = c.GetComponent<Hair>();
					if (hair != null)
					{
						hair.ApplyWind(speed * windForce);
					}
				}
			}
		}

		lastPos = currentPos;

	}

}
