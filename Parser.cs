using System;
using System.Reflection.Metadata.Ecma335;

public class Parser
{
	static private int units = 0;
	private String expression;
	private short[] op;
	private short[] nestedLevel;
	public String Expression
	{
		get; set;
	}
	private ExpressionUnit[] exprUnits;
	Parser(String data)
	{
		if (data.Equals(null))
		{
			Console.WriteLine("Empty expression!");
			return;
		}

		expression = new String(data);
        expression = expression.Replace(" ", "");

		expressionOpAndNestDetermine();
		if (!expressionDetectErrors())
			expressionUnitsCreate();
		else
			Console.WriteLine("Expression have mistakes, abort");
	}

	private void expressionOpAndNestDetermine()
	{ // Method fill op and nestedLevel arrays with values by conditions
		short nl = 0;
		op = new short[expression.Length];
		nestedLevel = new short[expression.Length];
		for (int i = 0; i < expression.Length; i++)
		{
			switch (expression.ElementAt(i))
			{
				case '+':
				case '-':
					op[i] = 1;
					nestedLevel[i] = nl;
					break;
				case '*':
				case ':':
				case '/':
					op[i] = 2;
					nestedLevel[i] = nl;
					break;
				case '^':
					op[i] = 3;
					nestedLevel[i] = nl;
					break;
				case '(':
					op[i] = 0;
					nestedLevel[i] = ++nl;
					break;
				case ')':
					op[i] = 0;
					nestedLevel[i] = ++nl;
					break;
				default:
					op[i] = 0;
					nestedLevel[i] = nl;
				break;
			}
		}
	}

	private bool isSubsequenceHaveMistakes(ref int startPos)
	{
		int saveStartPos = startPos;
		while (saveStartPos == startPos)
		{

		}
		return false;
	}
	private bool expressionDetectErrors()
	{ // Return false if expression sentence have syntax mistakes
		if (op.Length % 2 > 0) return false;
		if (nestedLevel.Count(x => x < 0) > 0) return false;
		if (nestedLevel.Count(x => x == 0) % 2 > 0) return false;
		for (int i = 0; i < op.Length; i++)
		{
			if ((op[i] > 0 && op[i-1] != 0) ||
				(op[i] > 0 && i+1 != op.Length && op[i+1] != 0))
			{
				return false;
			}
		}
		// op must contain subsequences of digits, where 
		// {0, [i], 0}, where [i] = {0, 1, 2, 3}

		if (nestedLevel[nestedLevel.Length - 1] > 1) return false;

		int iSymbolPos,
			diffrencePath;
        
		for (int i = 0; i < nestedLevel.Length; i++) 
		{
			iSymbolPos = nestedLevel[i];
			isSubsequenceHaveMistakes(ref i);
		}

		return true;
	}
	private void expressionUnitsCreate()
	{ // Init of ExpressionUnits array with true order of execute operators
		//Count of operator == count of units
		exprUnits = new ExpressionUnit[op.Count(x => x != 0)];
		
	}

}
