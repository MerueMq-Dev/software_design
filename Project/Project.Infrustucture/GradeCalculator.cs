namespace Project.Infrastructure;

public class GradeCalculator : IGradeCalculator
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades == null)
            throw new ArgumentNullException(nameof(grades));

        if (grades.Count == 0)
            throw new ArgumentException("Список оценок не может быть пустым", nameof(grades));

        long sum = 0;

        foreach (var grade in grades)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grades),
                    "Оценки должны быть в диапазоне от 0 до 100");

            sum += grade;
        }

        return (double)sum / grades.Count;
    }
}
