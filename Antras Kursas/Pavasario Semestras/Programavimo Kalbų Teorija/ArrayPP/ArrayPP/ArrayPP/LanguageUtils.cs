namespace ArrayPP.Structures;

public static class LanguageUtils
{
    public const float Epsilon = 1e-6f;
    public static VariableType MatchTypeText(string typeText)
    {
        return typeText switch
        {
            "int" => VariableType.Int,
            "float" => VariableType.Float,
            "string" => VariableType.String,
            "bool" => VariableType.Bool,
            "char" => VariableType.Char,
            "void" => VariableType.Void,
            _ => throw new ArgumentException($"Type '{typeText}' does not exist")
        };
    }

    public static bool IsNumeric(Symbol symbol) =>
        symbol.Type is VariableType.Int or VariableType.Float;

    public static bool IsTextual(Symbol symbol) =>
        symbol.Type is VariableType.Char or VariableType.String;

    public static float ParseFloat(Symbol symbol)
    {
        return symbol.Type switch
        {
            VariableType.Float => symbol.GetValue<float>(),
            VariableType.Int => symbol.GetValue<int>(),
            _ => throw new Exception($"Failed to parse '{symbol.Type.ToString()}' to float.")
        };
    }
}