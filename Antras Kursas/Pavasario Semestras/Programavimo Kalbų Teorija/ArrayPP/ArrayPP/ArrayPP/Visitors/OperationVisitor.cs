using Antlr4.Runtime.Misc;
using ArrayPP;
using ArrayPP.Structures;

namespace ArrayPP.Visitors;

public class OperationVisitor : ArrayPPBaseVisitor<object>
{
    private readonly InterpreterVisitor _parent;
    
    public OperationVisitor(InterpreterVisitor parent)
    {
        _parent = parent;
    }
    
    public override object VisitBoolExpression(ArrayPPParser.BoolExpressionContext context)
    {
        var symbol1 = _parent.Visit(context.expression(0)) as Symbol;
        var symbol2 = _parent.Visit(context.expression(1)) as Symbol;
        var op = context.boolOp().GetText() ?? throw new Exception("Operation cannot be null.");
        
        if (symbol1 is null || symbol2 is null)
            throw new Exception("Symbols cannot be null in operation.");

        if (symbol1.Type == VariableType.Bool && symbol2.Type == VariableType.Bool)
        {
            var v1 = symbol1.GetValue<bool>();
            var v2 = symbol2.GetValue<bool>();
            
            return new Symbol(VariableType.Bool, op switch
            {
                "&&" => v1 && v2,
                "||" => v1 || v2,
                _ => throw new InvalidOperationException($"Unsupported operation {op}.")
            });
        }
        
        throw new ArgumentException($"Operation {op} not supported between types '{symbol1.Type}' and '{symbol2.Type}'");
    }
    
    public override object VisitAddExpression([NotNull] ArrayPPParser.AddExpressionContext context)
    {
        var symbol1 = _parent.Visit(context.expression(0)) as Symbol;
        var symbol2 = _parent.Visit(context.expression(1)) as Symbol;
        var op = context.addOp().GetText() ?? throw new Exception("Operation cannot be null.");

        if (symbol1 is null || symbol2 is null)
            throw new Exception("Symbols cannot be null in operation.");
        
        if(LanguageUtils.IsNumeric(symbol1) && LanguageUtils.IsNumeric(symbol2))
        {
            if (symbol1.Type == VariableType.Int && symbol2.Type == VariableType.Int)
            {
                var v1 = symbol1.GetValue<int>();
                var v2 = symbol2.GetValue<int>();

                return op switch
                {
                    "+" => new Symbol(VariableType.Int, v1 + v2),
                    "-" => new Symbol(VariableType.Int, v1 - v2),
                    _ => throw new InvalidOperationException($"Unsupported operation {op}.")
                };
            }

            var f1 = LanguageUtils.ParseFloat(symbol1);
            var f2 = LanguageUtils.ParseFloat(symbol2);

            return op switch
            {
                "+" => new Symbol(VariableType.Float, f1 + f2),
                "-" => new Symbol(VariableType.Float, f1 - f2),
                _ => throw new InvalidOperationException($"Unsupported operation {op}.")
            };
        }

        if (LanguageUtils.IsTextual(symbol1) && LanguageUtils.IsTextual(symbol2))
        {
            var v1 = symbol1.GetValue<string>();
            var v2 = symbol2.GetValue<string>();

            if (op == "+")
            {
                return new Symbol(VariableType.String, v1 + v2);
            }
        }

        throw new ArgumentException($"Operation {op} not supported between types '{symbol1.Type}' and '{symbol2.Type}'");
    }
    
    public override object VisitMultExpression([NotNull] ArrayPPParser.MultExpressionContext context)
    {
        var symbol1 = _parent.Visit(context.expression(0)) as Symbol;
        var symbol2 = _parent.Visit(context.expression(1)) as Symbol;
        var op = context.multOp().GetText() ?? throw new Exception("Operation cannot be null.");
        
        if (symbol1 is null || symbol2 is null)
            throw new Exception("Symbols cannot be null in operation.");
        
        if(LanguageUtils.IsNumeric(symbol1) && LanguageUtils.IsNumeric(symbol2))
        {
            if (symbol1.Type == VariableType.Int && symbol2.Type == VariableType.Int)
            {
                var v1 = symbol1.GetValue<int>();
                var v2 = symbol2.GetValue<int>();

                return op switch
                {
                    "*" => new Symbol(VariableType.Int, v1 * v2),
                    "/" => new Symbol(VariableType.Int, v1 / v2),
                    _ => throw new InvalidOperationException($"Unsupported operation {op}.")
                };
            }

            var f1 = LanguageUtils.ParseFloat(symbol1);
            var f2 = LanguageUtils.ParseFloat(symbol2);

            return op switch
            {
                "*" => new Symbol(VariableType.Float, f1 * f2),
                "/" => new Symbol(VariableType.Float, f1 / f2),
                _ => throw new InvalidOperationException($"Unsupported operation {op}.")
            };
        } 
         
        throw new ArgumentException($"Operation {op} not supported between types '{symbol1.Type}' and '{symbol2.Type}'");
    }
    
    public override object VisitCompareExpression(ArrayPPParser.CompareExpressionContext context)
    {
        var symbol1 = _parent.Visit(context.expression(0)) as Symbol;
        var symbol2 = _parent.Visit(context.expression(1)) as Symbol;
        var op = context.compareOp().GetText();
        
        if (symbol1 is null || symbol2 is null)
            throw new Exception("Symbols cannot be null in operation.");

        if (LanguageUtils.IsNumeric(symbol1) && LanguageUtils.IsNumeric(symbol2))
        {
            var f1 = LanguageUtils.ParseFloat(symbol1);
            var f2 = LanguageUtils.ParseFloat(symbol2);
            
            return new Symbol(VariableType.Bool, op switch
            {
                ">" => f1 > f2,
                "<" => f1 < f2,
                "<=" => f1 <= f2,
                ">=" => f1 >= f2,
                "==" => Math.Abs(f1 - f2) < LanguageUtils.Epsilon,
                "!=" => Math.Abs(f1 - f2) > LanguageUtils.Epsilon,
                _ => throw new InvalidOperationException($"Unsupported operation {op}.")
            });
        }

        if (symbol1.Type == symbol2.Type)
        {
            var type = symbol1.Type;
            var equals = type switch
            {
                VariableType.String => symbol1.GetValue<string>().Equals(symbol2.GetValue<string>()),
                VariableType.Bool => symbol1.GetValue<bool>().Equals(symbol2.GetValue<bool>()),
                VariableType.Char => symbol1.GetValue<char>().Equals(symbol2.GetValue<char>()),
                VariableType.Array => symbol1 == symbol2,
                _ => throw new InvalidOperationException(
                    $"Invalid comparison between types '{symbol1.Type.ToString()}' and '{symbol2.Type.ToString()}'.")
            };
            
            return new Symbol(VariableType.Bool, op switch
            {
                "==" => equals,
                "!=" => !equals,
                _ => throw new InvalidOperationException($"Unsupported operation {op}.")
            });
        }
        
        throw new ArgumentException($"Operation {op} not supported between types '{symbol1.Type}' and '{symbol2.Type}'");
    }
}