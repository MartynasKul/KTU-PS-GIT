namespace ArrayPP;

public class InterpreterShell
{
    private int _indentLevel;
    private readonly Stack<char> _openBrackets = new();
    private string _buffer = string.Empty;

    public string ReadInput()
    {
        var input = Console.ReadLine() ?? string.Empty;

        foreach (var sym in input)
        {
            if ("[{(".Contains(sym))
            {
                _indentLevel++;
                _openBrackets.Push(sym);
            }
            else if ("]})".Contains(sym))
            {
                var opening = _openBrackets.Pop();
                if (MatchBrackets(opening, sym))
                    _indentLevel--;
                else 
                    throw new Exception($"Mismatched brackets. Tried to close {sym} when last open is {opening}");
            }
        }

        switch (_indentLevel)
        {
            case > 0:
                _buffer += input + "\n";
                return string.Empty;
            case 0 when _buffer.Length > 0:
            {
                var copy = _buffer + input;
                _buffer = string.Empty;
                return copy;
            }
            default:
                return input;
        }
    }

    public void PrintInput()
    {
        for (var i = 0; i < _indentLevel; i++)
        {
            Console.Write("... ");
        }
        
        if (_indentLevel == 0)
            Console.Write("> ");
    }

    private bool MatchBrackets(char opening, char closing)
    {
        return opening switch
        {
            '[' => closing == ']',
            '(' => closing == ')',
            '{' => closing == '}',
            _ => false
        };
    }
}