using RomeCalculator.Exceptions;
using RomeCalculator.Grammar;

namespace RomeCalculator.Calculator;

public class RomanCalculator : ICalulator<string, string>
{
    public string Evaluate(string input)
    {
        try
        {
            var da = RomanGrammar.ParseCondition(input);
            return da.Compile()().ToString();
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