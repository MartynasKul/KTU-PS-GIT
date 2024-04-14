namespace ArrayPP.Structures;

public class ArraySymbol
{
    public VariableType Type { get; set; }
    public List<Symbol> List { get; set; }

    public ArraySymbol(VariableType type)
        : this(type, new List<Symbol>())
    {
        
    }

    public ArraySymbol(VariableType type, List<Symbol> list)
    {
        Type = type;
        List = list;
    }

    public override string ToString()
    {
        var output = "[ ";
        
        for (var i = 0; i < List.Count; i++)
        {
            output += List[i].Value;
            if (i != List.Count - 1) output += ", ";
        }

        return output + " ]";
    }
}