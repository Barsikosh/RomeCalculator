using RomeCalculator.Exceptions;
using RomeCalculator.Models;

namespace RomeCalculator.Helpers;

internal static class IntegerExtension
{
    private static readonly string[] Ones = {"","I","II","III","IV","V","VI","VII","VIII","IX"};
    private static readonly string[] Tens = {"","X","XX","XXX","XL","L","LX","LXX","LXXX","XC"};
    private static readonly string[] Hrns = {"","C","CC","CCC","CD","D","DC","DCC","DCCC","CM"};
    private static readonly string[] Ths= {"","M","MM","MMM"};
    
    public static RomeNumber ToRoman(this int num)
    {
        if (num > 3999 || num < 1)
        {
            throw new ArgumentException(CalculateExceptionMessages.RomeNumberExceptionMessage);
        }
        
        var result = Ths[num / 1000] + Hrns[(num % 1000) / 100] + Tens[(num % 100) / 10] + Ones[num % 10];
        return new RomeNumber(result.Select(x => new RomeNumberEntity(x.ToString())).ToArray());
    }
}