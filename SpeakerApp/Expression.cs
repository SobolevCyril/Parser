using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Roslesinforg.Sigma.SrzParser;
using Antlr4.Runtime.Misc;

namespace SpeakerApp
{

 
    public class SrzFunction
    {
        // Проверка, что хотя бы один экземпляр макета удовлетворяет заданному условию
        public static object Any(string maket, bool condition)
        {
            return condition ? 1 :0;
        }
    }
    
    public class Expression : SRZBaseVisitor<object>
    {
        public List<String> Lines = new List<String>();
        //public List<String> FieldValues = new List<string>();
        private Dictionary<string, object> DataRepository = new Dictionary<string, object>();
        private Dictionary<string, object> FuncRepository = new Dictionary<string, object>();
        
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
            var left = Int32.Parse(this.Visit(context.left).ToString());
            var right = Int32.Parse(this.Visit(context.right).ToString());
            return eval(left, context.op.Type, right);
        }

        public override object VisitOpExpComp(SRZParser.OpExpCompContext context)
        {
            var left = Decimal.Parse(this.Visit(context.left).ToString());
            var right = Decimal.Parse(this.Visit(context.right).ToString());
            switch (context.op.Type)
            {
                case SRZParser.EQU: return left == right;
                case SRZParser.NOTEQU: return left != right;
                case SRZParser.LT: return left < right;
                case SRZParser.LE: return left <= right;
                case SRZParser.GE: return left >= right;
                case SRZParser.GT: return left > right;
            }
            throw new Exception(String.Format("Неизвестная логическая операция: {0}", context.GetText()));
        }

        public override object VisitOpExpBool(SRZParser.OpExpBoolContext context)
        {
            Boolean left = (Boolean)this.Visit(context.left);
            Boolean right = (Boolean)this.Visit(context.right);
            switch (context.op.Type)
            {
                case SRZParser.OR: return left || right;
                case SRZParser.AND: return left && right;
            }
            throw new Exception(String.Format("Неизвестная логическая операция: {0}", context.GetText()));
        }
        
        // Сравнение значения слева с множеством справа
        public override object VisitOpSetComp(SRZParser.OpSetCompContext context)
        {
			int value  = Convert.ToInt32(this.Visit(context.left));
            List<Range<int>> ranges = (List<Range<int>>)this.Visit(context.right);
			bool isValueInSet = (ranges.Any(r => r.ContainsValue(value)));
            switch (context.op.Type)
            {
                case SRZParser.EQU: return isValueInSet;
                case SRZParser.NOTEQU: return !isValueInSet; 
            }
            throw new Exception(String.Format("Неизвестная логическая операция: {0}", context.GetText()));
        }

        public override object VisitStart(SRZParser.StartContext context)
        {
            //инициализируем переменные для проверки,по идее тут должен быть репозиторий
            // с доступом к полям модели выдела
            DataRepository.Add("m0001", 5);
            DataRepository.Add("m1001", 3);
            DataRepository.Add("m1002", 3);
            DataRepository.Add("m1003", 3);
            DataRepository.Add("m1004", 3);
            DataRepository.Add("m1005", 3);
            DataRepository.Add("m1006", 3);
            DataRepository.Add("m1007", 3);
            DataRepository.Add("m1008", 3);
            DataRepository.Add("m1009", 3);
            DataRepository.Add("m1010", 3);
            DataRepository.Add("m1011", 3);

            var res = this.Visit(context.expression());
            return Convert.ToBoolean(res);
        }
            
        public override object VisitVarExp(SRZParser.VarExpContext context)
        {
            object val;
            if (!DataRepository.TryGetValue(context.GetText().ToLowerInvariant(), out val))
                //throw new ArgumentException(String.Format("Имя переменной отсутствует в словаре системы: {0}", context.GetText()));
                val = 1;
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
            return this.Visit(context.literal());
        }



        /// <summary>
        /// Visit a parse tree produced by the <c>funcExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitFuncExp(SRZParser.FuncExpContext context)
        {
            return this.Visit(context.function());

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
        /// Visit a parse tree produced by <see cref="SRZParser.params"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitParams(SRZParser.ParamsContext context)
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
            return new Range<int>()
                { Minimum = Int32.Parse(context.@from.Text),
                  Maximum = Int32.Parse(context.to.Text)
                };
        }
        /// <summary>
        /// Visit a parse tree produced by <see cref="SRZParser.set"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitSet(SRZParser.SetContext context)
        {
			// Проверяем, из чего состоит множестово, для этого рассматриваем отдельно литералы и множества
			foreach (var elem in context.literal()) { Visit(elem)   };
			//return VisitChildren(context.range);
			//return ;
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

        public override object VisitFnAny([NotNull] SRZParser.FnAnyContext context)
        {
            return SrzFunction.Any(context.mak.Text, (bool)this.Visit(context.cond));
        }

    }
}
