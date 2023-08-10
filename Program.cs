namespace Algeng
{
    class Program
    {

        static void Main()
        {
            string input;
            List<Variable> variables = new List<Variable>();
            List<Function> functions = new List<Function>();
            Parser parser = new Parser();
            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                //Console.WriteLine((int)parser.GetLineType(input));
                switch (parser.GetLineType(input))
                {
                    case CodeStructure.Expression:
                        Console.WriteLine(parser.ParseExpression(input).Evaluate(variables, functions));
                        break;
                    case CodeStructure.Statement:
                        parser.ParseStatement(input).Execute(variables, functions);
                        break;
                    case CodeStructure.Function:
                        functions.Add(parser.ParseFunction(input));
                        break;
                    case CodeStructure.InsideFunctionExpression:
                        functions.Last().SetReturn(parser.ParseExpression(input));
                        break;
                    case CodeStructure.InsideFunctionStatement:
                        functions.Last().AddStatement(parser.ParseStatement(input));
                        break;
                }
            }
        }
    }
}