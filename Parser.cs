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
	public Parser(String data)
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
		{
			Console.WriteLine("Expression have no mistakes");
			expressionUnitsCreate(); 
		}
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
					nestedLevel[i] = nl--;
					break;
				default:
					op[i] = 0;
					nestedLevel[i] = nl;
				break;
			}
		}
	}

	private bool isSubsequenceHaveMistakes(int startPos, ref short[] arr)
	{
		int i = startPos+1;
		while (i < arr.Length &&
			arr[i] == arr[startPos])
		{
			i++;
		}
		i--;
		if (i == arr.Length-1 && arr[i] == 0)
		{
			return false;
		} else
		{
			if (arr[i] > arr[startPos])
			{ //Sign of nl was changed as increment
				return isSubsequenceHaveMistakes(i, ref arr);
			}
            if (i - startPos >= 5 && (i - startPos) % 2 > 0)
            {
				for (int j = startPos; j < i; j++)
					arr[j]--;
				return isSubsequenceHaveMistakes(i, ref arr);
			}
            else
                return true;
		}
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
			short []arrOfNestedLevel = new short[nestedLevel.Length];
			for (int j = 0; j < nestedLevel.Length; j++)
			{
				arrOfNestedLevel[j] = nestedLevel[j];
			}
			return !isSubsequenceHaveMistakes(0, ref arrOfNestedLevel);
			
		}

		return true;
	}
	private void expressionUnitsCreate()
	{ // Init of ExpressionUnits array with true order of execute operators
		//Count of operator == count of units
		//exprUnits = new ExpressionUnit[op.Count(x => x != 0)];
		
	}

}
