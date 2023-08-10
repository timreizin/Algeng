using System.Runtime.InteropServices.JavaScript;

namespace Algeng;

enum CodeStructure
{
    Expression, Statement, Function, InsideFunctionExpression, InsideFunctionStatement
}

class Parser
{
    private Dictionary<String, int> _variableNameMap = new Dictionary<string, int>();
    private Dictionary<String, int> _functionNameMap = new Dictionary<string, int>();
    private Dictionary<String, int> _insideFunctionVariableNameMap = new Dictionary<string, int>();

    private bool _isInFunction = false;

    public CodeStructure GetLineType(string line)
    {
        if (!line.Contains(':'))
            return CodeStructure.Function;
        if (!line.Contains('='))
            return _isInFunction ? CodeStructure.InsideFunctionStatement : CodeStructure.Statement;
        return _isInFunction ? CodeStructure.InsideFunctionExpression : CodeStructure.Expression;
    }

    private int GetFunctionId(string name)
    {
        return _functionNameMap[name];
    }
    
    private int GetVariableId(string name)
    {
        if (_isInFunction)
        {
            if (_insideFunctionVariableNameMap.ContainsKey(name))
                return _insideFunctionVariableNameMap[name];
            int id = _insideFunctionVariableNameMap.Count;
            _insideFunctionVariableNameMap[name] = id;
            return id;
        }
        else
        {
            if (_variableNameMap.ContainsKey(name))
                return _variableNameMap[name];
            int id = _variableNameMap.Count;
            _variableNameMap[name] = id;
            return id;
        }
    }

    private Expression ParseAccumulated(string code)
    {
        if (code.Contains('['))
        {
            string[] arguments = code.Split(new []{'[', ']', ','}, StringSplitOptions.RemoveEmptyEntries);
            int functionId = GetFunctionId(arguments[0]);
            List<int> argumentIds = new List<int>();
            for (int i = 1; i < arguments.Length; ++i)
                argumentIds.Add(GetVariableId(arguments[i]));
            return new FunctionExpression(functionId, argumentIds);
        }

        if (code.Any(Char.IsLetter))
            return new VariableExpression(GetVariableId(code));
        
        return new ConstantExpression(Convert.ToInt64(code));
    }

    private void CompressStacks(Stack<Expression> expressionStack, Stack<Char> operatorStack)
    {
        while (operatorStack.Count > 0 && expressionStack.Count > 0)
        {
            if (operatorStack.Pop() == '-')
                expressionStack.Push(new UnaryOperation(new Negation(), expressionStack.Pop()));
            else
            {
                if (expressionStack.Count < 2) return;
                Expression operand2 = expressionStack.Pop();
                Expression operand1 = expressionStack.Pop();
                BinaryOperator opOperator = new Addition();
                if (operatorStack.Peek() == '*')
                    opOperator = new Addition();
                operatorStack.Pop();
                expressionStack.Push(new BinaryOperation(opOperator, operand1, operand2));
            }
        }
    }
    
    private Expression ParseExpression(string code, ref int index)
    {
        Stack<Expression> expressionStack = new Stack<Expression>();
        Stack<Char> operatorStack = new Stack<char>();
        List<char> accumulateCharacters = new List<char>();
        for (int i = index; i < code.Length; ++i)
        {
            if (code[i] == ' ')
                continue;
            
            if (!"+-*()".Contains(code[i]))
            {
                accumulateCharacters.Add(code[i]);
                continue;
            }
            
            if (accumulateCharacters.Count > 0)
            {
                expressionStack.Push(ParseAccumulated(accumulateCharacters.ToString()));
                accumulateCharacters.Clear();
                CompressStacks(expressionStack, operatorStack);
            }

            if (code[i] == ')')
                return expressionStack.Peek();
            
            if (code[i] == '-')
            {
                operatorStack.Push('+');
                operatorStack.Push('-');
                continue;
            }

            if (code[i] == '+' || code[i] == '*')
            {
                operatorStack.Push(code[i]);
                continue;
            }

            if (code[i] == '(')
            {
                expressionStack.Push(ParseExpression(code, ref i));
                CompressStacks(expressionStack, operatorStack);
            }
        }

        return expressionStack.Peek();
    }

    public Expression ParseExpression(string code)
    {
        int index = 0;
        if (_isInFunction)
        {
            _isInFunction = false;
            _insideFunctionVariableNameMap = new Dictionary<string, int>();
        }

        return ParseExpression(code, ref index);
    }

    public Statement ParseStatement(string code)
    {
        string[] parts = code.Split('=');
        int variableId = GetVariableId(parts[0].Trim());
        Expression expression = ParseExpression(parts[1]);
        Statement statement = new Statement(variableId, expression);
        return statement;
    }

    public Function ParseFunction(string code)
    {
        string[] arguments = code.Split(new []{'[', ']', ',', ':'}, StringSplitOptions.RemoveEmptyEntries);
        int functionId = GetFunctionId(arguments[0]);
        _isInFunction = true;
        
        List<int> argumentIds = new List<int>();
        for (int i = 1; i < arguments.Length; ++i)
            GetVariableId(arguments[i]);
        return new Function();
    }
}