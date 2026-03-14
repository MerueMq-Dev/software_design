namespace Project.Infrastructure;

public class MenuCommandExecutor
{
    // Храним команды по Id, чтобы быстро находить выбранную пользователем.
    private readonly Dictionary<int, BaseMenuCommand> _commands;

    // Абстракция вывода — позволяет не зависеть напрямую от типа вывода.
    private readonly IUserIO _output;

    // Команды передаются извне, чтобы можно было легко добавлять новые.
    public MenuCommandExecutor(IUserIO output, IEnumerable<BaseMenuCommand> commands)
    {
        _commands = commands.ToDictionary(c => c.Id);
        _output = output;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();

            _output.Write("Выберите пункт: ");
            string? input = _output.ReadLine();

            if (!int.TryParse(input, out int id))
            {
                _output.WriteLine("Некорректный ввод");
                continue;
            }

            if (!_commands.TryGetValue(id, out var command))
            {
                _output.WriteLine("Команда не найдена");
                continue;
            }

            // Полиморфный вызов — executor не знает конкретный тип команды.
            command.Execute();

            if (command is ExitMenuCommand)
                break;
        }
    }

    private void PrintMenu()
    {
        _output.WriteLine("\nМеню:");

        foreach (var cmd in _commands.Values.OrderBy(c => c.Id))
        {
            _output.WriteLine($"{cmd.Id}. {cmd.Title}");
        }

        _output.WriteLine();
    }
}

// Базовый класс для всех команд меню.
// Общая логика вынесена сюда, чтобы команды не дублировали код.
public abstract class BaseMenuCommand(IUserIO output)
{
    public abstract int Id { get; }
    public abstract string Title { get; }

    // Каждая команда сама реализует свою логику выполнения.
    public abstract void Execute();

    protected void PrintHeader(string title)
    {
        int totalWidth = 50;
        int padding = (totalWidth - title.Length - 2) / 2;

        output.WriteLine();
        output.WriteLine("╔" + new string('═', totalWidth - 2) + "╗");
        output.WriteLine("║" + new string(' ', padding) + title +
                        new string(' ', totalWidth - title.Length - padding - 2) + "║");
        output.WriteLine("╚" + new string('═', totalWidth - 2) + "╝");
        output.WriteLine();
    }

    protected void PrintSuccess(string message)
    {
        output.WriteLine($"\n✓ {message}");
    }

    protected void PrintError(string message)
    {
        output.WriteLine($"\n✗ {message}");
    }
}

// Конкретные команды меню.
public class StartMenuCommand(IUserIO output) : BaseMenuCommand(output)
{
    public override int Id => 1;
    public override string Title => "Старт";

    public override void Execute()
    {
        PrintHeader("Программа запускается");
    }
}

public class SalaryStatisticsMenuCommand(IUserIO output) : BaseMenuCommand(output)
{
    public override int Id => 2;
    public override string Title => "Статистика по зарплатам";

    public override void Execute()
    {
        PrintHeader("СТАТИСТИКА ПО ЗАРПЛАТАМ");

        output.WriteLine($"Средняя зарплата: {82_000:C}");
        output.WriteLine($"Сотрудников с зарплатой выше средней: {70}");
    }
}

public class ExitMenuCommand(IUserIO output) : BaseMenuCommand(output)
{
    public override int Id => 3;
    public override string Title => "Выход";

    public override void Execute()
    {
        output.WriteLine("\nЗавершение работы программы...");
    }
}

// Интерфейс вывода и вывода — позволяет заменить Console на другой источник вывода
// Например, GUI или консоль.
public interface IUserIO
{
    void WriteLine();
    void WriteLine(string message);
    void Write(string message);
    string? ReadLine();

}

// Реализация вывода и выводаы для консольного приложения.
class ConsoleOutput : IUserIO
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteLine()
    {
        Console.WriteLine();
    }
}