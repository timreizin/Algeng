namespace Algeng;

//Class that represents a single variable
class Variable
{
    //Current value of the variable
    private long _value; 
    public long Value
    {
        get => _value;
        set => _value = value;
    }

    public Variable(long value = 0)
    {
        _value = value;
    }
}