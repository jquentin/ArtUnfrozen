using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntegerPickableSet
{

	int GetRandomValue();

	List<int> GetRandomValues(int nbValues);

	int GetRandomValueExcluding(List<int> excluding);

	int GetRandomValueExcluding(params int[] excluding);

	int GetRandomValueWithMin(int min);

	int GetRandomValueWithMinExcluding(int min, List<int> excluding);

	int GetRandomValueWithMinExcluding(int min, params int[] excluding);

	int GetRandomValueWithMax(int max);

	int GetRandomValueWithMaxExcluding(int max, List<int> excluding);

	int GetRandomValueWithMaxExcluding(int max, params int[] excluding);

	int GetRandomValueWithMinMax(int min, int max);

	int GetRandomValueWithMinMaxExcluding(int min, int max, List<int> excluding);

	int GetRandomValueWithMinMaxExcluding(int min, int max, params int[] excluding);

	List<int> GetAllPossibleValues();

	bool IsInRange(int number);

}
