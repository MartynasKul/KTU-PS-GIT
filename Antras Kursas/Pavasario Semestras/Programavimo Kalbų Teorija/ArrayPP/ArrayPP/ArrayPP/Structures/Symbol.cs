namespace ArrayPP.Structures;

public class Symbol
{
    public VariableType Type { get; set; }
    public object? Value { get; set; }
    
    public Symbol(VariableType type, object? value)
    {
        Type = type;
        Value = value;
    }

    public TValue GetValue<TValue>()
    {
        if (Value is TValue value) return value;
        throw new Exception($"Cannot convert Symbol of type '{Type.ToString()}' to type '{nameof(TValue)}'.");
    }

    public Type? GetRawType()
    {
        return Type switch
        {
            VariableType.Bool => typeof(bool),
            VariableType.Int => typeof(int),
            VariableType.Float => typeof(float),
            VariableType.Char => typeof(char),
            VariableType.String => typeof(string),
            _ => null
        };
    }
}