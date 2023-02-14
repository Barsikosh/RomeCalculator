using RomeCalculator.Exceptions;
using RomeCalculator.Grammar;

namespace RomeCalculator.Calculator;

public class RomanCalculator : ICalulator<string, string>
{
    public string Evaluate(string input)
    {
        try
        {
            return RomanGrammar.ParseCondition(input).Compile()().ToString();
        }

        catch (Sprache.ParseException)
        {
            throw new CalculateException(CalculateExceptionMessages.InvalidFormatExceptionMessage);
        }

        catch (Exception e)
        {
            throw new CalculateException(e.Message);
        }
    }
}