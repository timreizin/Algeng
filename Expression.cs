namespace Algeng;

//A class that represents a single expression
abstract class Expression
{
    //Function that can be used to evaluate the expression
    public abstract long Evaluate(List<Variable> variables, List<Function> functions);
}

//A class that represents a constant expression, that is a single number
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

//A class that represents a variable expression, that is a single variable
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

//A class that represents a function expression, that is a single function and all its call arguments
class FunctionExpression : Expression
{
    private int _functionId;
    private List<Expression> _arguments;

    public FunctionExpression(int functionId, List<Expression> arguments)
    {
        _functionId = functionId;
        _arguments = arguments;
    }

    public override long Evaluate(List<Variable> variables, List<Function> functions)
    {
        List<Variable> functionVariables = new List<Variable>();
        foreach (Expression expression in _arguments) 
            functionVariables.Add(new Variable(expression.Evaluate(variables, functions)));
        return functions[_functionId].Call(functionVariables, functions);
    }
}

//A class that represents an unary operation, that is an expression, and an unary operator that has to be applied to it
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

//A class that represents a binary operation, that two expressions, and a binary operator that has to be applied to them
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