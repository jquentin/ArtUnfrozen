using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatMinMaxRangeAttribute : PropertyAttribute
{
	public float minLimit, maxLimit;

	public FloatMinMaxRangeAttribute( float minLimit, float maxLimit )
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}
}

[System.Serializable]
public class FloatMinMaxRange
{
	public float rangeStart, rangeEnd;

	public FloatMinMaxRange(){}

	public FloatMinMaxRange(float rangeStart, float rangeEnd)
	{
		this.rangeStart = rangeStart;
		this.rangeEnd = rangeEnd;
	}

	public float GetRandomValue()
	{
		return Random.Range( rangeStart, rangeEnd );
	}

}
