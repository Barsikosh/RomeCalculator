namespace RomeCalculator.Calculator;

public interface ICalulator <in TInput, out TOutput>
{
    public TOutput Evaluate(TInput input);
}