using Antlr4.Runtime;
using ArrayPP;
using ArrayPP.Structures;
using ArrayPP.Visitors;
using ArrayPP.errorListener;

namespace ArrayPP;

public static class Program
{
    public static int Main(string[] args)
    {
        var isInteractive = false;
        string? fileName = "file";

        // Run the program
        if (isInteractive) RunInteractive();
        else if (fileName != null) RunFile(fileName);
        
        return 0;
    }
    private static void RunFile(string fileName)
    {
        try{
            Console.WriteLine("Running file: \"" + fileName + "\"");
            var symbolTable = new List<SymbolTable> {new()};
            ExecuteCode(symbolTable, CharStreams.fromPath(fileName));
        }
        catch (Exception e) { 
            Console.WriteLine(e.Message);
        }
    }

    private static void ExecuteCode(List<SymbolTable> symbolTables, ICharStream openRead)
    {
        var lexer = new ArrayPPLexer(openRead);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ArrayPPParser(tokens);
        parser.RemoveErrorListeners();
        ArrayPPErrorListener errorListener = new ArrayPPErrorListener();
        parser.AddErrorListener(errorListener);
        var tree = parser.program();
        if (errorListener.isHasSyntaxError()) {
             throw new Exception(errorListener.getErrorMsg());
        }
        var interpreter = new InterpreterVisitor(symbolTables);
        interpreter.Visit(tree);   
    }
        

    private static void RunInteractive()
    {
        var symbolTables = new List<SymbolTable> {new()};
        var shell = new InterpreterShell();
        while (true)
        {
            try{
                shell.PrintInput();
                var input = shell.ReadInput();
                if (input == "exit") break;
                ExecuteCode(symbolTables, CharStreams.fromString(input));
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}