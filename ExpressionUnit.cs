using System;

public class ExpressionUnit
{
	private Char? unitLeft = null;
	private Char? unitRight = null;
	private Char operatorBetween;
	private ExpressionUnit? nestedUnit = null;
	private double answer = 0;
	public ExpressionUnit(Char unitLeft, Char unitRight, Char operatorBetween)
	{ // 1 + 2 etc
		this.unitLeft = unitLeft;
		this.unitRight = unitRight;
		this.operatorBetween = operatorBetween;
	}
	public ExpressionUnit(Char unitLeft, ExpressionUnit unitRight, Char operatorBetween)
	{ //1 + x etc
		this.unitLeft = unitLeft;
		this.nestedUnit = unitRight;
		this.operatorBetween = operatorBetween;
	}
	public ExpressionUnit(ExpressionUnit unitLeft, Char unitRight, Char operatorBetween)
	{ //x + 1 etc
		this.nestedUnit = unitLeft;
		this.unitRight = unitRight;
		this.operatorBetween = operatorBetween;
	}
}
