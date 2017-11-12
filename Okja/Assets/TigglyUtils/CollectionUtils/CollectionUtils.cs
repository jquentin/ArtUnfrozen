using UnityEngine;
using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



public class CircularQueue<Type>:Queue<Type>
{
	
	public int maxSize { get; private set; }
	
	public CircularQueue(int size):base(size)
	{
		maxSize = size;
	}
	
	public void EnqueueWithLimit(Type element)
	{
		Enqueue(element);
		if (Count > maxSize)
			Dequeue();
	}
}

public static class CollectionUtils 
{

	public static List<List<T>> Combinations<T>(this List<T> elements, int k)
	{
		if (k > elements.Count)
			return new List<List<T>>();
		else if (k == elements.Count)
			return new List<List<T>>(){new List<T>(elements)};
		else if (k == 1)
			return elements.Select(t => new List<T>(){t}).ToList();
		
		List<List<T>> res = new List<List<T>>();
		for(int i = 0 ; i <= elements.Count - k ; i++)
		{
			List<List<T>> subCombs = Combinations(elements.GetRange(i + 1, elements.Count - (i + 1)), k - 1);
			subCombs.ForEach(list => list.Insert(0, elements[i]));
			res.AddRange(subCombs);
		}
		return res;
	}

	public static string ListToString<T>(this List<T> elements)
	{
		return string.Format("{{ {0} }}", string.Join(" ", elements.Select((T arg) => arg.ToString()).ToArray()));
	}

	public static string ToStringInDepth(this System.Object o)
	{
		Type type = o.GetType();

		// if it's a generic, check if it's a collection or keyvaluepair
		if (type.IsGenericType) {
			// a collection? iterate items
			if (o is System.Collections.IEnumerable) {
				StringBuilder result = new StringBuilder("{");
				bool hasAtLeastOneItem = false;
				foreach (var i in (o as System.Collections.IEnumerable)) {
					result.Append(ToStringInDepth(i));
					result.Append(", ");
					hasAtLeastOneItem = true;
				}
				if (hasAtLeastOneItem)
					result.Remove(result.Length - 2, 2);
				result.Append("}");
				return result.ToString();

				// a keyvaluepair? show key => value
			} else if (type.GetGenericArguments().Length == 2 &&
				type.FullName.StartsWith("System.Collections.Generic.KeyValuePair")) {
				StringBuilder result = new StringBuilder();
				result.Append(ToStringInDepth(type.GetProperty("Key").GetValue(o, null)));
				result.Append(" => ");
				result.Append(ToStringInDepth(type.GetProperty("Value").GetValue(o, null)));
				return result.ToString();
			}
		}
		// arbitrary generic or not generic
		return o.ToString();
	}


}
