using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowsAndColumnsSpawnAreaCollider2D : SpawnAreaCollider2D {


	public override Vector3 PickPosition (bool local)
	{
		return PickPositions(1, 0f, local)[0];
	}

	public override List<Vector3> PickPositions (int number, float minDistance, bool local)
	{
		List<Vector3> res = new List<Vector3>();
		// TODO: Use inner box instead of bounds
		Bounds bounds = collider2D.bounds;
		float ratio = bounds.size.x / bounds.size.y;
		Decomposition bestDecomposition = CalculateBestShape(number, ratio);
		for (int i = 0 ; i < bestDecomposition.nbRows && res.Count < number ; i++)
		{
			for (int j = 0 ; j < bestDecomposition.nbColumns && res.Count < number ; j++)
			{
				Vector3 center = local ? transform.TransformPoint(bounds.center) : bounds.center;
				float scaleX = bounds.size.x / bestDecomposition.nbColumns;
				float scaleY = bounds.size.y / bestDecomposition.nbRows;
				res.Add(center + new Vector3(scaleX * (j - 0.5f * (bestDecomposition.nbColumns - 1)), -scaleY * (i - 0.5f * (bestDecomposition.nbRows - 1))));
			}
		}
		return res;
	}

	public class Decomposition 
	{ 
		public int nbRows; 
		public int nbColumns; 
		public Decomposition(int nbRows, int nbColumns){ this.nbRows = nbRows; this.nbColumns = nbColumns; } 
		public float ratio { get { return (float)nbColumns / (float)nbRows; } }
		public override string ToString ()
		{
			return string.Format ("[Decomposition: nbRows={0}, nbColumns={1}]", nbRows, nbColumns);
		}
	}

	Decomposition CalculateBestShape(int number, float ratio)
	{
		List<Decomposition> decompositions = GetDecompositions(number);
		Decomposition bestDecomposition = new Decomposition(number, 1);
		float bestDecompositionFactor = float.MaxValue;
		foreach(Decomposition decomposition in decompositions)
		{
			float factor = Mathf.Abs(ratio - decomposition.ratio);
			if (factor < bestDecompositionFactor)
			{
				bestDecomposition = decomposition;
				bestDecompositionFactor = factor;
			}
		}
		return bestDecomposition;

	}

	public static List<Decomposition> GetDecompositions(int number)
	{
		List<Decomposition> decompositions = new List<Decomposition>();
		List<int> primeFactors = PrimeFactors(number);
		if (primeFactors.Count == 1)
		{
			for (int i = 1 ; i < Mathf.Sqrt(number) ; i++)
			{
				int otherDimension = Mathf.CeilToInt( number / (float)i);
				decompositions.Add(new Decomposition(i, otherDimension));
				decompositions.Add(new Decomposition(otherDimension, i));
			}
		}
		else
		{
			for (int i = 1 ; i < primeFactors.Count ; i++)
			{
				foreach(List<int> comb in primeFactors.Combinations(i))
				{
					List<int> rest = new List<int>(primeFactors);
					foreach(int nb in comb)
						rest.Remove(nb);
					decompositions.Add(new Decomposition(comb.Aggregate((a, x) => a * x), rest.Aggregate((a, x) => a * x)));
				}
			}
		}
		return decompositions;
	}

	/// <summary>
	/// Find prime factors
	/// </summary>
	public static List<int> PrimeFactors(int a)
	{
		List<int> retval = new List<int>();
		for (int b = 2; a > 1; b++)
		{               
			while (a % b == 0)
			{
				a /= b;
				retval.Add(b);
			}               
		}
		return retval;
	}

}
