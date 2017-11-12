using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerPickableList : IIntegerPickableSet {

	List<int> list;

	public IntegerPickableList(List<int> list)
	{
		this.list = new List<int>(list);
	}

	public int GetRandomValue()
	{
		return list.PickRandomElement();
	}

	public List<int> GetRandomValues(int nbValues)
	{
		return list.PickRandomElements(nbValues);
	}

	public int GetRandomValueExcluding(List<int> excluding)
	{
		return list.PickRandomElements(1, excluding)[0];
	}

	public int GetRandomValueExcluding(params int[] excluding)
	{
		return GetRandomValueExcluding(excluding.ToList());
	}

	List<int> GetValuesUntil(int max)
	{
		return list.FindAll(i => i <= max);
	}
	List<int> GetValuesFrom(int min)
	{
		return list.FindAll(i => i >= min);
	}
	List<int> GetValuesBetween(int min, int max)
	{
		return list.FindAll(i => i >= min && i <= max);
	}

	public int GetRandomValueWithMin(int min)
	{
		return GetValuesFrom(min).PickRandomElement();
	}

	public int GetRandomValueWithMinExcluding(int min, List<int> excluding)
	{
		return GetValuesFrom(min).PickRandomElements(1, excluding)[0];
	}

	public int GetRandomValueWithMinExcluding(int min, params int[] excluding)
	{
		return GetRandomValueWithMinExcluding(min, excluding.ToList());
	}

	public int GetRandomValueWithMax(int max)
	{
		return GetValuesUntil(max).PickRandomElement();
	}

	public int GetRandomValueWithMaxExcluding(int max, List<int> excluding)
	{
		return GetValuesUntil(max).PickRandomElements(1, excluding)[0];
	}

	public int GetRandomValueWithMaxExcluding(int max, params int[] excluding)
	{
		return GetRandomValueWithMaxExcluding(max, excluding.ToList());
	}

	public int GetRandomValueWithMinMax(int min, int max)
	{
		return GetValuesBetween(min, max).PickRandomElement();
	}

	public int GetRandomValueWithMinMaxExcluding(int min, int max, List<int> excluding)
	{
		return GetValuesBetween(min, max).PickRandomElements(1, excluding)[0];
	}

	public int GetRandomValueWithMinMaxExcluding(int min, int max, params int[] excluding)
	{
		return GetRandomValueWithMinMaxExcluding(min, max, excluding.ToList());
	}

	public List<int> GetAllPossibleValues()
	{
		return new List<int>(list);
	}

	public bool IsInRange(int number)
	{
		return list.Contains(number);
	}

}
