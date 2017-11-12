using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the picking of several different combinations of values
/// from several IntMinMaxRange objects.
/// 
/// This is useful for cases where you need to pick numbers from ranges, and they don't
/// have to be each different, but the combinations need to be different.
/// 
/// Typical example is picking a suite of equation problems: x + y = z, where 1 <= x <= 3 and 2 <= y <= 4
/// x can be the same several times, and y can be the same several times,
/// but the {x,y} combination can't be the same.
/// 
/// A condition predicate can be set to allow only combinations that respect specific conditions.
/// 
/// This allows to constraint the values in these combinations, including constraints among values in a combination.
/// 
/// Example case is picking a suite of equation problems where the result is in a given range
/// X + Y = Z, where 1 <= X <= 3 and 2 <= Z <= 4
/// Y will be defined by the values picked for X and Z, but still needs to be a strictly positive number.
/// So X needs to be less than Z - 1.
/// We will then accept only combinations of {X,Z} where X is less than Z - 1.
/// 
/// The combinations are returned as List<int> objects, so those list will have a number of elements
/// equal to the number of ranges.
/// 
/// Example 1:
/// 
/// IntMinMaxRange range1 = new IntMinMaxRange(0,1);
/// IntMinMaxRange range2 = new IntMinMaxRange(2,3);
/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2);
/// List<List<int>> allCombs = comb.GetAllCombinations();
/// List<List<int>> pickedCombs = comb.PickCombinations(3);
/// 
/// allCombs will be all combinations of 2 numbers in ranges, respectively [0-1],[2,3]
/// -> {0,2},{0,3},{1,2},{1,3}
/// 
/// pickedCombs will be 3 randomly selected different combinations of those.
/// 
/// Example 2:
/// 
/// IntMinMaxRange range1 = new IntMinMaxRange(0,2);
/// IntMinMaxRange range2 = new IntMinMaxRange(0,2);
/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2);
/// comb.SetCondition((List<int> obj) => obj[0] > obj[1]);
/// List<List<int>> allCombs = comb.GetAllCombinations();
/// 
/// allCombs will be all combinations of 2 numbers in ranges, [0-2],[0-2], 
/// in which the first value is greater than the second.
/// -> {0,1},{0,2},{1,2}
/// 
/// Example 3:
/// 
/// IntMinMaxRange range1 = new IntMinMaxRange(0,1);
/// IntMinMaxRange range2 = new IntMinMaxRange(1,2);
/// IntMinMaxRange range3 = new IntMinMaxRange(2,3);
/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2, range3);
/// comb.SetCondition((List<int> obj) => obj[0] != obj[1] && obj[1] != obj[2] && obj[2] != obj[0]);
/// List<List<int>> allCombs = comb.GetAllCombinations();
/// List<List<int>> pickedCombs = comb.PickCombinations(3);
/// 
/// allCombs will be all combinations of 3 numbers in ranges, respectively [0-1],[1,2],[2,3], in which all
/// elements are different. 
/// -> {0,1,2},{0,1,3},{0,2,3},{1,2,3}
///  
/// pickedCombs will be 3 randomly selected different combinations of those.
/// 
/// </summary>
public class IntMinMaxRangeCombination
{

	List<IntMinMaxRange> ranges;

	/// <summary>
	/// Optional condition applied on each combination independently
	/// Example : 
	/// 
	/// IntMinMaxRange range1 = new IntMinMaxRange(0,2);
	/// IntMinMaxRange range2 = new IntMinMaxRange(0,2);
	/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2);
	/// comb.SetCondition((List<int> obj) => obj[0] > obj[1]);
	/// List<List<int>> allCombs = comb.GetAllCombinations();
	/// 
	/// allCombs will be all combinations of 2 numbers in ranges, [0-2],[0-2], 
	/// in which the first value is greater than the second.
	/// -> {0,1},{0,2},{1,2}
	/// 
	/// </summary>
	Predicate<List<int>> condition;

	/// <summary>
	/// An optional condition between the combinations returned by PickCombinations.
	/// Example:
	/// 
	/// IntMinMaxRange range1 = new IntMinMaxRange(0,2);
	/// IntMinMaxRange range2 = new IntMinMaxRange(0,2);
	/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2);
	/// comb.SetMutualCondition((List<int> arg1, List<int> arg2) => arg1[0] != arg2[0]);
	/// List<List<int>> pickedCombs = comb.PickCombinations(2);
	/// 
	/// pickedCombs can be {{0,1},{1,2}} or {{1,1},{2,0}}
	/// But it cannot be {{0,1},{0,2}} or {{1,2},{1,1}}
	/// 
	/// Note: Because of lack of time, the implementation actually makes it unlikely that it happens, but not impossible.
	/// 
	/// TODO: Rewrite this so that it's reliable
	/// 
	/// </summary>
	Func<List<int>, List<int>, bool> mutualCondition;

	/// <summary>
	/// An optional condition on the combinations returned by PickCombinations, depending on the index at which the element will be in the returned list.
	/// 
	/// Example:
	/// 
	/// IntMinMaxRange range1 = new IntMinMaxRange(0,2);
	/// IntMinMaxRange range2 = new IntMinMaxRange(0,2);
	/// IntMinMaxRangeCombination comb = new IntMinMaxRangeCombination(range1, range2);
	/// comb.SetIndexCondition((int index, List<int> list) => list[0] == index);
	/// List<List<int>> pickedCombs = comb.PickCombinations(2);
	/// 
	/// The range1 picked element must be equal to the index in the returned list of combinations.
	/// So, pickedCombs will always be of the form: {{0,x},{1,y}}
	/// x being in {0-2} and y being in {0-2}
	/// 
	/// </summary>
	Func<int, List<int>, bool> indexCondition;

	public IntMinMaxRangeCombination(params IntMinMaxRange[] ranges)
	{
		this.ranges = ranges.ToList();
	}

	public void SetCondition(Predicate<List<int>> condition)
	{
		this.condition = condition;
	}

	public void SetMutualCondition(Func<List<int>, List<int>, bool> mutualCondition)
	{
		this.mutualCondition = mutualCondition;
	}

	public void SetIndexCondition(Func<int, List<int>, bool> indexCondition)
	{
		this.indexCondition = indexCondition;
	}

	public List<int> PickCombination()
	{
		return PickCombinations(1)[0];
	}

	public List<List<int>> PickCombinations(int nbCombinations)
	{
		if (mutualCondition == null && indexCondition == null)
			return GetAllCombinations().PickRandomElements(nbCombinations);
		else
		{
			List<List<int>> res = new List<List<int>>();
			List<List<int>> allCombinations = GetAllCombinations();
			for (int i = 0 ; i < nbCombinations ; i++)
			{
				// For safety, if we don't have enough combinations left to pick from, reset the list
				if (allCombinations.Count < nbCombinations - i)
				{
					Debug.LogError("The list of combinations which respects the mutual condition is too small. Resetting the list. There will be repetitions.");
					allCombinations = GetAllCombinations();
				}
				List<List<int>> indexCombinations = allCombinations;
				if (indexCondition != null)
					indexCombinations = allCombinations.FindAll((List<int> obj) => indexCondition(i, obj));

				List<int> pick = indexCombinations.PickRandomElement();
				res.Add(pick);
				if (mutualCondition != null)
					allCombinations.RemoveAll(lst => !mutualCondition(lst, pick));
			}
			return res;
		}
	}

	public List<List<int>> GetAllCombinations()
	{
		List<List<int>> valuesSets = new List<List<int>>();
		foreach (IntMinMaxRange range in ranges)
		{
			valuesSets.Add(range.GetAllPossibleValues());
		}
		List<List<int>> res = GetAllCombinationsRec(new List<List<int>>(){new List<int>()}, valuesSets);
		if (condition != null)
			res = res.FindAll(condition);
		return res;
	}

	List<List<int>> GetAllCombinationsRec(List<List<int>> beg, List<List<int>> rest)
	{
		if (rest.Count == 0)
			return beg;
		List<List<int>> begPlusOne = new List<List<int>>();
		foreach(List<int> begEntry in beg)
		{
			foreach(int restNext in rest[0])
			{
				List<int> begPlusOneEntry = new List<int>(begEntry);
				begPlusOneEntry.Add(restNext);
				begPlusOne.Add(begPlusOneEntry);
			}
		}
		return GetAllCombinationsRec(begPlusOne, rest.GetRange(1, rest.Count - 1));
	}


}
