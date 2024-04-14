
using Antlr4.Runtime;
namespace ArrayPP.errorListener;
public class ArrayPPErrorListener : BaseErrorListener
{
    private String errorMsg = null;
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        errorMsg = "Syntax error at line " + line + ", position " + charPositionInLine + ": " + msg;
    }
    public bool isHasSyntaxError() {
        return errorMsg != null;
    }
    public String getErrorMsg() {
        return errorMsg;
    }
}

