using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TTDUtils
{

	public static float PIXELS_TO_MILLIMETERS
	{
		get
		{
			return 25.4f / Screen.dpi;
		}
	}
	public static float MILLIMETERS_TO_PIXELS
	{
		get
		{
			return Screen.dpi / 25.4f;
		}
	}


}
