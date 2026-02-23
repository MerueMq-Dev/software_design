using FluentAssertions;
using Project.Infrastructure;

public class AverageCalculatorTests
{
    [Theory]
    [InlineData(new int[] { 12, 10 }, 11)]
    [InlineData(new int[] { 100, 100 }, 100)]
    [InlineData(new int[] { 322, 322, 322 }, 322)]
    public void CalculateAverage_WhenAverageIsInteger_ReturnsCorrectResult(int[] input, double expected)
    {
        IAverageCalculator calculator = new AverageCalculator();
        calculator.CalculateAverage(input).Should().Be(expected);
    }

    [Theory]
    [InlineData(new int[] { 1, 2 }, 1.5)]
    [InlineData(new int[] { 3, 6 }, 4.5)]
    [InlineData(new int[] { 10, 5 }, 7.5)]
    public void CalculateAverage_WhenAverageIsFraction_ReturnsCorrectResult(int[] input, double expected)
    {
        IAverageCalculator calculator = new AverageCalculator();
        calculator.CalculateAverage(input).Should().Be(expected);
    }

    [Theory]
    [InlineData(new int[] { 0, 0, 0 }, 0)]
    public void CalculateAverage_WhenAllElementsAreZero_ReturnsZero(int[] input, double expected)
    {
        IAverageCalculator calculator = new AverageCalculator();
        calculator.CalculateAverage(input).Should().Be(expected);
    }

    [Theory]
    [InlineData(new int[] { })]    
    public void CalculateAverage_WhenArrayIsEmpty_ThrowsArgumentException(int[] input)
    {
        IAverageCalculator calculator = new AverageCalculator();
        Action act = () => calculator.CalculateAverage(input);
        act.Should().Throw<ArgumentException>();
    }


    [Theory] 
    [InlineData(null)]
    public void CalculateAverage_WhenArrayIsNull_ThrowsArgumentException(int[]? input)
    {
        IAverageCalculator calculator = new AverageCalculator();
        Action act = () => calculator.CalculateAverage(input);
        act.Should().Throw<ArgumentNullException>();
    }
}