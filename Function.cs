namespace Algeng;

//Class that represents a single function
class Function
{
    //List of statements that compose the body of the function
    private List<Statement> _statements = new List<Statement>();
    //Return expression of the function
    private Expression _returnExpression;

    public void AddStatement(Statement statement)
    {
        _statements.Add(statement);
    }

    public void SetReturn(Expression expression)
    {
        _returnExpression = expression;
    }
    
    //Function that can be used to call the function with the given list of variables and (other)functions
    public long Call(List<Variable> variables, List<Function> functions)
    {
        foreach (Statement statement in _statements)
        {
            statement.Execute(variables, functions);
        }
        return _returnExpression.Evaluate(variables, functions);
    }
}