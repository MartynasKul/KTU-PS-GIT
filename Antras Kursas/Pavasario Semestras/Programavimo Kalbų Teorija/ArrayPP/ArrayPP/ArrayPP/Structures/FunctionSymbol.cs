using ArrayPP;

namespace ArrayPP.Structures;

public class FunctionSymbol
{
    public Symbol ReturnType { get; set; }
    public ArrayPPParser.FunctionContext Context { get; set; }
    public List<Symbol> ArgumentTypes { get; set; }
    public List<string> ArgumentNames { get; set; }

    public FunctionSymbol(ArrayPPParser.FunctionContext context,
        List<Symbol> args,
        List<string> argNames,
        Symbol? returnType = null)
    {
        Context = context;
        ArgumentTypes = args;
        ArgumentNames = argNames;
        ReturnType = returnType ?? new Symbol(VariableType.Void, null);
    }
}