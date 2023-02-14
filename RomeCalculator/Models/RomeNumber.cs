using RomeCalculator.Helpers;

namespace RomeCalculator.Models;

internal class RomeNumber
{
    private readonly RomeNumberEntity[] _romeNumberEntities;
    
    private static readonly Dictionary<string, int> AlphabetDictionary = new()
    {
        { "I", 1 },
        { "V", 5 },
        { "X", 10 },
        { "L", 50 },
        { "C", 100 },
        { "D", 500 },
        { "M", 1000 }
    };

    public int ArabicNumber
    {
        get
        {
            int sum = 0;
            int n = _romeNumberEntities.Length;
            var romeNum = this._romeNumberEntities;
            for (int i = 0; i < n; i++) {
                
                if (i != n - 1 && AlphabetDictionary[romeNum[i].Value] < AlphabetDictionary[romeNum[i + 1].Value]) {
                    sum += AlphabetDictionary[romeNum[i + 1].Value] - AlphabetDictionary[romeNum[i].Value];
                    i++;
                } else {
                    sum += AlphabetDictionary[romeNum[i].Value];
                }
            }
            return sum;
        }
    }

    public RomeNumber(RomeNumberEntity[] romeNumberEntities)
    {
        _romeNumberEntities = romeNumberEntities;
    }

    public static RomeNumber operator +(RomeNumber a, RomeNumber b)
    {
        return (a.ArabicNumber + b.ArabicNumber).ToRoman();
    }
    
    public static RomeNumber operator *(RomeNumber a, RomeNumber b)
    {
        return (a.ArabicNumber * b.ArabicNumber).ToRoman();
    }
    
    public static RomeNumber operator -(RomeNumber a, RomeNumber b)
    {
        return (a.ArabicNumber - b.ArabicNumber).ToRoman();
    }

    public override string ToString()
    {
        return string.Join(string.Empty, _romeNumberEntities.Select(x => x.Value));
    }
}