using System.Runtime.InteropServices.JavaScript;
using System.Security.AccessControl;

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
        if (line.Contains(':'))
            return CodeStructure.Function;
        if (line.Contains('='))
            return _isInFunction ? CodeStructure.InsideFunctionStatement : CodeStructure.Statement;
        return _isInFunction ? CodeStructure.InsideFunctionExpression : CodeStructure.Expression;
    }

    private int GetFunctionId(string name)
    {
        if (!_functionNameMap.ContainsKey(name))
        {
            int id = _functionNameMap.Count;
            _functionNameMap[name] = id;
        }
        
        return _functionNameMap[name];
    }
    
    private int GetVariableId(string name)
    {
        if (_isInFunction)
        {
            if (!_insideFunctionVariableNameMap.ContainsKey(name))
            {
                int id = _insideFunctionVariableNameMap.Count;
                _insideFunctionVariableNameMap[name] = id;
            }

            return _insideFunctionVariableNameMap[name];
        }
        else
        {
            if (!_variableNameMap.ContainsKey(name))
            {
                int id = _variableNameMap.Count;
                _variableNameMap[name] = id;
            }

            return _variableNameMap[name];
        }
    }

    private Expression ParseAccumulated(string code, List<Expression> arguments)
    {
        if (arguments.Count > 0)
        {
            List<Expression> copy = new List<Expression>(arguments);
            return new FunctionExpression(GetFunctionId(code), copy);
        }

        if (code.Any(Char.IsLetter))
            return new VariableExpression(GetVariableId(code));
        
        return new ConstantExpression(Convert.ToInt64(code));
    }

    private void CompressStacks(Stack<Expression> expressionStack, Stack<Char> operatorStack)
    {
        while (operatorStack.Count > 0 && expressionStack.Count > 0)
        {
            if (operatorStack.Peek() == '-')
                expressionStack.Push(new UnaryOperation(new Negation(), expressionStack.Pop()));
            else
            {
                if (expressionStack.Count < 2) return;
                Expression operand2 = expressionStack.Pop();
                Expression operand1 = expressionStack.Pop();
                BinaryOperator opOperator = new Addition();
                if (operatorStack.Peek() == '*')
                    opOperator = new Multiplication();
                expressionStack.Push(new BinaryOperation(opOperator, operand1, operand2));
            }
            operatorStack.Pop();
        }
    }
    
    private Expression ParseExpression(string code, ref int index)
    {
        Stack<Expression> expressionStack = new Stack<Expression>();
        Stack<Char> operatorStack = new Stack<char>();
        List<char> accumulateCharacters = new List<char>();
        List<Expression> accumulateExpressions = new List<Expression>();
        for (; index < code.Length; ++index)
        {
            if (code[index] == ' ')
                continue;
            
            if (!"+-*()[],|".Contains(code[index]))
            {
                accumulateCharacters.Add(code[index]);
                continue;
            }

            if (code[index] == '[' || code[index] == ',')
            {
                ++index;
                accumulateExpressions.Add(ParseExpression(code, ref index));
                continue;
            }
            
            if (accumulateCharacters.Count > 0)
            {
                expressionStack.Push(ParseAccumulated(new string(accumulateCharacters.ToArray()), accumulateExpressions));
                accumulateCharacters.Clear();
                accumulateExpressions.Clear();
                CompressStacks(expressionStack, operatorStack);
            }

            if (code[index] == ')' || code[index] == ']' || code[index] == '|')
                return expressionStack.Peek();
            
            if (code[index] == '-')
            {
                operatorStack.Push('+');
                operatorStack.Push('-');
            }

            if (code[index] == '+' || code[index] == '*')
            {
                operatorStack.Push(code[index]);
            }

            if (code[index] == '(')
            {
                ++index;
                expressionStack.Push(ParseExpression(code, ref index));
                CompressStacks(expressionStack, operatorStack);
            }
        }
        
        if (accumulateCharacters.Count > 0)
        {
            expressionStack.Push(ParseAccumulated(new string(accumulateCharacters.ToArray()), accumulateExpressions));
            accumulateCharacters.Clear();
            accumulateExpressions.Clear();
            CompressStacks(expressionStack, operatorStack);
        }
        
        return expressionStack.Peek();
    }

    public Expression ParseExpression(string code)
    {
        int index = 0;
        List<char> newCode = new List<char>();
        foreach (char i in code)
        {
            if (i == ',')
                newCode.Add('|');
            newCode.Add(i);
        }
        Expression expression = ParseExpression(new string(newCode.ToArray()), ref index);
        if (_isInFunction)
        {
            _isInFunction = false;
            _insideFunctionVariableNameMap = new Dictionary<string, int>();
        }

        return expression;
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
        for (int i = 1; i < arguments.Length; ++i)
            GetVariableId(arguments[i]);
        return new Function();
    }
}