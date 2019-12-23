using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLine : MonoBehaviour
{

	[SerializeField] Ball ballPrefab;

	[SerializeField] int ballCount = 1;

	[SerializeField] Color ballColor = Color.white;

	[SerializeField] Color trailColor = Color.white;

	//void Start()
	//{
	//	Init();
	//}

	[ContextMenu("Init")]
	void Init()
	{
		for (int i = transform.childCount - 1; i >= 0 ; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
		for (int i = 0; i < ballCount; i++)
		{
			Ball ball = UnityEditor.PrefabUtility.InstantiatePrefab(ballPrefab, this.transform) as Ball;
			ball.transform.localPosition = new Vector3(BallGroup.DIST_BETWEEN_BALLS * (i - (ballCount - 1) * 0.5f), 0f);
			ball.Init(ballColor, trailColor);
		}

		//foreach (var ball in GetComponentsInChildren<Ball>())
		//{
		//	ball.Init(ballColor, trailColor);
		//}
	}

}
