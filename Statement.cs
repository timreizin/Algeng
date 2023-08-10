namespace Algeng;

class Statement
{
    private int _variableId;
    private Expression _expression;

    public Statement(int variableId, Expression expression)
    {
        _variableId = variableId;
        _expression = expression;
    }

    public void Execute(List<Variable> variables, List<Function> functions)
    {
        if (_variableId >= variables.Count)
        {
            variables.Add(new Variable());
        }
        variables[_variableId].Value = _expression.Evaluate(variables, functions);
    }
}