using Microsoft.VisualBasic;
using System.Globalization;

namespace Project.Infrastructure;

public class DateExample
{
    public static void Run(string[] args)
    {
        string dateString = "2024-05-13T14:30:00+02:00";

        if (DateTimeOffset.TryParseExact(
            dateString,
            "yyyy-MM-dd'T'HH:mm:sszzz",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var date))
        {
            Console.WriteLine($"Дата: {date}");
            Console.WriteLine($"Дата в UTC: {date.UtcDateTime}");
        }
        else
        {
            Console.WriteLine("Не валидный формат даты");
        }
    }
}
