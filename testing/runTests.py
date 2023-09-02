import pytest
import os, os.path
import filecmp

def test_expression():
    testIn = os.path.join(os.getcwd(), "tests/expression.in")
    testOut = os.path.join(os.getcwd(), "tests/expression.out")
    os.system(f"cat {testIn} | {os.path.join(os.getcwd(), '../bin/Debug/net7.0/Algeng')} > out.txt")
    result = filecmp.cmp(testOut, os.path.join(os.getcwd(), "out.txt"))
    os.system(f"rm out.txt")
    assert result, f"Expression test failed"

def test_variable():
    testIn = os.path.join(os.getcwd(), "tests/variable.in")
    testOut = os.path.join(os.getcwd(), "tests/variable.out")
    os.system(f"cat {testIn} | {os.path.join(os.getcwd(), '../bin/Debug/net7.0/Algeng')} > out.txt")
    result = filecmp.cmp(testOut, os.path.join(os.getcwd(), "out.txt"))
    os.system(f"rm out.txt")
    assert result, f"Variable test failed"

def test_variable_expression():
    testIn = os.path.join(os.getcwd(), "tests/variable_expression.in")
    testOut = os.path.join(os.getcwd(), "tests/variable_expression.out")
    os.system(f"cat {testIn} | {os.path.join(os.getcwd(), '../bin/Debug/net7.0/Algeng')} > out.txt")
    result = filecmp.cmp(testOut, os.path.join(os.getcwd(), "out.txt"))
    os.system(f"rm out.txt")
    assert result, f"Variable expression test failed"

def test_function():
    testIn = os.path.join(os.getcwd(), "tests/function.in")
    testOut = os.path.join(os.getcwd(), "tests/function.out")
    os.system(f"cat {testIn} | {os.path.join(os.getcwd(), '../bin/Debug/net7.0/Algeng')} > out.txt")
    result = filecmp.cmp(testOut, os.path.join(os.getcwd(), "out.txt"))
    os.system(f"rm out.txt")
    assert result, f"Function test failed"

def test_function_expression():
    testIn = os.path.join(os.getcwd(), "tests/function_expression.in")
    testOut = os.path.join(os.getcwd(), "tests/function_expression.out")
    os.system(f"cat {testIn} | {os.path.join(os.getcwd(), '../bin/Debug/net7.0/Algeng')} > out.txt")
    result = filecmp.cmp(testOut, os.path.join(os.getcwd(), "out.txt"))
    os.system(f"rm out.txt")
    assert result, f"Function expression test failed"