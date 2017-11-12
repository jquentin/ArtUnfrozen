using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MathOperator {

	public enum Operation { Add, Subtract, Multiply, Divide, Set }
	public Operation operation;

	public MathOperator(Operation operation)
	{
		this.operation = operation;
	}

	public Func<int, int, int> intFunction
	{
		get
		{
			switch (operation)
			{
				case Operation.Add:
					return (a, b) => a + b;
				case Operation.Subtract:
					return (a, b) => a - b;
				case Operation.Multiply:
					return (a, b) => a * b;
				case Operation.Divide:
					return (a, b) => a / b;
				case Operation.Set:
				default:
					return (a, b) => b;
				
			}
		}
	}
	public Func<float, float, float> floatFunction
	{
		get
		{
			switch (operation)
			{
				case Operation.Add:
					return (a, b) => a + b;
				case Operation.Subtract:
					return (a, b) => a - b;
				case Operation.Multiply:
					return (a, b) => a * b;
				case Operation.Divide:
					return (a, b) => a / b;
				case Operation.Set:
				default:
					return (a, b) => b;

			}
		}
	}

	public static MathOperator FromString(string s, Operation defaultOp)
	{
		Operation op;
		try
		{
			op = (Operation) Enum.Parse(typeof(Operation), s);
		}
		catch
		{
			op = defaultOp;
		}
		return new MathOperator(op);
	}

	public override string ToString ()
	{
		return string.Format ("[MathOperator: operation={0}]", operation.ToString());
	}

}
