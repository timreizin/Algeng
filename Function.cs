namespace Algeng;

class Function
{
    private List<Statement> _statements;
    private Expression _returnExpression;

    public void AddStatement(Statement statement)
    {
        _statements.Add(statement);
    }

    public void SetReturn(Expression expression)
    {
        _returnExpression = expression;
    }
    public long Call(List<Variable> variables, List<Function> functions)
    {
        foreach (Statement statement in _statements)
        {
            statement.Execute(variables, functions);
        }
        return _returnExpression.Evaluate(variables, functions);
    }
}