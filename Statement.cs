namespace Algeng;

//A class that represents a single statement
class Statement
{
    private int _variableId;
    private Expression _expression;

    public Statement(int variableId, Expression expression)
    {
        _variableId = variableId;
        _expression = expression;
    }

    //Function that can be used to execute the statement (that is evaluate the expression and save the result to the variable (possibly creating a new one))
    public void Execute(List<Variable> variables, List<Function> functions)
    {
        if (_variableId >= variables.Count)
        {
            variables.Add(new Variable());
        }
        variables[_variableId].Value = _expression.Evaluate(variables, functions);
    }
}