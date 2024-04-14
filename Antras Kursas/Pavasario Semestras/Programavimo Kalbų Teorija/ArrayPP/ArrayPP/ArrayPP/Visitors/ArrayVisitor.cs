using System.Data;
using Antlr4.Runtime.Misc;
using ArrayPP;
using ArrayPP.Structures;

namespace ArrayPP.Visitors;

public class ArrayVisitor : ArrayPPBaseVisitor<object>
{
    private readonly InterpreterVisitor _parent;

    public ArrayVisitor(InterpreterVisitor parent)
    {
        _parent = parent;
    }

    public override object VisitArrayExpression(ArrayPPParser.ArrayExpressionContext context)
    {
        if (context.arrayLiteral().GetText() == "[]")
        {
            return new Symbol(VariableType.Array, new ArraySymbol(VariableType.Void));
        }

        var arrayLiteral = context.arrayLiteral();
        var count = arrayLiteral.expressionList().expression().Length;
        
        var list = new List<Symbol>();
        var type = VariableType.Void; 
        for (var i = 0; i < count; i++)
        {
            var expression = arrayLiteral.expressionList().expression(i);

            if (_parent.Visit(expression) is not Symbol symbol)
            {
                throw new EvaluateException($"Could not evaluate '{expression.GetText()}' in array expression.");
            }
            
            if (i == 0) type = symbol.Type;
            else if (type != symbol.Type)
                throw new ArrayTypeMismatchException($"Cannot insert type {symbol.Type.ToString()} into array of type {type.ToString()}.");

            list.Add(symbol);
        }

        return new Symbol(VariableType.Array, new ArraySymbol(type, list));
    }

    public override object VisitIndexAccess(ArrayPPParser.IndexAccessContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();

        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");

        var index = ParseIndex(_parent.Visit(context.expression()));
        var arraySymbol = symbol.GetValue<ArraySymbol>();
        if (index < 0)
        {
            index = index > 0 ? index : -index;
            if (index > arraySymbol.List.Count)
            {
                throw new Exception($"Index '{index}' bigger than array size");
            }
            int c = arraySymbol.List.Count;
            c = c - index;
            c++;
            index = c;
        }
        switch (symbol.Type)
        {
            case VariableType.Array:
                return arraySymbol.List[index];
            case VariableType.String:
                var str = symbol.GetValue<string>();
                return str[index];
            default:
                return null!;
        }
    }

    public override object VisitArrayRemoval(ArrayPPParser.ArrayRemovalContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();

        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index removal can only be used with array types.");

        var index = ParseIndex(_parent.Visit(context.expression()));

        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        if (index < 0)
        {
            index = index > 0 ? index : -index;
            if (index > arraySymbol.List.Count)
            {
                throw new Exception($"Index '{index}' bigger than array size");
            }
            int c = arraySymbol.List.Count;
            c = c - index;
            //c++;
            arraySymbol.List.RemoveAt(c);
        }
        else
        {
            arraySymbol.List.RemoveAt(index);
        }

        return null!;
    }

    public override object VisitArrayAssignment(ArrayPPParser.ArrayAssignmentContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();

        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index assignment can only be used with array types.");

        var index = ParseIndex(_parent.Visit(context.expression(0)));

        var valueExpression = _parent.Visit(context.expression(1));
        if (valueExpression is not Symbol valueSymbol)
            throw new Exception("Expression cannot be assigned.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        if (arraySymbol.Type != valueSymbol.Type)
            throw new ArrayTypeMismatchException();
        
        if (index < 0)
        {
            index = index > 0 ? index : -index;
            if (index > arraySymbol.List.Count)
            {
                throw new Exception($"Index '{index}' bigger than array size");
            }
            int c = arraySymbol.List.Count;
            c = c - index;
            c++;
            arraySymbol.List[c] = valueSymbol;
        }
        else
        {
            arraySymbol.List[index] = valueSymbol;
        }

        return null!;
    }
    public override object VisitArrayInsert(ArrayPPParser.ArrayInsertContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index removal can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        
        var index = ParseIndex(_parent.Visit(context.expression(0)));
        
        var valueExpression = _parent.Visit(context.expression(1));
        if (valueExpression is not Symbol valueSymbol)
            throw new Exception("Expression cannot be parsed.");

        if (valueSymbol.Type != arraySymbol.Type)
            throw new ArrayTypeMismatchException(
                $"Cannot insert value of type '{valueSymbol.Type.ToString()}' into '{arraySymbol.Type.ToString()}' array.");
        
        if (index < 0)
        {
            index = index > 0 ? index : -index;
            if (index > arraySymbol.List.Count)
            {
                throw new Exception($"Index '{index}' bigger than array size");
            }
            int c = arraySymbol.List.Count;
            c = c - index;
            c++;
            arraySymbol.List.Insert(c, valueSymbol);
        }
        else
        {
            arraySymbol.List.Insert(index, valueSymbol);
        }
        return null!;
    }

    public override object VisitArrayLength(ArrayPPParser.ArrayLengthContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("length can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        return new Symbol(VariableType.Int, arraySymbol.List.Count);
    }

    public override object VisitArrayRandomiser(ArrayPPParser.ArrayRandomiserContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("length can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");
        
        arraySymbol.List = arraySymbol.List.OrderBy(a => Guid.NewGuid()).ToList();
        return null!;
    }
    
    public override object VisitArrayFill(ArrayPPParser.ArrayFillContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index removal can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        
        var index = ParseIndex(_parent.Visit(context.expression(0)));
        
        var valueExpression = _parent.Visit(context.expression(1));
        if (valueExpression is not Symbol valueSymbol)
            throw new Exception("Expression cannot be parsed.");

        if (valueSymbol.Type != arraySymbol.Type)
            throw new ArrayTypeMismatchException(
                $"Cannot insert value of type '{valueSymbol.Type.ToString()}' into '{arraySymbol.Type.ToString()}' array.");

        arraySymbol.List = Enumerable.Repeat(valueSymbol, index).ToList();
        return null!;
    }
    public override object VisitArrayFilter(ArrayPPParser.ArrayFilterContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index removal can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        
        var from = ParseIndex(_parent.Visit(context.expression(0)));
        
        var to = ParseIndex(_parent.Visit(context.expression(1)));
        
        for(int i = 0; i < arraySymbol.List.Count; i++)
        {
            var item = ParseIndex(arraySymbol.List[i]);
            if (item >= from && item <= to)
            {
                arraySymbol.List.RemoveAt(i);
                i--;
            }
        }
        return null!;
    }
    public override object VisitArraySlice(ArrayPPParser.ArraySliceContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();
        
        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("Index removal can only be used with array types.");
        
        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        
        var to = ParseIndex(_parent.Visit(context.expression(0)));
        
        var from = ParseIndex(_parent.Visit(context.expression(1)));

        arraySymbol.List = arraySymbol.List.GetRange(to, from);
        return null!;
    }

    public override object VisitArrayUnique(ArrayPPParser.ArrayUniqueContext context)
    {
        var arraySymbolName = context.IDENTIFIER().GetText();

        var symbol = _parent.GetSymbol(arraySymbolName);
        if (symbol is null)
            throw new Exception($"Variable '{arraySymbolName}' does not exist");
        if (symbol.Type != VariableType.Array)
            throw new Exception("length can only be used with array types.");

        if (symbol.Value is not ArraySymbol arraySymbol)
            throw new Exception("Array cannot be null.");

        arraySymbol.List = arraySymbol.List.Distinct().ToList();
        return null!;
    }
    

    private static int ParseIndex(object? expression)
    {
        if (expression is not Symbol indexSymbol)
            throw new Exception("Cannot get index expression result.");
        if (indexSymbol.Type != VariableType.Int)
            throw new ArgumentException("Index must be an integer.");
        if (indexSymbol.Value is not int index)
            throw new Exception("Index cannot be null.");
        return index;
    }
}