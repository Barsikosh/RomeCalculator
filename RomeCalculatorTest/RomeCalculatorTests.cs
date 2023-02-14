using RomeCalculator.Calculator;
using RomeCalculator.Exceptions;

namespace RomeCalculatorTest;

public class RomeCalculatorTests
{
    private readonly RomanCalculator _romanCalculator = new();
    
    [Theory]
    [InlineData("(MMMDCCXXIV - MMCCXXIX) * II", "MMCMXC")]
    [InlineData("(MDCCXXIV + IV) * II", "MMMCDLVI")]
    [InlineData("I + IV * II", "IX")]
    [InlineData("  xX  - Iv *  II   ", "XII")]
    [InlineData("m *   iI   ", "MM")]
    [InlineData("II", "II")]
    public void ExpressionSuccessTest(string input, string expectedResult)
    {
        var result = _romanCalculator.Evaluate(input);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("(MMMMMDCCXXIV - MMCCXXIX) * II")]
    [InlineData("MMMCMXCIX + I")]
    [InlineData("I - V")]
    public void RomeLimitExceeded_Throws(string input)
    {
        var exception = Assert.Throws<CalculateException>(() => _romanCalculator.Evaluate(input));
        Assert.Equal(CalculateExceptionMessages.RomeNumberExceptionMessage, exception.Message);
    }
    
    [Theory]
    [InlineData("(MMMMMDCCXXIV - - MMCCXXIX) * II")]
    [InlineData("5 + 10")]
    [InlineData("a")]
    public void InvalidInputFormat_Throws(string input)
    {
        var exception = Assert.Throws<CalculateException>(() => _romanCalculator.Evaluate(input));
        Assert.Equal(CalculateExceptionMessages.InvalidFormatExceptionMessage, exception.Message);
    }
}