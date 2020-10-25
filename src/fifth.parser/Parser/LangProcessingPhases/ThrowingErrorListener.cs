using System;
using System.IO;
using Antlr4.Runtime;

namespace Fifth.Parser
{
    public class ThrowingErrorListener<TResult> : IAntlrErrorListener<TResult>
    {
        #region IAntlrErrorListener<TSymbol> Implementation
        public void SyntaxError(TextWriter output, IRecognizer recognizer, TResult offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) => throw new Exception($"line {line}:{charPositionInLine} {msg}");
        #endregion
    }
}
