namespace Algeng
{
    public class Program
    {
        static List<Variable> _variables = new List<Variable>();
        static List<Function> _functions = new List<Function>();
        static Parser _parser = new Parser();
        
        //A function that processes a line of the input
        public static string ProcessLine(string line)
        {
            long? result = null;
            switch (_parser.GetLineType(line))
            {
                case CodeStructure.Expression:
                    result = _parser.ParseExpression(line).Evaluate(_variables, _functions);
                    break;
                case CodeStructure.Statement:
                    _parser.ParseStatement(line).Execute(_variables, _functions);
                    break;
                case CodeStructure.Function:
                    _functions.Add(_parser.ParseFunction(line));
                    break;
                case CodeStructure.InsideFunctionExpression:
                    _functions.Last().SetReturn(_parser.ParseExpression(line));
                    break;
                case CodeStructure.InsideFunctionStatement:
                    _functions.Last().AddStatement(_parser.ParseStatement(line));
                    break;
            }

            if (result == null)
                return "";
            return result.ToString() + '\n';
        }
        
        
        //A function that runs the program, it reads lines one by one and write output from the processing function
        static void Main()
        {
            string input;
            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
                Console.Write(ProcessLine(input));
        }
    }
}