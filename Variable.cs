namespace Algeng;

class Variable
{
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