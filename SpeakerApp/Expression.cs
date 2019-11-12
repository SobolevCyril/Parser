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
            throw new Exception("Неизвестная операция");
        }

        public override object VisitOpExp(SRZParser.OpExpContext context)
        {
            var left = Int32.Parse(this.VisitChildren(context.left).ToString());
            var right = Int32.Parse(this.VisitChildren(context.right).ToString());
            return eval(left, context.op.TokenIndex, right);
        }


        public override object VisitStart(SRZParser.StartContext context)
        {
            //инициализируем переменные для проверки,по идее тут должен быть репозиторий
            // с доступом к полям модели выдела
            DataRepository.Add("M0001", 5);

            return Boolean.Parse(this.VisitChildren(context).ToString());
            //var sb = new StringBuilder();
            //var exp = context;
            //var line = exp.GetText();
            //Lines.Add(line);
            //return line;
        }
            
        public override object VisitVarExp(SRZParser.VarExpContext context)
        {
            var v = context.variable();
            // выполняем извлечение значения из словаря значений по имени поля
            //var value = ValueByVarName(v.GetText());
            return v.GetText();
        }

        /// <summary>
        /// Visit a parse tree produced by the <c>literalExp</c>
        /// labeled alternative in <see cref="SRZParser.expression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override object VisitLiteralExp(SRZParser.LiteralExpContext context)
        {
            return context.literal().ToString();
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
            switch (context.typ.TokenIndex)
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
