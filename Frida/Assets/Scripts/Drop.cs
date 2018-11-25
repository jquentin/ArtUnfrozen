using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

	IEnumerator Start () 
	{
		yield return new WaitForSeconds (0.32f);
		Destroy (gameObject);
	}

}
