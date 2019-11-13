using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslesinforg.Sigma.SrzParser;

namespace SpeakerApp
{
    public class Expression : SRZBaseVisitor<object>
    {
        public List<String> Lines = new List<String>();
        //public List<String> FieldValues = new List<string>();
        private Dictionary<string, object> DataRepository = new Dictionary<string, object>();
        
        private int eval(int left, int op, int right)
        {
            switch (op)
            {
                case SRZParser.MUL: return left * right;
                case SRZParser.DIV: return left / right;
                case SRZParser.ADD: return left + right;
                case SRZParser.SUB: return left - right;
            }
            throw new Exception(String.Format("Неизвестная операция: {0}", SRZLexer.DefaultVocabulary.GetSymbolicName(op)));
        }

        public override object VisitOpExp(SRZParser.OpExpContext context)
        {
            var left = Int32.Parse(this.VisitChildren(context.left).ToString());
            var right = Int32.Parse(this.VisitChildren(context.right).ToString());
            return eval(left, context.op.Type, right);
        }

        public override object VisitOpExpBool(SRZParser.OpExpBoolContext context)
        {
            var left = Int32.Parse(this.Visit(context.left).ToString());
            var right = Int32.Parse(this.Visit(context.right).ToString());
            switch (context.op.Type)
            {
                case SRZParser.EQU: return left == right;
                case SRZParser.NOTEQU: return left != right;
                case SRZParser.LT: return left < right;
                case SRZParser.LE: return left <= right;
                case SRZParser.GE: return left >= right;
                case SRZParser.GT: return left > right;
                case SRZParser.OR: return Convert.ToBoolean(left) || Convert.ToBoolean(right);
                case SRZParser.AND: return Convert.ToBoolean(left) && Convert.ToBoolean(right);
            }
            throw new Exception(String.Format("Неизвестная логическая операция: {0}", context.GetText()));
        }

        public override object VisitStart(SRZParser.StartContext context)
        {
            //инициализируем переменные для проверки,по идее тут должен быть репозиторий
            // с доступом к полям модели выдела
            DataRepository.Add("M0001", 5);
            var res = this.Visit(context.expression());
            return Convert.ToBoolean(res);
        }
            
        public override object VisitVarExp(SRZParser.VarExpContext context)
        {
            object val;
            if (!DataRepository.TryGetValue(context.GetText(), out val))
                throw new ArgumentException(String.Format("Имя переменной отсутствует в словаре системы: {0}", context.GetText()));

            return val; 
        }

        /// <summary>
        /// Visit a parse tree produced by the <c>literalExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitLiteralExp(SRZParser.LiteralExpContext context)
        {
            return this.VisitChildren(context);
        }



        /// <summary>
        /// Visit a parse tree produced by the <c>funcExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitFuncExp(SRZParser.FuncExpContext context)
        {
            return null;

        }

        /// <summary>
        /// Visit a parse tree produced by the <c>parenthesisExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitParenthesisExp(SRZParser.ParenthesisExpContext context)
        {
            return this.Visit(context.expression());
        }

        /// <summary>
        /// Visit a parse tree produced by the <c>setExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitSetExp(SRZParser.SetExpContext context)
        {
            return null;
        }

 
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.params"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitParams(SRZParser.ParamsContext context)
        {
            return null;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.function"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object  VisitFunction(SRZParser.FunctionContext context)
        {
            return null;
        }
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.variable"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitVariable(SRZParser.VariableContext context)
        {
            object variable;  
            if (!DataRepository.TryGetValue(context.GetText(), out variable)) 
            throw new ArgumentException(String.Format("Имя переменной отсутствует в словаре системы: {0}", context.GetText()));
            return variable;
        }
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.range"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitRange(SRZParser.RangeContext context)
        {
            return null;
        }
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.set"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitSet(SRZParser.SetContext context)
        {
            return null;
        }
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.literal"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object  VisitLiteral( SRZParser.LiteralContext context)
        {
            switch (context.typ.Type)
            {
                case SRZParser.INT:
                {
                    return Int32.Parse(context.GetText());
                }
                case SRZParser.NUMBER:
                {
                    return Decimal.Parse(context.GetText());
                }
                case SRZParser.STRING:
                {
                    return context.GetText();
                }
            }
            throw new Exception("Неизвестный тип данных");
        }
    }
}
