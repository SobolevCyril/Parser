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
			// Пока вставлена заглушка
			return condition ? 1 :0;
        }
    }
    
    public class Expression : SRZBaseVisitor<object>
    {
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
			var value  = Convert.ToDecimal(this.Visit(context.left));
            var set = (SrzSet)this.Visit(context.right);
            switch (context.op.Type)
            {
                case SRZParser.EQU: return set.In(value);
                case SRZParser.NOTEQU: return !set.In(value); 
            }
            throw new Exception(String.Format("Неизвестная логическая операция: {0}", context.GetText()));
        }

        public override object VisitStart(SRZParser.StartContext context)
        {
			//инициализируем переменные для проверки,по идее тут должен быть репозиторий
			// с доступом к полям модели выдела
			TestData.Fill(DataRepository);
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

        public override object VisitFuncExp(SRZParser.FuncExpContext context)
        {
            return this.Visit(context.function());
        }

        public override object VisitParenthesisExp(SRZParser.ParenthesisExpContext context)
        {
            return this.Visit(context.expression());
        }

        public override object VisitVariable(SRZParser.VariableContext context)
        {
            object variable;  
            if (!DataRepository.TryGetValue(context.GetText(), out variable)) 
            throw new ArgumentException(String.Format("Имя переменной отсутствует в словаре системы: {0}", context.GetText()));
            return variable;
        }

		public override object VisitRange(SRZParser.RangeContext context)
        {
            return new SrzRange<decimal>()
                { Minimum = Decimal.Parse(context.@from.Text),
                  Maximum = Decimal.Parse(context.to.Text)
                };
        }

		public override object VisitSet(SRZParser.SetContext context)
        {
			var set = new SrzSet();
			// Проверяем, из чего состоит множестово, для этого рассматриваем отдельно целые числа, дробные и диапазоны
			foreach (var elem in context.INT())
			{
				Int32 set_elem = Convert.ToInt32(elem.GetText());
				set.Add(set_elem);

			};
			foreach (var elem in context.NUMBER())
			{
				var set_elem = Convert.ToDecimal(elem.GetText());
				set.Add(set_elem);
			};
			foreach (var elem in context.range())
			{
				var range = (SrzRange<decimal>)(Visit(elem));
				set.Add(range);
			};

			return set;
		}

		public override object  VisitConstant( SRZParser.ConstantContext context)
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
