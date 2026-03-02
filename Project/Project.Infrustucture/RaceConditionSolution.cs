namespace Project.Infrastructure;


// В первом примере проблема в том, что counter++ — это не одна неделимая операция, а три шага: прочитать значение,
// увеличить и записать обратно. Когда несколько потоков делают это одновременно, они могут «перезаписать» результат 
// друг друга, и часть увеличений просто теряется — это и есть состояние гонки. Поэтому итоговое число получается меньше
// ожидаемого. Один из вариантов решения — это использовать атомарную операцию, например Interlocked.Increment,
// чтобы гарантировать корректное изменение общей переменной.


// Решение проблемы Race Condition
class RaceConditionSolution
{
    private static int counter = 0;

    static void Main()
    {
        int numberOfThreads = 10;
        Thread[] threads = new Thread[numberOfThreads];

        for (int i = 0; i < numberOfThreads; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 100000; j++)
                {
                    // Атомарное увеличение
                    Interlocked.Increment(ref counter);
                }
            });

            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Final counter value: {counter}");
    }
}
