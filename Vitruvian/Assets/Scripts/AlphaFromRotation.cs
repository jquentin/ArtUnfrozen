using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AlphaFromRotation : MonoBehaviour {

	public float rotation1;
	public float rotation2;

	public SpriteRenderer sprite1;
	public SpriteRenderer sprite2;

//	void Update () 
//	{
//		float currentRotation = ((transform.eulerAngles.z + 180f) % 360f) - 180f;
//		float alpha1 = Mathf.InverseLerp(rotation2, rotation1, currentRotation);
//		float alpha2 = Mathf.InverseLerp(rotation1, rotation2, currentRotation);
//		sprite1.color = new Color(1f, 1f, 1f, alpha1);
//		sprite2.color = new Color(1f, 1f, 1f, alpha2);
//	}

	public void SetRotation(float relative)
	{
		transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(rotation1, rotation2, relative));
		sprite1.color = new Color(1f, 1f, 1f, 1f - relative);
		sprite2.color = new Color(1f, 1f, 1f, relative);
	}

}
