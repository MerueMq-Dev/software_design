namespace Project.Tests;

using FluentAssertions;
using Project.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

public class GradeCalculatorTests
{    
    [Fact]
    public void CalculateAverage_WhenValidGrades_ReturnsCorrectAverage()
    {
        var calculator = new GradeCalculator();
        var grades = new List<int> { 80, 90, 100 };

        var result = calculator.CalculateAverage(grades);

        result.Should().Be(90);
    }

    [Fact]
    public void CalculateAverage_WhenSingleGrade_ReturnsThatGrade()
    {
        var calculator = new GradeCalculator();
        var grades = new List<int> { 75 };

        var result = calculator.CalculateAverage(grades);

        result.Should().Be(75);
    }
    
    [Fact]
    public void CalculateAverage_WhenEmptyList_ThrowsArgumentException()
    {
        var calculator = new GradeCalculator();
        var grades = new List<int>();

        Action act = () => calculator.CalculateAverage(grades);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CalculateAverage_WhenNull_ThrowsArgumentNullException()
    {
        var calculator = new GradeCalculator();

        Action act = () => calculator.CalculateAverage(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CalculateAverage_WhenGradeOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var calculator = new GradeCalculator();
        var grades = new List<int> { 80, 150 };

        Action act = () => calculator.CalculateAverage(grades);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
