//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from SRZ.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Roslesinforg.Sigma.SrzParser {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="SRZParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface ISRZVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart([NotNull] SRZParser.StartContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>varExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarExp([NotNull] SRZParser.VarExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>literalExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralExp([NotNull] SRZParser.LiteralExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>funcExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncExp([NotNull] SRZParser.FuncExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>parenthesisExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisExp([NotNull] SRZParser.ParenthesisExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>setExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSetExp([NotNull] SRZParser.SetExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>opExp</c>
	/// labeled alternative in <see cref="SRZParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpExp([NotNull] SRZParser.OpExpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams([NotNull] SRZParser.ParamsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction([NotNull] SRZParser.FunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] SRZParser.VariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.range"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRange([NotNull] SRZParser.RangeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.set"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSet([NotNull] SRZParser.SetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SRZParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] SRZParser.LiteralContext context);
}
} // namespace Roslesinforg.Sigma.SrzParser