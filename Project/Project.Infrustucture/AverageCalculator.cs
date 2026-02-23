namespace Project.Infrastructure;

public class AverageCalculator : IAverageCalculator
{
    public double CalculateAverage(int[] numbers)
    {
        if (numbers is null)
            throw new ArgumentNullException(nameof(numbers));

        if (numbers.Length == 0)
            throw new ArgumentException("Массив не может быть пустым", nameof(numbers));

        return numbers.Sum() / (double)numbers.Length;
    }
}

// Тесты не спасают от всех багов — только от тех, на которые ты догадался написать проверку.
// Например, что должен вернуть метод CalculateAverage для массива { 1, 2 } — 1 или 1.5? Если такой тест не написан,
// баг может жить в коде годами, пока кто-нибудь не столкнётся с ним в продакшене.