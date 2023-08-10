namespace Algeng;

abstract class Expression
{
    public abstract long Evaluate(List<Variable> variables, List<Function> functions);
}

class ConstantExpression : Expression
{
    private long _constant;

    public ConstantExpression(long constant)
    {
        _constant = constant;
    }
    
    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        return _constant;
    }
}

class VariableExpression : Expression
{
    private int _variableId;

    public VariableExpression(int variableId)
    {
        _variableId = variableId;
    }

    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        return variables[_variableId].Value;
    }
}

class FunctionExpression : Expression
{
    private int _functionId;
    private List<int> _argumentIds;

    public FunctionExpression(int functionId, List<int> argumentIds)
    {
        _functionId = functionId;
        _argumentIds = argumentIds;
    }

    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        List<Variable> functionVariables = new List<Variable>();
        foreach (int id in _argumentIds) 
            functionVariables.Add(variables[id]);
        return functions[_functionId].Call(functionVariables, functions);
    }
}

class UnaryOperation : Expression
{
    private UnaryOperator _operator;
    private Expression _expression;

    public UnaryOperation(UnaryOperator opOperator, Expression expression)
    {
        _operator = opOperator;
        _expression = expression;
    }

    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        return _operator.Apply(_expression.Evaluate(variables, functions));
    }
}

class BinaryOperation : Expression
{
    private BinaryOperator _operator;
    private Expression _expression1, _expression2;

    public BinaryOperation(BinaryOperator opOperator, Expression expression1, Expression expression2)
    {
        _operator = opOperator;
        _expression1 = expression1;
        _expression2 = expression2;
    }

    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        return _operator.Apply(_expression1.Evaluate(variables, functions),
            _expression2.Evaluate(variables, functions));
    }
}