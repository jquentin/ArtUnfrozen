using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class IntMinMaxRangeAttribute : PropertyAttribute
{
	public int minLimit, maxLimit;

	public IntMinMaxRangeAttribute( int minLimit, int maxLimit )
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}
}

[System.Serializable]
public class IntMinMaxRange : IIntegerPickableSet
{
	public int rangeStart, rangeEnd;

	public int rangeSize { get { return rangeEnd - rangeStart + 1; } }

	public IntMinMaxRange(){}

	public IntMinMaxRange(int rangeStart, int rangeEnd)
	{
		this.rangeStart = rangeStart;
		this.rangeEnd = rangeEnd;
	}

	/// <summary>
	/// Gets a random value between rangeStart (including) and rangeEnd (INCLUDING!).
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValue()
	{
		return Random.Range( rangeStart, rangeEnd + 1 );
	}

	/// <summary>
	/// Gets nbValues random values between rangeStart (including) and rangeEnd (INCLUDING!).
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public List<int> GetRandomValues(int nbValues)
	{
		return MathUtils.RangeList(rangeStart, rangeEnd).PickRandomElements(nbValues);
	}

	/// <summary>
	/// Gets a random value between rangeStart (including) and rangeEnd (INCLUDING!), 
	/// excluding the list of values in parameter.
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueExcluding(List<int> excluding)
	{
		return RandomUtils.RandomValueInListExcluding(MathUtils.RangeList(rangeStart, rangeEnd) , excluding);
	}

	public int GetRandomValueExcluding(params int[] excluding)
	{
		return GetRandomValueExcluding(excluding.ToList());
	}

	/// <summary>
	/// Gets a random value, greater or equals to min, between rangeStart (including) and rangeEnd (INCLUDING).
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMin(int min)
	{
		if (min > rangeEnd)
			Debug.LogWarningFormat("The min value: {0} is more than the range [{1}-{2}]. Returning min.", min, rangeStart, rangeEnd);
		int start = Mathf.Max(min, rangeStart);
		int end = Mathf.Max(min, rangeEnd) + 1;
		return Random.Range( start, end );
	}

	/// <summary>
	/// Gets a random value, greater or equals to min, between rangeStart (including) and rangeEnd (INCLUDING), 
	/// excluding the list of values in parameter.
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMinExcluding(int min, List<int> excluding)
	{
		if (min > rangeEnd)
			Debug.LogWarningFormat("The min value: {0} is more than the range [{1}-{2}]. Returning min.", min, rangeStart, rangeEnd);
		int start = Mathf.Max(min, rangeStart);
		int end = Mathf.Max(min, rangeEnd);
		return RandomUtils.RandomValueInListExcluding(MathUtils.RangeList(start, end), excluding);
	}

	public int GetRandomValueWithMinExcluding(int min, params int[] excluding)
	{
		return GetRandomValueWithMinExcluding(min, excluding.ToList());
	}

	/// <summary>
	/// Gets a random value, less or equals to max, between rangeStart (including) and rangeEnd (INCLUDING).
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMax(int max)
	{
		if (max < rangeStart)
			Debug.LogWarningFormat("The max value: {0} is less than the range [{1}-{2}]. Returning max.", max, rangeStart, rangeEnd);
		int start = Mathf.Min(max, rangeStart);
		int end = Mathf.Min(max, rangeEnd) + 1;
		return Random.Range( start, end );
	}

	/// <summary>
	/// Gets a random value, less or equals to max, between rangeStart (including) and rangeEnd (INCLUDING), 
	/// excluding the list of values in parameter.
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMaxExcluding(int max, List<int> excluding)
	{
		if (max < rangeStart)
			Debug.LogWarningFormat("The max value: {0} is less than the range [{1}-{2}]. Returning max.", max, rangeStart, rangeEnd);
		int start = Mathf.Min(max, rangeStart);
		int end = Mathf.Min(max, rangeEnd);
		return RandomUtils.RandomValueInListExcluding(MathUtils.RangeList(start, end), excluding);
	}

	public int GetRandomValueWithMaxExcluding(int max, params int[] excluding)
	{
		return GetRandomValueWithMaxExcluding(max, excluding.ToList());
	}

	/// <summary>
	/// Gets a random value, between min and max(including), and between rangeStart (including) and rangeEnd (INCLUDING).
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMinMax(int min, int max)
	{
		if (min > rangeEnd)
			Debug.LogWarningFormat("The min value: {0} is more than the range [{1}-{2}]. Returning min.", min, rangeStart, rangeEnd);
		if (max < rangeStart)
			Debug.LogWarningFormat("The max value: {0} is less than the range [{1}-{2}]. Returning max.", max, rangeStart, rangeEnd);
		int start = Mathf.Min( max, Mathf.Max(min, rangeStart));
		int end = Mathf.Min( max, Mathf.Max(min, rangeEnd)) + 1;
			return Random.Range( start, end );
	}

	/// <summary>
	/// Gets a random value, between min and max(including), and between rangeStart (including) and rangeEnd (INCLUDING), 
	/// excluding the list of values in parameter.
	/// This is different from Random.Range(int, int) which is excluding rangeEnd.
	/// </summary>
	public int GetRandomValueWithMinMaxExcluding(int min, int max, List<int> excluding)
	{
		if (min > rangeEnd)
			Debug.LogWarningFormat("The min value: {0} is more than the range [{1}-{2}]. Returning min.", min, rangeStart, rangeEnd);
		if (max < rangeStart)
			Debug.LogWarningFormat("The max value: {0} is less than the range [{1}-{2}]. Returning max.", max, rangeStart, rangeEnd);
		int start = Mathf.Min( max, Mathf.Max(min, rangeStart));
		int end = Mathf.Min( max, Mathf.Max(min, rangeEnd));
		return RandomUtils.RandomValueInListExcluding(MathUtils.RangeList(start, end), excluding);
	}

	public int GetRandomValueWithMinMaxExcluding(int min, int max, params int[] excluding)
	{
		return GetRandomValueWithMinMaxExcluding(min, max, excluding.ToList());
	}

	public List<int> GetAllPossibleValues()
	{
		return MathUtils.RangeList(rangeStart, rangeEnd);
	}

	public bool IsInRange(int number)
	{
		return number >= rangeStart && number <= rangeEnd;
	}

}
