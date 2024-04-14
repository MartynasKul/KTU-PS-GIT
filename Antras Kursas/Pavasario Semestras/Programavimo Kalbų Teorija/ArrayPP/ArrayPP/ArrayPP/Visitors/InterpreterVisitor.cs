using System.Globalization;
using Antlr4.Runtime.Misc;
using ArrayPP;
using ArrayPP.Structures;

namespace ArrayPP.Visitors;

public class InterpreterVisitor : ArrayPPBaseVisitor<object>
{
    private List<SymbolTable> _symbolTables;
    private Symbol? _returnValue;
    private readonly ArrayVisitor _arrayVisitor;
    private readonly OperationVisitor _operationVisitor;

    public InterpreterVisitor(List<SymbolTable> symbolTables)
    {
        _symbolTables = symbolTables;
        _arrayVisitor = new ArrayVisitor(this);
        _operationVisitor = new OperationVisitor(this);
    }
    
    public int FindSymbol(string value)
    {
        for(var i = _symbolTables.Count - 1; i >= 0; i--)
        {
            if(_symbolTables[i].Exists(value))
            {
                return i;
            }
        }
        return -1;
    }
    
    public Symbol AddSymbol(string name, Symbol symbol)
    {
        _symbolTables.Last().Add(name, symbol);
        return symbol;
    }
    
    public Symbol AddSymbol(string name, VariableType varType, object? value)
    {
        var symbol = new Symbol(varType, value);
        _symbolTables.Last().Add(name, symbol);
        return symbol;
    }

    public Symbol? GetSymbol(string name)
    {
        var index = FindSymbol(name);
        return index == -1 ? null : _symbolTables[index].Get(name);
    }

    public override object VisitFunction([NotNull] ArrayPPParser.FunctionContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var index = FindSymbol(name);
        if (index != -1)
            throw new ArgumentException($"Identifier '{name}' is in use");

        var argCount = context.declaration().Length;
        var argList = new List<Symbol>();
        var argNames = new List<string>();
        for (var i = 0; i < argCount; i++)
        {
            var type = context.declaration(i).type();
            var argName = context.declaration(i).IDENTIFIER().GetText();
            if (type.primitiveType() != null && type.GetText().Contains("[]"))
            {
                var arrayType = LanguageUtils.MatchTypeText(type.primitiveType().GetText());
                var arraySymbol = new ArraySymbol(arrayType);
                argList.Add(new Symbol(VariableType.Array, arraySymbol));
            }
            else if (type.primitiveType() != null)
            {
                var argType = LanguageUtils.MatchTypeText(type.primitiveType().GetText());
                argList.Add(new Symbol(argType, null));
            }
            else
                throw new ArgumentException(
                $"Unexpected function argument '{context.declaration(i).GetText()}' in declaration.");
            argNames.Add(argName);
        }
        
        var funcSymbol = new FunctionSymbol(context, argList, argNames);
        if (context.type() != null)
        {
            var type = context.type();
            if (type.primitiveType() != null && type.GetText().Contains("[]"))
            {
                var arrayType = LanguageUtils.MatchTypeText(type.primitiveType().GetText());
                funcSymbol.ReturnType = new Symbol(VariableType.Array, new ArraySymbol(arrayType));
            }
            else if (type.primitiveType() != null)
            {
                var returnType = LanguageUtils.MatchTypeText(type.primitiveType().GetText());
                funcSymbol.ReturnType = new Symbol(returnType, null);
            }
            else throw new ArgumentException(
                $"Unexpected function type '{type.GetText()}' in declaration.");
        }

        AddSymbol(name, VariableType.Function, funcSymbol);
        return null!;
    }
    
    public override object VisitFunctionCall([NotNull] ArrayPPParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var index = FindSymbol(name);
        if (index == -1) 
            throw new ArgumentException($"Function '{name}' was not found");

        var symbol = GetSymbol(name);
        if (symbol is null)
            throw new Exception("Function symbol is null.");
        var funcSymbol = symbol.GetValue<FunctionSymbol>();
        var args = context.expression();
        
        var parsedArgSymbols = new List<Symbol>();
        for (var i = 0; i < args.Length; i++)
        {
            if (Visit(args[i]) is not Symbol argument){
                throw new ArgumentException($"Function argument at position {i + 1} cannot be parsed.");
            }

            switch (argument.Type)
            {
                case VariableType.Void:
                    throw new ArgumentException($"Function argument at position {i + 1} cannot be void.");
                default:
                    parsedArgSymbols.Add(argument);
                    break;
            }
        }
        
        if (parsedArgSymbols.Count != funcSymbol.ArgumentTypes.Count) 
            throw new ArgumentException(
                $"Incorrect argument count. Expected {funcSymbol.ArgumentTypes.Count}, got {parsedArgSymbols.Count}.");
        
        _symbolTables.Add(new SymbolTable());
        for (var i = 0; i < funcSymbol.ArgumentTypes.Count; i++)
        {
            var argument = parsedArgSymbols[i];
            var expectedArg = funcSymbol.ArgumentTypes[i];
            
            if (argument.Type != expectedArg.Type)
            {
                throw new ArgumentException(
                    $"Function argument type mismatch at position {i + 1}. Expected {expectedArg.Type.ToString()}, found {argument.Type.ToString()}.");
            }
            
            _symbolTables.Last().Add(funcSymbol.ArgumentNames[i], expectedArg.Type, argument.Value);
        }
        
        VisitBlock(funcSymbol.Context.block());
        
        if (_returnValue is null && funcSymbol.ReturnType.Type != VariableType.Void)
            throw new Exception("Function did not return a value.");
        
        if (_returnValue is not null)
        {
            if (_returnValue.Type != funcSymbol.ReturnType.Type)
                throw new Exception(
                    $"Function returned {_returnValue.Type.ToString()} when {funcSymbol.ReturnType.Type.ToString()} was expected.");

            if (_returnValue.Type == VariableType.Array &&
                _returnValue.GetValue<ArraySymbol>().Type != funcSymbol.ReturnType.GetValue<ArraySymbol>().Type)
                throw new Exception(
                    $"Function returned array of type {_returnValue.GetValue<ArraySymbol>().Type.ToString()} when {funcSymbol.ReturnType.GetValue<ArraySymbol>().Type.ToString()} array was expected.");
            
        }
        _symbolTables.RemoveAt(_symbolTables.Count - 1);
        var returnCopy = _returnValue;
        _returnValue = null;
        return returnCopy!;
    }
    
    public override object VisitReturn([NotNull] ArrayPPParser.ReturnContext context)
    {
        if (_returnValue != null) return _returnValue;
        
        if (context.expression() == null) _returnValue = new Symbol(VariableType.Void, null);
        else
        {
            _returnValue = Visit(context.expression()) as Symbol;
            if (_returnValue is null)
                throw new Exception("Failed to parse return expression.");   
        }
        
        return _returnValue;
    }

    public override object VisitParenExpression(ArrayPPParser.ParenExpressionContext context)
    {
        return Visit(context.expression());
    }

    #region Declaration Assignment
    public override object VisitVariableDeclaration(ArrayPPParser.VariableDeclarationContext context)
    {
        var varName = context.declaration().IDENTIFIER().GetText();
        var index = FindSymbol(varName);
        if (index != -1)
            throw new Exception($"Variable '{varName}' already exists");

        var symbol = Visit(context.declaration()) as Symbol;
        var valueSymbol = Visit(context.expression()) as Symbol;

        if (symbol is null)
            throw new Exception($"Failed to parse declaration: {context.declaration().GetText()}");
        if (valueSymbol is null)
            throw new Exception($"Failed to parse expression: {context.expression().GetText()}");
        
        if (symbol.Type != valueSymbol.Type)
            throw new Exception(
                $"Cannot assign {valueSymbol.Type.ToString()} to variable of type {symbol.Type.ToString()}");
        if (symbol.Type == VariableType.Array &&
            symbol.GetValue<ArraySymbol>().Type != valueSymbol.GetValue<ArraySymbol>().Type)
        {
            var arraySymbol = valueSymbol.GetValue<ArraySymbol>();
            if (arraySymbol.Type == VariableType.Void && arraySymbol.List.Count == 0)
            {
                index = FindSymbol(varName);
                var innerArray = valueSymbol.GetValue<ArraySymbol>();
                innerArray.Type = symbol.GetValue<ArraySymbol>().Type;
                _symbolTables[index].Update(varName, symbol.Type, innerArray);
                return null!;
            }

            throw new Exception(
                $"Cannot assign array of type {valueSymbol.GetValue<ArraySymbol>().Type.ToString()} to array of type {symbol.GetValue<ArraySymbol>().Type.ToString()}.");
        }

        index = FindSymbol(varName);
        _symbolTables[index].Update(varName, valueSymbol.Type, valueSymbol.Value);

        return null!;
    }
    
    public override object VisitDeclaration(ArrayPPParser.DeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var index = FindSymbol(varName);
        if (index != -1)
            throw new ArgumentException($"Variable '{varName}' already exists");

        if (context.type().primitiveType() != null)
        {
            var varTypeText = context.type().primitiveType().GetText();
            var varType = LanguageUtils.MatchTypeText(varTypeText);

            if (!context.type().GetText().Contains("[]")) 
                return AddSymbol(varName, varType, null);
            
            var emptyArray = new Symbol(VariableType.Array, new ArraySymbol(varType));
            return AddSymbol(varName, emptyArray);

        }
        

        return null!;
    }
    
    public override object VisitAssignment(ArrayPPParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        if (Visit(context.expression()) is not Symbol symbol)
            throw new ArgumentException("Symbol is null.");

        var ind = FindSymbol(varName);
        if (ind == -1) throw new ArgumentException($"Variable '{varName}' does not exist");

        var variableSymbol = _symbolTables[ind].Get(varName);
        if(symbol.Type != variableSymbol.Type) 
            throw new ArgumentException($"Cannot convert '{symbol.Type.ToString()}' to '{variableSymbol.Type.ToString()}'");

        _symbolTables[ind].Update(varName, symbol.Type, symbol.Value);
        return symbol;
    }
    #endregion

    #region Arrays and Tuples
    public override object VisitArrayExpression(ArrayPPParser.ArrayExpressionContext context)
    {
        return _arrayVisitor.VisitArrayExpression(context);
    }
    

    public override object VisitIndexAccess(ArrayPPParser.IndexAccessContext context)
    {
        return _arrayVisitor.VisitIndexAccess(context);
    }

    public override object VisitArrayAssignment(ArrayPPParser.ArrayAssignmentContext context)
    {
        return _arrayVisitor.VisitArrayAssignment(context);
    }

    public override object VisitArrayRemoval(ArrayPPParser.ArrayRemovalContext context)
    {
        return _arrayVisitor.VisitArrayRemoval(context);
    }

    public override object VisitArrayInsert(ArrayPPParser.ArrayInsertContext context)
    {
        return _arrayVisitor.VisitArrayInsert(context);
    }

    public override object VisitArrayLength(ArrayPPParser.ArrayLengthContext context)
    {
        return _arrayVisitor.VisitArrayLength(context);
    }

    public override object VisitArrayRandomiser(ArrayPPParser.ArrayRandomiserContext context)
    {
        return _arrayVisitor.VisitArrayRandomiser(context);
    }
    
    public override object VisitArrayFill(ArrayPPParser.ArrayFillContext context)
    {
        return _arrayVisitor.VisitArrayFill(context);
    }

    public override object VisitArrayUnique(ArrayPPParser.ArrayUniqueContext context)
    {
        return _arrayVisitor.VisitArrayUnique(context);
    }
    public override object VisitArrayFilter(ArrayPPParser.ArrayFilterContext context)
    {
        return _arrayVisitor.VisitArrayFilter(context);
    }
    public override object VisitArraySlice(ArrayPPParser.ArraySliceContext context)
    {
        return _arrayVisitor.VisitArraySlice(context);
    }
    #endregion

    #region Constants
    public override object VisitIntegerConstant(ArrayPPParser.IntegerConstantContext context)
    {
        var i = new Symbol(VariableType.Int, int.Parse(context.INTEGER().GetText()));
        return i;
    }

    public override object VisitBoolConstant(ArrayPPParser.BoolConstantContext context)
    {
        return new Symbol(VariableType.Bool, bool.Parse(context.BOOL().GetText()));
    }

    public override object VisitFloatConstant(ArrayPPParser.FloatConstantContext context)
    {
        return new Symbol(VariableType.Float, float.Parse(context.FLOAT().GetText(), CultureInfo.InvariantCulture));
    }

    public override object VisitCharConstant(ArrayPPParser.CharConstantContext context)
    {
        var ch = context.CHAR().GetText();
        ch = ch.Remove(ch.Length - 1);
        ch = ch.Remove(0, 1);
        return new Symbol(VariableType.Char, ch);
    }

    public override object VisitStringConstant(ArrayPPParser.StringConstantContext context)
    {
        var str = context.STRING().GetText();
        str = str.Remove(str.Length - 1);
        str = str.Remove(0, 1);
        return new Symbol(VariableType.String, str);
    }
    #endregion

    public override object VisitIdentifierExpression(ArrayPPParser.IdentifierExpressionContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var symbol = GetSymbol(varName);
        
        if (symbol is null)
            throw new ArgumentException($"Variable '{varName}' does not exist");
        
        return symbol;
    }

    #region IO
    public override object VisitPrintToScreen(ArrayPPParser.PrintToScreenContext context)
    {
        var symbol = Visit(context.expression()) as Symbol;
        Console.WriteLine(symbol?.Value);
        return null!;
    }

    public override object VisitReadFromFile([NotNull] ArrayPPParser.ReadFromFileContext context)
    {
        var fileArgument = Visit(context.expression()) as Symbol;
        var fileName = fileArgument?.GetValue<string>();
        
        if (fileName is null) throw new ArgumentException("fileName must be a string.");
        return new Symbol(VariableType.String, File.ReadAllText(fileName));
    }
    #endregion

    #region Operator Expressions
    public override object VisitCompareExpression(ArrayPPParser.CompareExpressionContext context)
    {
        return _operationVisitor.VisitCompareExpression(context);
    }
    
    public override object VisitBoolExpression(ArrayPPParser.BoolExpressionContext context)
    {
        return _operationVisitor.VisitBoolExpression(context);
    }

    public override object VisitAddExpression([NotNull] ArrayPPParser.AddExpressionContext context)
    {
        return _operationVisitor.VisitAddExpression(context);
    }

    public override object VisitMultExpression([NotNull] ArrayPPParser.MultExpressionContext context)
    {
        return _operationVisitor.VisitMultExpression(context);
    }
    #endregion

    public override object VisitSubstringExpression(ArrayPPParser.SubstringExpressionContext context)
    {
        var val = Visit(context.substring().expression(0)) as Symbol;
        var count = Visit(context.substring().expression(1)) as Symbol;
        
        if (val is null || val.Type != VariableType.String)
            throw new ArgumentException("Argument at position 1 must be String.");
        
        if (count is null || count.Type != VariableType.Int)
            throw new ArgumentException("Argument at position 2 must be Int.");

        var substring = val.GetValue<string>().Substring(0, count.GetValue<int>());

        return new Symbol(VariableType.String, substring);
    }

    public override object VisitSplit([NotNull] ArrayPPParser.SplitContext context)
    {
        Symbol text = null;
        try{
            text = Visit(context.functionCall()) as Symbol;
        }
        catch{
            var ind = FindSymbol(context.IDENTIFIER().GetText());
            if(ind != -1)
            {
                text = _symbolTables[ind].Get(context.IDENTIFIER().GetText()) as Symbol;
            }
            else{
                throw new ArgumentException($"variable '{context.IDENTIFIER().GetText()} not found");
            }
        }
        Symbol delimiter = Visit(context.expression()) as Symbol;
        try{
            string[] line = text.Value.ToString().Split(delimiter.Value.ToString());
            List<Symbol> sym = new List<Symbol>();
            foreach(string l in line)
            {
                sym.Add(new Symbol(VariableType.String, l));
            }
            ArraySymbol ar = new ArraySymbol(VariableType.String, sym);
            return new Symbol(VariableType.Array, ar);
        }
        catch{
            throw new ArgumentException("Impossible to split");
        }
    }
    
    #region Codeflow
    public override object VisitIfBlock([NotNull] ArrayPPParser.IfBlockContext context) {
        // Get the left and right expressions from the context
        var exp = Visit(context.expression()) as Symbol;
        if(exp.Value.ToString() == "True" && context.block() != null)
        {
            _symbolTables.Add(new SymbolTable());
            VisitBlock(context.block());
            _symbolTables.RemoveAt(_symbolTables.Count - 1);
        }
        else if(context.elseIfBlock() != null){
            VisitElseIfBlock(context.elseIfBlock());
        }
        return null;
    }

    public override object VisitElseIfBlock([NotNull] ArrayPPParser.ElseIfBlockContext context) {
        // Get the left and right expressions from the context
        if(context.ifBlock() != null)
        {
            VisitIfBlock(context.ifBlock());
        }
        else if(context.block() != null)
        {
            _symbolTables.Add(new SymbolTable());
            VisitBlock(context.block());
            _symbolTables.RemoveAt(_symbolTables.Count - 1);
        }
        return null;
    }
    
    public override object VisitForCycle([NotNull] ArrayPPParser.ForCycleContext context) {
        _symbolTables.Add(new SymbolTable());
        try{
            Visit(context.variableDeclaration());
        }
        catch{
            Visit(context.assignment(0));
        }
        var exp = Visit(context.expression()) as Symbol;
        
        while(exp.Value.ToString() == "True")
        {
            _symbolTables.Add(new SymbolTable());
            VisitBlock(context.block());
            _symbolTables.RemoveAt(_symbolTables.Count - 1);
            try{
                Visit(context.assignment(1));
            }
            catch{
                Visit(context.assignment(0));
            }
            exp = Visit(context.expression()) as Symbol;
        }
        _symbolTables.RemoveAt(_symbolTables.Count - 1);
        return null;
    }

    public override object VisitWhileCycle([NotNull] ArrayPPParser.WhileCycleContext context) {
        
        _symbolTables.Add(new SymbolTable());
        var exp = Visit(context.expression()) as Symbol;
        
        while(exp.Value.ToString() == "True")
        {
            _symbolTables.Add(new SymbolTable());
            VisitBlock(context.block());
            _symbolTables.RemoveAt(_symbolTables.Count - 1);
            exp = Visit(context.expression()) as Symbol; 
        }
        _symbolTables.RemoveAt(_symbolTables.Count - 1);
        return null;
    }
    public override object VisitBlock([NotNull] ArrayPPParser.BlockContext context) {
            var count = context.line().Length;
            for (var i = 0; i < count; i++)
            {
                var value = Visit(context.line(i));
                if (_returnValue is not null)
                {
                    break;
                }
            }
            return null!;
    }
    #endregion
}