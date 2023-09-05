using System.Text;
using NUnit.Framework;
using Algeng;

namespace Tests
{
    public class Tests
    {

        [Test]
        public void TestExpression()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathIn = dir + @"/../../../testing/tests/expression.in";
            string pathOut = dir + @"/../../../testing/tests/expression.out";
            string line;
            StreamReader sr = new StreamReader(pathIn);
            List<string> output = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine())) 
                output.Append(Program.ProcessLine(line));
            List<string> correct = new List<string>();
            sr = new StreamReader(pathOut);
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                correct.Append(line);
            Assert.AreEqual(output, correct);
        }
        
        [Test]
        public void TestVariable()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathIn = dir + @"/../../../testing/tests/variable.in";
            string pathOut = dir + @"/../../../testing/tests/variable.out";
            string line;
            StreamReader sr = new StreamReader(pathIn);
            List<string> output = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine())) 
                output.Append(Program.ProcessLine(line));
            List<string> correct = new List<string>();
            sr = new StreamReader(pathOut);
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                correct.Append(line);
            Assert.AreEqual(output, correct);
        }
        
        [Test]
        public void TestVariableExpression()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathIn = dir + @"/../../../testing/tests/variable_expression.in";
            string pathOut = dir + @"/../../../testing/tests/variable_expression.out";
            string line;
            StreamReader sr = new StreamReader(pathIn);
            List<string> output = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine())) 
                output.Append(Program.ProcessLine(line));
            List<string> correct = new List<string>();
            sr = new StreamReader(pathOut);
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                correct.Append(line);
            Assert.AreEqual(output, correct);
        }
        
        [Test]
        public void TestFunction()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathIn = dir + @"/../../../testing/tests/function.in";
            string pathOut = dir + @"/../../../testing/tests/function.out";
            string line;
            StreamReader sr = new StreamReader(pathIn);
            List<string> output = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine())) 
                output.Append(Program.ProcessLine(line));
            List<string> correct = new List<string>();
            sr = new StreamReader(pathOut);
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                correct.Append(line);
            Assert.AreEqual(output, correct);
        }
        
        [Test]
        public void TestFunctionExpression()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathIn = dir + @"/../../../testing/tests/function_expression.in";
            string pathOut = dir + @"/../../../testing/tests/function_expression.out";
            string line;
            StreamReader sr = new StreamReader(pathIn);
            List<string> output = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine())) 
                output.Append(Program.ProcessLine(line));
            List<string> correct = new List<string>();
            sr = new StreamReader(pathOut);
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                correct.Append(line);
            Assert.AreEqual(output, correct);
        }
    }
}