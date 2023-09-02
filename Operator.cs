namespace Algeng;

//Abstract class for unary operators
abstract class UnaryOperator
{
    //A member function Apply, that can be used to apply the operator class represents
    public abstract long Apply(long operand); 
}

//An unary operator that negates the number
class Negation : UnaryOperator
{
    public override long Apply(long operand)
    {
        return -operand;
    }
}

//Abstract class for binary operators
abstract class BinaryOperator
{
    //A member function Apply, that can be used to apply the operator class represents
    public abstract long Apply(long operand1, long operand2);
}

//A binary operator that adds two numbers
class Addition : BinaryOperator
{
    public override long Apply(long operand1, long operand2)
    {
        return operand1 + operand2;
    }
}

//A binary operator that multiplies two numbers
class Multiplication : BinaryOperator
{
    public override long Apply(long operand1, long operand2)
    {
        return operand1 * operand2;
    }
}