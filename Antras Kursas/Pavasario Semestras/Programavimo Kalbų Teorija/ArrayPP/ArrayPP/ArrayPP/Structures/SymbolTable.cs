namespace ArrayPP.Structures;

public class SymbolTable
{
    private readonly Dictionary<string, Symbol> _symbols;

    public SymbolTable()
    {
        _symbols = new Dictionary<string, Symbol>();
    }

    public void Add(string name, VariableType type, object? value)
    {
        _symbols.Add(name, new Symbol(type, value));
    }

    public void Add(string name, Symbol symbol)
    {
        _symbols.Add(name, symbol);
    }
    
    public void Update(string name, VariableType type, object? value)
    {
        _symbols[name] = new Symbol(type, value);
    }
    
    public void Update(string name, Symbol symbol)
    {
        _symbols[name] = symbol;
    }


    public Symbol Get(string name) => _symbols[name];

    
    public bool Exists(string name) => _symbols.ContainsKey(name);
}