namespace Algeng;

abstract class UnaryOperator
{
    public abstract long Apply(long operand);
}

class Negation : UnaryOperator
{
    public override long Apply(long operand)
    {
        return -operand;
    }
}

abstract class BinaryOperator
{
    public abstract long Apply(long operand1, long operand2);
}

class Addition : BinaryOperator
{
    public override long Apply(long operand1, long operand2)
    {
        return operand1 + operand2;
    }
}

class Multiplication : BinaryOperator
{
    public override long Apply(long operand1, long operand2)
    {
        return operand1 * operand2;
    }
}