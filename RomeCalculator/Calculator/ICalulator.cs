namespace RomeCalculator;

internal interface ICalulator <in TInput, out TOutput>
{
    public TOutput Evaluate(TInput input);
}