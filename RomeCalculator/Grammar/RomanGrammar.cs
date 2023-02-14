using System.Linq.Expressions;
using RomeCalculator.Models;
using Sprache;

namespace RomeCalculator.Grammar;

internal static class RomanGrammar
{
    public static Expression<Func<RomeNumber>> ParseCondition(string text)
        => Lambda.Parse(text);

    private static readonly Parser<RomeNumberEntity> INumber =
        Parse.IgnoreCase("i").Once().Return(new RomeNumberEntity("I"));

    private static readonly Parser<RomeNumberEntity> VNumber =
        Parse.IgnoreCase("V").Once().Return(new RomeNumberEntity("V"));

    private static readonly Parser<RomeNumberEntity> XNumber =
        Parse.IgnoreCase("X").Once().Return(new RomeNumberEntity("X"));

    private static readonly Parser<RomeNumberEntity> LNumber =
        Parse.IgnoreCase("L").Once().Return(new RomeNumberEntity("L"));

    private static readonly Parser<RomeNumberEntity> CNumber =
        Parse.IgnoreCase("C").Once().Return(new RomeNumberEntity("C"));

    private static readonly Parser<RomeNumberEntity> DNumber =
        Parse.IgnoreCase("D").Once().Return(new RomeNumberEntity("D"));

    private static readonly Parser<RomeNumberEntity> MNumber =
        Parse.IgnoreCase("M").Once().Return(new RomeNumberEntity("M"));

    private static readonly Parser<Expression> RomeNumberParser =
        from n in INumber
            .Or(VNumber).Or(XNumber).Or(LNumber)
            .Or(CNumber).Or(DNumber).Or(MNumber).Token()
            .AtLeastOnce()
        select Expression.Constant(new RomeNumber(n.ToArray()));

    private static Parser<Expression<Func<RomeNumber>>> Lambda =>
        PlusTerm.Select(body => Expression.Lambda<Func<RomeNumber>>(body));

    private static Parser<Expression> PlusTerm =>
        Parse.ChainOperator(PlusAndSubtractOp, MultiplyTerm, Expression.MakeBinary);

    private static readonly Parser<ExpressionType> PlusAndSubtractOp = MakeOperator("+", ExpressionType.Add)
        .Or(MakeOperator("-", ExpressionType.Subtract));

    static Parser<Expression> MultiplyTerm =>
        Parse.ChainOperator(OpMultiply, Factor, Expression.MakeBinary);

    private static readonly Parser<ExpressionType> OpMultiply = MakeOperator("*", ExpressionType.Multiply);

    private static Parser<Expression> Factor =>
        SubExpression
            .Or(RomeNumberParser);

    private static Parser<Expression> SubExpression =>
        from lparen in Parse.Char('(').Token()
        from expr in PlusTerm.Or(MultiplyTerm)
        from rparen in Parse.Char(')').Token()
        select expr;

    static Parser<ExpressionType> MakeOperator(string token, ExpressionType type)
        => Parse.IgnoreCase(token).Token().Return(type);
}