using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime;
using System.Runtime.Serialization;

namespace SpeakerApp
{

    [Serializable]
    public class ParseCancellationException : ApplicationException
    {
        public ParseCancellationException() { }
        public ParseCancellationException(string message) : base(message) { }
        public ParseCancellationException(string message, Exception inner) : base(message, inner) { }
        protected ParseCancellationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class ErrorListener : BaseErrorListener
    {
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ParseCancellationException("строка " + line + ":" + charPositionInLine /*+ " " + msg*/);
        }
    }
}
